using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static Login instance; //싱글톤

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
            Debug.Log("회원가입에 성공했습니다");
        }
        else
        {
            Debug.Log("회원가입에 실패했습니다");
        }
    }

    public void OnClickLogin()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, password.text);
        if (bro.IsSuccess())
        {
            Debug.Log("로그인에 성공했습니다");
            Invoke("ToPlayScene", 2f);
        }
        else
        {
            Debug.Log("로그인에 실패했습니다");
        }
    }

    void ToPlayScene()
    {
        SceneManager.LoadScene("JHW_TestScene+Map");
    }
}
