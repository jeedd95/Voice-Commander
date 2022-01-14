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
    public InputField Nickname;

    //회원가입페이지
    public InputField id2;
    public InputField password2;
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

    private void Update()
    {
        if(id.isFocused==true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                password.Select();
            }
        }
        if(password.isFocused==true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Nickname.Select();
            }
        }
        if(Nickname.isFocused==true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                id.Select();
            }
        }
        if (id2.isFocused == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                password2.Select();
            }
        }
        if (password2.isFocused == true)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                id2.Select();
            }
        }
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && SignUpWindow.activeSelf==false)
        {
            OnClickLogin();
        }
        if(SignUpWindow.activeSelf && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            OnClickSignUp();
        }
    }

    public void OnClickSignUpUI()
    {
        SignUpWindow.SetActive(true);
        exitBtn.SetActive(false);


    }
    public void OnClickSignUp()
    {
        BackendReturnObject BRO = Backend.BMember.CustomSignUp(id2.text, password2.text);
        if(!BRO.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "회원 가입에 실패했습니다\n아이디가 중복이거나 유효하지 않습니다";
        }
        else
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "회원 가입에 성공했습니다!";
        }

    }

    public void OnClickLogin()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, password.text);
        if(!bro.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "로그인에 실패했습니다\n 아이디와 비밀번호를 확인해주십시오";
        }
        Backend.BMember.UpdateNickname("c2q6d6c7t9a6z3z2zzx");
        BackendReturnObject bro2 = Backend.BMember.CreateNickname(Nickname.text);

        if (bro.IsSuccess() && bro2.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "로그인에 성공했습니다 \n 잠시 후 게임이 시작됩니다";
            Invoke("OnClickToPlayScene", 25f);
            //Debug.Log("로그인에 성공했습니다");
        }
        else if(!bro2.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "닉네임 생성에 실패했습니다\n20자 이상, 닉네임 중복, 앞뒤 공백이 있는 경우 사용할 수 없습니다";
        }
    }

    public void OnClickToPlayScene()
    {
        SceneManager.LoadScene("JHW_StartCine");
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
