using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaltManager
{
    protected string path;
    protected const string fileName = "config";
    protected static string finalPath;

    public SaltManager()
    {
        if(finalPath==null)
        {
            path = Application.dataPath;
            finalPath = path + "/" + fileName;
        }

    }

    public bool FileExist()
    {
        return File.Exists(finalPath);
    }

    public string FindLine(int pos)
    {
        if (FileExist())
        {
            StreamReader sr = File.OpenText(finalPath);
            string str = null;
            string text = null;
            int num = 0;
            while ((str = sr.ReadLine()) != null)
            {
                num++;
                if (num == pos)
                {
                    text = str;
                    break;
                }
            }
            sr.Close();
            sr.Dispose();

            return text;
        }
        return null;
    }
}
