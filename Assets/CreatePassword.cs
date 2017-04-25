using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class CreatePassword : MonoBehaviour
{
    private string path;
    private const string fileName = "password";
    private static string finalPath;

    public CreatePassword()
    {
        if (finalPath == null)
        {
            path = Application.dataPath;
            finalPath = path + "/" + fileName;
        }

    }


    public string Create(string password)
    {
        try
        {
            int pos1, pos2;
            string result, str1, str2;
            GetSalt(out pos1, out pos2, out str1, out str2);
            result = CreateSHA512(password, str1);
            result = CreateMD5(result, str2);
            result = pos1.ToString().PadLeft(2, '0') + pos2.ToString().PadLeft(2, '0') + result;

            FileStream fs1 = new FileStream(finalPath, FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1);
            sw.WriteLine(result);//开始写入值

            sw.Close();
            fs1.Close();
            sw.Dispose();
            fs1.Dispose();

            return result;
        }
        catch (Exception ex)
        {

            throw new Exception("GetSHA512HashFromString() fail,error:" + ex.Message);
        }
    }

    bool GetSalt(out int pos1, out int pos2, out string str1, out string str2)
    {
        try
        {


            SaltManager salt = new SaltManager();
            if (salt.FileExist())
            {
                pos1 = UnityEngine.Random.Range(0, 100);
                pos2 = UnityEngine.Random.Range(0, 100);
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

    string CreateMD5(string password, string salt)
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
