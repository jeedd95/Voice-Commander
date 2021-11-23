using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���


    public int Score = 0; //�÷��̾� ����
    public int Gold = 25; //�÷��̾� ���
    public float specialGauge = 0f; //����� ������

    public Text scoreT; //���� �ؽ�Ʈ
    public Text goldT; //��� �ؽ�Ʈ
    public Text specialgageT; //����� ������ �ؽ�Ʈ
    public Text text4; //����� ������ �ؽ�Ʈ

    public List<JHW_UnitManager> hidingUnits;
    public List<JHW_UnitManager> RushUnits;

    bool ishiding; //���ڿ� ������
    bool isClickSpecialGauge = false;

    //bool bDefensiveDown;


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
        // Mathf.Clamp(specialGauge, 0, 100); //����� �������� 0~100 ������ ����

            hidingUnits = JHW_UnitFactory.instance.myUnits;
            RushUnits = JHW_UnitFactory.instance.myUnits;


        if (isClickSpecialGauge) // ����� �������� ������ ������ ��� ȣ��
        {
            specialGauge -= 0.01f;
        }

        if (isClickSpecialGauge == false) //����� �������� ������ ������������ ��� ȣ��
        {
            PlusSpecialGauge();
        }

        PlusScore();
        PlusGold();

        scoreT.text = "�÷��̾� ���� : " + Score;
        goldT.text = "��� : " + Gold;
        specialgageT.text = string.Format("{0,3:N0}", specialGauge) + " %";
        text4.text = specialgageT.text;

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

    public void OnClickDefense() //����¼� ��ưŬ���̺�Ʈ
    {
        if (isClickSpecialGauge == false)
        {
            isClickSpecialGauge = true;
            ChangePosture(JHW_UnitManager.State.Hide);
        }
    }
    public void NotClickDefense() //����¼��� �� �� ����� ���ֵ��� Move���·�
    {
        if (isClickSpecialGauge == true)
        {
            isClickSpecialGauge = false;
            ChangePosture(JHW_UnitManager.State.Move);
        }
    }

    public void OnClickOffense() //�����¼� ��ưŬ���̺�Ʈ
    {
        if (isClickSpecialGauge == false)
        {
            isClickSpecialGauge = true;
            print("�����¼� ��");
            for (int i = 0; i < RushUnits.Count; i++)
            {
                RushUnits[i].unitinfo.UseOffensive = true;
            }
        }
    }
    public void NotClickOffense()
    {
        if (isClickSpecialGauge == true)
        {
            isClickSpecialGauge = false;
            print("�����¼� ����");

            for (int i = 0; i < RushUnits.Count; i++)
            {
                RushUnits[i].unitinfo.UseOffensive = false;
            }
        }
    }
    void ChangePosture(JHW_UnitManager.State state) //���̾��Ű���ִ� ��� Player ������ �˻��ϰ� ���º���
    {
        List<JHW_UnitManager> playerUnits = JHW_UnitFactory.instance.myUnits;

        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].SetState(state);
        }

    }

}

