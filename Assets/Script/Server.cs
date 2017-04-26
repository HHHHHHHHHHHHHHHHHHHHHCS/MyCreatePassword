using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Server : MonoBehaviour
{
    [SerializeField]
    private Button oneClickButton;
    [SerializeField]
    private Button saltButton;
    [SerializeField]
    private Button passwordButton;
    [SerializeField]
    private InputField resultText;
    [SerializeField]
    private Button copyButton;

    private void Awake()
    {
        if (oneClickButton != null)
        {
            oneClickButton.onClick.AddListener(OneClick);
        }
        if (oneClickButton != null)
        {
            saltButton.onClick.AddListener(CreateSaltClick);
        }
        if (oneClickButton != null)
        {
            passwordButton.onClick.AddListener(CreatePasswordClick);
        }
        if(copyButton != null)
        {
            copyButton.onClick.AddListener(CopyButtonClick);
        }

    }

    void OneClick()
    {
        CreateSaltClick();
        CreatePasswordClick();
    }

    void CreateSaltClick()
    {
        SaltManager_Server salt = new SaltManager_Server();
        salt.Create();
    }

    void CreatePasswordClick()
    {
        CreatePassword create = new CreatePassword();
        string str = create.Create();
        if (resultText != null)
        {
            resultText.text = str;
        }

    }

    void CopyButtonClick()
    {
        if (resultText != null)
        {
            string str = resultText.text;
            TextEditor textEditor = new TextEditor()
            {
                text = str
            };
            textEditor.OnFocus();
            textEditor.Copy();
        }

    }


}
