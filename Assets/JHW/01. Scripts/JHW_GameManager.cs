using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���
    //JHW_UnitManager unitManager;
    GameObject[] MyUnits; //���� �����ִ� ��� �� ���ֵ�
    GameObject[] MyUnits2;

    public int Score = 0; //�÷��̾� ����
    public int Gold = 25 ; //�÷��̾� ���
    public float specialGauge = 0f; //����� ������

    public Text text; //���� �ؽ�Ʈ
    public Text text2; //��� �ؽ�Ʈ
    public Text text3; //����� ������ �ؽ�Ʈ
    public Text text4; //����� ������ �ؽ�Ʈ

    bool isClickSpecialGauge=false;

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
    }

    private void Start()
    {

    }

    private void Update()
    {
        PlusScore();
        PlusGold();
        
        Mathf.Clamp(specialGauge, 0, 100); //����� �������� 0~100 ������ ����
        if (isClickSpecialGauge == false) PlusSpecialGauge(); //����� �������� ������ ������������ ����
        if (isClickSpecialGauge == true) DefensivePosture(); //����� �������� ������ ������ ����

        text.text = "�÷��̾� ���� : " + Score;
        text2.text = "��� : " + Gold;
        text3.text = string.Format("{0,3:N0}", specialGauge)  + " %";
        text4.text = text3.text;
    }

    float currentTime;
    float currentTime2;
    float currentTime3;

    float scoreEarnTime = 1; //1�ʸ��� ����
    float goldEarnTime = 3; //3�ʸ��� ���
    float specialEarnTime = 2f; // 2 �ʸ��� ������ ��

    void PlusScore() //�� �ʸ��� ���� �ִ� �ڵ�
    {
        currentTime += Time.deltaTime;
        if (currentTime > scoreEarnTime)
        {
            Score += 15;
            currentTime = 0;
        }
    }

    void PlusGold() //�� �ʸ��� ��� �ִ� �ڵ�
    {
        currentTime2 += Time.deltaTime;
        if (currentTime2 > goldEarnTime)
        {
            Gold += 5;
            currentTime2 = 0;
        }
    }

    void PlusSpecialGauge() //�� �ʸ��� ������ �÷��ִ� �ڵ�
    {
        currentTime3 += Time.deltaTime;
        if (currentTime3 > specialEarnTime)
        {
            specialGauge += 1f;
            currentTime3 = 0;
        }
    }

    void DefensivePosture() // ����¼�, ���̾��Ű���ִ� ��� Player �±� ������ �˻��ϰ� State�� Hide�� ����
    {
        MyUnits = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < MyUnits.Length; i++) 
        {
            if(MyUnits[i].transform.childCount == 0)
            {
                //MyUnits[i].
            }

            MyUnits2[i] = MyUnits[i].transform.parent.gameObject;
            MyUnits2[i].GetComponent<JHW_UnitManager>().state = JHW_UnitManager.State.Hide;
        }
        specialGauge -= 0.01f; //������ �ٿ�
    }

    public void OnClickDefense() //����¼� ��ưŬ���̺�Ʈ
    {
        isClickSpecialGauge = true;
    }
    public void NotClickDefense()
    {
        isClickSpecialGauge = false;

        MyUnits = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < MyUnits.Length; i++) //���̾��Ű���ִ� ��� ������ �˻��ϰ� State�� Move�� ����
        {
            MyUnits[i].GetComponent<JHW_UnitManager>().state = JHW_UnitManager.State.Move;
        }
    }
    public void OnClickOffense() //�����¼� ��ưŬ���̺�Ʈ
    {
        isClickSpecialGauge = true;
    }
    public void NotClickOffense()
    {
        isClickSpecialGauge = false;
    }


}

