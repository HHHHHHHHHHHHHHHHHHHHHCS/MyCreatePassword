using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaltManager
{
    private string path;
    private const string fileName = "config";
    private static string finalPath;

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

    public void Create()
    {
        FileStream fs1 = new FileStream(finalPath, FileMode.Create, FileAccess.Write);//创建写入文件 
        StreamWriter sw = new StreamWriter(fs1);
        for (int i = 0; i < 100; i++)
        {
            char[] ch = new char[16];
            for (int j = 0; j < 16; j++)
            {
                int ran = Random.Range(0, 3);
                char ranChar = ' ';
                switch (ran)
                {
                    case 0:
                        {
                            ranChar = (char)(Random.Range(48, 58));
                            break;
                        }
                    case 1:
                        {
                            ranChar = (char)(Random.Range(65, 91));
                            break;
                        }
                    case 2:
                        {
                            ranChar = (char)(Random.Range(97, 123));
                            break;
                        }
                }

                ch[j] = ranChar;
            }
            sw.WriteLine(ch);//开始写入值
        }

        sw.Close();
        fs1.Close();
        sw.Dispose();
        fs1.Dispose();
    }
}
