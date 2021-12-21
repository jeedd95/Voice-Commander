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
        BackendReturnObject bro2 = Backend.BMember.CreateNickname(Nickname.text);






        if(!BRO.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "�ߺ��� ID�Դϴ�";
        }
        if (!bro2.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "�г��� ������ �����߽��ϴ�\n20�� �̻�, �г��� �ߺ�, �յ� ������ �ִ� ��� ����� �� �����ϴ�";
        }
        if(BRO.IsSuccess() && bro2.IsSuccess())
        {
            MessageBox2.SetActive(true);
            MessageBoxText2.text = "ȸ�����Կ� �����߽��ϴ�";
        }

    }

    public void OnClickLogin()
    {
        BackendReturnObject bro = Backend.BMember.CustomLogin(id.text, password.text);

        if (bro.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "�α��ο� �����߽��ϴ� \n ��� �� ������ ���۵˴ϴ�";

            Invoke("OnClickToPlayScene", 20f);

            //Debug.Log("�α��ο� �����߽��ϴ�");
        }
        else
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "�α��ο� �����߽��ϴ�\n ���̵�� ��й�ȣ�� Ȯ�����ֽʽÿ�";
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
