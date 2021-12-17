using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static Login instance; //�̱���

    public InputField id;
    public InputField password;

    private void Awake()
    {
        
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        

        Backend.Initialize();
    }
    private void Start()
    {
        
    }

    public void OnClickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(id.text, password.text);
        if (BRO.IsSuccess())
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
        else
        {
            Debug.Log("ȸ�����Կ� �����߽��ϴ�");
        }
    }

    public void OnClickLogin()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, password.text);
        if (bro.IsSuccess())
        {
            Debug.Log("�α��ο� �����߽��ϴ�");
            Invoke("ToPlayScene", 2f);
        }
        else
        {
            Debug.Log("�α��ο� �����߽��ϴ�");
        }
    }

    void ToPlayScene()
    {
        SceneManager.LoadScene("JHW_TestScene+Map");
    }
}
