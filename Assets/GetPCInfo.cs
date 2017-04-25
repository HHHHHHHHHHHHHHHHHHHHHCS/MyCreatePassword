using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class GetPCInfo 
{
    private string path;
    private const string fileName = "psw";
    private static string finalPath;

    public GetPCInfo()
    {
        if (finalPath == null)
        {
            path = Application.dataPath;
            finalPath = path + "/" + fileName;
        }

    }

    public void CreatePsw(string info)
    {
        FileStream fs1 = new FileStream(finalPath, FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);

        sw.WriteLine(CreateMD5(info));//开始写入值


        sw.Close();
        fs1.Close();
        sw.Dispose();
        fs1.Dispose();
    }

    string CreateMD5(string password)
    {
        byte[] bytValue = Encoding.UTF8.GetBytes(password);
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
