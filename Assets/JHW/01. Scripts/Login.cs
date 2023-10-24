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
    public InputField Nickname;

    //ȸ������������
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
        JHW_SoundManager.instance.MyAudio[0].Stop();
        JHW_SoundManager.instance.flag = false;
        JHW_SoundManager.instance.state = JHW_SoundManager.State.LoginScene;

        // Backend.Initialize();
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
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);

        SignUpWindow.SetActive(true);
        exitBtn.SetActive(false);


    }
    public void OnClickSignUp()
    {
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);

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
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);

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
            Invoke("OnClickToPlayScene", 25f);


            //Debug.Log("�α��ο� �����߽��ϴ�");
        }
        else if(!bro2.IsSuccess())
        {
            MessageBox.SetActive(true);
            MessageBoxText.text = "�г��� ������ �����߽��ϴ�\n20�� �̻�, �г��� �ߺ�, �յ� ������ �ִ� ��� ����� �� �����ϴ�";
        }

    }

    public void OffLineLogin()
    {
        MessageBox.SetActive(true);
        MessageBoxText.text = "�α��ο� �����߽��ϴ� \n ��� �� ������ ���۵˴ϴ�";
        Invoke("OnClickToPlayScene", 25f);
    }

    public void OnClickToPlayScene()
    {
        SceneManager.LoadScene("JHW_StartCine");
        //JHW_SoundManager.instance.flag = false;
        //JHW_SoundManager.instance.state = JHW_SoundManager.State.Idle;

    }
    public void OnClickExit()
    {
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);

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
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);

        SignUpWindow.SetActive(false);
        exitBtn.SetActive(true);
    }
    
}
