using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public static Login instance; //�̱���

    public GameObject MessageBox;
    public Text MessageBoxText;
    //1��° ������
    public InputField id;
    public InputField password;
    public GameObject exitBtn;

    //ȸ������������
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
        if(!BRO.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "ȸ�� ���Կ� �����߽��ϴ�\n���̵� �ߺ��̰ų� ��ȿ���� �ʽ��ϴ�";
        }
        else
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "ȸ�� ���Կ� �����߽��ϴ�!";
        }

    }

    public void OnClickLogin()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, password.text);
        if(!bro.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "�α��ο� �����߽��ϴ�\n ���̵�� ��й�ȣ�� Ȯ�����ֽʽÿ�";
        }
        Backend.BMember.UpdateNickname("c2q6d6c7t9a6z3z2zzx");
        BackendReturnObject bro2 = Backend.BMember.CreateNickname(Nickname.text);

        if (bro.IsSuccess() && bro2.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "�α��ο� �����߽��ϴ� \n ��� �� ������ ���۵˴ϴ�";
            Invoke("OnClickToPlayScene", 30f);
            //Debug.Log("�α��ο� �����߽��ϴ�");
        }
        else if(!bro2.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "�г��� ������ �����߽��ϴ�\n20�� �̻�, �г��� �ߺ�, �յ� ������ �ִ� ��� ����� �� �����ϴ�";
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
