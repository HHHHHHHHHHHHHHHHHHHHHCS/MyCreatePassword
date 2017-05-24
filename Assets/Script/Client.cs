using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    [SerializeField]
    private Button createButton;
    [SerializeField]
    private Button checkButton;
    [SerializeField]
    private Text resultText;

    private void Awake()
    {
        if(createButton!=null)
        {
            createButton.onClick.AddListener(CreateInfoClick);
        }
        if (checkButton != null)
        {
            checkButton.onClick.AddListener(CheckPswClick);
        }
        
    }

    public void CreateInfoClick()
    {
        GetPCInfo pcInfo = new GetPCInfo();
        string result = pcInfo.PCInfo();
        result = pcInfo.CreatePsw(result);
        if (resultText != null)
        {
            resultText.text = result!=null ? "创建成功!" : "创建失败！";
        }
    }

    public void CheckPswClick()
    {
        EqualPassWord equal = new EqualPassWord();
        GetPCInfo pcInfo = new GetPCInfo();
        string pc = pcInfo.PCInfo();
        bool bl = pc==null?false:equal.EqualPassword(pc);
        if (resultText!=null)
        {
            resultText.text = bl ? "绑定成功!" : "绑定失败！";
        }

    }
}
