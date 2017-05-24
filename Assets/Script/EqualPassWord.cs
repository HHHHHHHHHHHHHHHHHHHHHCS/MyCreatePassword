using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class EqualPassWord
{
    private string path;
    private const string fileName = "password";
    private static string finalPath;

    public EqualPassWord()
    {
        if (finalPath == null)
        {
            path = Application.dataPath;
            finalPath = path + "/" + fileName;
        }

    }

    public bool EqualPassword(string input)
    {
        if (input == null)
        {
            return false;
        }
        string password = GetPassword();
        if (password == null)
        {
            return false;
        }

        int pos1 = -1, pos2 = -1;
        GetPos(password, out pos1, out pos2);
        if (pos1 == -1 || pos2 == -1)
        {
            return false;
        }
        else
        {
            string salt1, salt2;
            GetSalt(pos1, pos2, out salt1, out salt2);
            if (salt1 == null || salt2 == null)
            {
                return false;
            }
            else
            {
                string result;
                result = CreateMD5(input);
                result = CreateSHA512(result, salt1);
                result = CreateMD5(result, salt2);
                result = pos1.ToString().PadLeft(2, '0') + pos2.ToString().PadLeft(2, '0') + result;
                if (result == password)
                {
                    return true;
                }
                return false;
            }
        }
    }

    string GetPassword()
    {
        try
        {
            StreamReader sr = File.OpenText(finalPath);
            string str = sr.ReadLine();
            sr.Close();
            sr.Dispose();
            return str;
        }
        catch
        {
            return null;
        }
    }

    bool GetPos(string password, out int pos1, out int pos2)
    {
        try
        {
            pos1 = int.Parse(password.Substring(0, 2));
            pos2 = int.Parse(password.Substring(2, 2));
            return true;
        }
        catch
        {
            pos1 = pos2 = -1;
            return false;
        }
    }

    bool GetSalt(int pos1, int pos2, out string str1, out string str2)
    {
        try
        {
            SaltManager salt = new SaltManager();
            if (salt.FileExist())
            {
                str1 = salt.FindLine(pos1);
                str2 = salt.FindLine(pos2);
                return true;
            }
            else
            {
                pos1 = pos2 = -1;
                str1 = str2 = null;
                return false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromString() fail,error:" + ex.Message);

        }
    }

    string CreateSHA512(string password, string salt)
    {
        byte[] bytValue = Encoding.UTF8.GetBytes(password + salt);
        try
        {
            SHA512 sha512 = new SHA512Managed();
            byte[] retVal = sha512.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetSHA512HashFromString() fail,error:" + ex.Message);
        }

    }

    string CreateMD5(string password, string salt = "")
    {
        byte[] bytValue = Encoding.UTF8.GetBytes(password + salt);
        try
        {
            MD5 md5 = MD5.Create();
            byte[] retVal = md5.ComputeHash(bytValue);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("GetMD5HashFromString() fail,error:" + ex.Message);
        }
    }
}
