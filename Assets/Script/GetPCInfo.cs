﻿using System;
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

    public string CreatePsw(string info)
    {
        if (string.IsNullOrEmpty(info))
        {
            return null;
        }

        FileStream fs1 = new FileStream(finalPath, FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        string result = CreateMD5(info);
        sw.WriteLine(result);//开始写入值


        sw.Close();
        fs1.Close();
        sw.Dispose();
        fs1.Dispose();
        return result;
    }

    string CreateMD5(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            return null;
        }

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


    public string PCInfo()
    {
        SteamVR steamVR = SteamVR.instance;
        string systemInfo = "_deviceName:" + SystemInfo.deviceName + "_deviceType:" + SystemInfo.deviceType +
            "_deviceUniqueIdentifier:" + SystemInfo.deviceUniqueIdentifier + "_graphicsDeviceID:" + SystemInfo.graphicsDeviceID +
            "_graphicsDeviceName:" + SystemInfo.graphicsDeviceName + "_graphicsDeviceVendor:" + SystemInfo.graphicsDeviceVendor +
            "_graphicsDeviceVendorID:" + SystemInfo.graphicsDeviceVendorID +
            "_graphicsMemorySize:" + "_operatingSystem:" + SystemInfo.operatingSystem +
            "_processorCount:" + SystemInfo.processorCount + "_processorType:" + SystemInfo.processorType +
            "_systemMemorySize:" + SystemInfo.systemMemorySize;
        if (steamVR != null)
        {
            systemInfo += "_hmd_TrackingSystemName:" + steamVR.hmd_TrackingSystemName + "_hmd_SerialNumber:" + steamVR.hmd_SerialNumber;
        }
        else
        {
            systemInfo = null;
        }
        return systemInfo;
    }
}
