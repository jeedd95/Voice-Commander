using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static Login instance; //싱글톤

    public GameObject MessageBox;
    public Text MessageBoxText;
    //1번째 페이지
    public InputField id;
    public InputField password;
    public GameObject exitBtn;

    //회원가입페이지
    public InputField id2;
    public InputField password2;
    public InputField Nickname;
    public GameObject SignUpWindow;
    public GameObject MessageBox2;
    public Text MessageBoxText2;
    public GameObject NicknameBox;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //  DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Backend.Initialize();
    }

    public void OnClickSignUpUI()
    {
        SignUpWindow.SetActive(true);
        exitBtn.SetActive(false);


    }
    public void OnClickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(id2.text, password2.text);
        BackendReturnObject bro2 = Backend.BMember.CreateNickname(Nickname.text);






        if(!BRO.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "중복된 ID입니다";
        }
        if (!bro2.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "닉네임 생성에 실패했습니다\n20자 이상, 닉네임 중복, 앞뒤 공백이 있는 경우 사용할 수 없습니다";
        }
        if(BRO.IsSuccess() && bro2.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "회원가입에 성공했습니다";
        }

    }

    public void OnClickLogin()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, password.text);

        if (bro.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "로그인에 성공했습니다 \n 잠시 후 게임이 시작됩니다";

            Invoke("OnClickToPlayScene", 20f);

            //Debug.Log("로그인에 성공했습니다");
        }
        else
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "로그인에 실패했습니다\n 아이디와 비밀번호를 확인해주십시오";
        }
    }

    public void OnClickToPlayScene()
    {
        SceneManager.LoadScene("JHW_TestScene+Map");
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
    public void OnClickReturn()
    {
        MessageBox.SetActive(false);
    }
    public void OnClickReturn3()
    {
        MessageBox2.SetActive(false);
    }
    public void OnClickReturn2()
    {
        SignUpWindow.SetActive(false);
        exitBtn.SetActive(true);
    }
}
