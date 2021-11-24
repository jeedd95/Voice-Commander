using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���

    JHW_UnitInfo[] unitInfo;

    public int Score = 0; //�÷��̾� ����
    public int Gold = 25; //�÷��̾� ���
    public float specialGauge ; //����� ������
    public int currentPopulation; //���� �α���
    public int wholePopulationLimit; //��ü �α��� ���� (�� ���ֺ�x) (�ʱ�2 ~ �ִ� 33����)
    public float playTime; //�÷���Ÿ�� �ð� �� 

    public Text scoreT; //���� �ؽ�Ʈ
    public Text goldT; //��� �ؽ�Ʈ
    public Text specialgageT; //����� ������ �ؽ�Ʈ(����¼�)
    public Text text4; //����� ������ �ؽ�Ʈ(�����¼�)
    public Text Population; //�α��� ���� �ؽ�Ʈ
    public Text timer; // �÷��� Ÿ�� �ð� ����
    
    /* ���� ���� UI ============================*/
    public Text RifleManText; //�����ø��� �ؽ�Ʈ
    public Text ScoutText; 
    public Text SniperText; 
    public Text ArtilleryText; 
    public Text HeavyWeaponText; 
    public Text ArmouredText; 
    public Text TankText; 
    public Text HelicopterText; 
    public Text RaptorText;

    public int RifleManCurrentPopulation; //�����ø��� �����α�
    public int ScoutCurrentPopulation;
    public int SniperCurrentPopulation;
    public int ArtilleryCurrentPopulation;
    public int HeavyWeaponCurrentPopulation;
    public int ArmouredCurrentPopulation;
    public int TankCurrentPopulation;
    public int HelicopterCurrentPopulation;
    public int RaptorCurrentPopulation;
    /*=====================================*/

    public List<JHW_UnitManager> hidingUnits;
    public List<JHW_UnitManager> RushUnits;

   // bool ishiding; //���ڿ� ������
    bool isClickSpecialGauge = false; //����� �������� �����ִ���
    public bool CanProduce_Whole; // ��ü �α��� ���� �� �� �ִ���
    public bool CanProduce_Individual; //���ֺ� ���� �α��� ���� �� �� �ִ���

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
        int i;
        for (i = 0; i < 9; i++)
        {
            unitInfo[i] = GameObject.Find("UnitFactory").GetComponent<JHW_UnitFactory>().Units[i].GetComponent<JHW_UnitInfo>();
            print(unitInfo[i].populationLimit);
        }  //==================== �߸��� �� ã��
    }

    private void Update()
    {
            hidingUnits = JHW_UnitFactory.instance.myUnits; 
            RushUnits = JHW_UnitFactory.instance.myUnits;

        currentPopulation = JHW_UnitFactory.instance.myUnits.Count; // ���� �����ִ� �α��� �� ���ֵ��� ����

        if (currentPopulation < wholePopulationLimit) //�����α��� �ִ� �α����� ������ ���� ���� ���·� �������
        {
            CanProduce_Whole = true;
        }
        else CanProduce_Whole = false; //�ƴϸ� �Ұ���

        SpecialGageManager();
        PlusScore();
        PlusGold();
        Timer();
        TextManager();
        UnitPopulation();
    }

    float currentTime;
    float currentTime2;
    float currentTime3;

    float scoreEarnTime = 1; //1�ʸ��� ����
    float goldEarnTime = 3; //3�ʸ��� ���
    float specialEarnTime = 0.1f; // 2 �ʸ��� ������ ��

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
            specialGauge += 0.05f;
            currentTime3 = 0;
        }
    }

    public void OnClickDefense() //����¼� ��ưŬ���̺�Ʈ
    {
        if (isClickSpecialGauge == false && specialGauge >0)
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

    void Timer()
    {
        playTime += Time.deltaTime;
    }

    void UnitCoolDownText()
    {
       // RifleManText.text = "RifleMan\n" + 

    }

    void SpecialGageManager()
    {
        if (isClickSpecialGauge && specialGauge >= 0) // ����� �������� ������ ������ ��� ȣ��  
        {
            specialGauge -= 0.01f;
        }

        if (specialGauge < 0) //����� �������� 0������ ���� ��� ���¸� move�� �ٲٰ� Useoffensive�� ����
        {
            print("����� �������� �����մϴ�");

            isClickSpecialGauge = false;
            ChangePosture(JHW_UnitManager.State.Move);

            for (int i = 0; i < hidingUnits.Count; i++)
            {
                hidingUnits[i].unitinfo.UseDefensive = true;
            }

            for (int i = 0; i < RushUnits.Count; i++)
            {
                RushUnits[i].unitinfo.UseOffensive = false;
            }
        }

        if (isClickSpecialGauge == false && specialGauge <= 100) //����� �������� ������ ������������ ��� ȣ��
        {
            PlusSpecialGauge();
        }
    }


    void TextManager()
    {
        scoreT.text = "�÷��̾� ���� : " + Score;
        goldT.text = "��� : " + Gold;
        specialgageT.text = string.Format("{0,3:N0}", specialGauge) + " %";
        text4.text = specialgageT.text;
        Population.text = currentPopulation + " / " + wholePopulationLimit;

        RifleManText.text = "Rifleman\n" + RifleManCurrentPopulation + " / " + unitInfo[0].populationLimit;
        //ScoutText.text = "Scout\n" + ScoutCurrentPopulation + " / " + unitInfo[1].populationLimit;
        //SniperText.text = "Sniper\n" + SniperCurrentPopulation + " / " + unitInfo[2].populationLimit;
        //ArtilleryText.text = "Artillery\n" + ArtilleryCurrentPopulation + " / " + unitInfo[3].populationLimit;
        //HeavyWeaponText.text = "HeavyWeapon\n" + HeavyWeaponCurrentPopulation + " / " + unitInfo[4].populationLimit;
        //ArmouredText.text = "Armoured\n" + ArmouredCurrentPopulation + " / " + unitInfo[5].populationLimit;
        //TankText.text = "Tank\n" + TankCurrentPopulation + " / " + unitInfo[6].populationLimit;
        //HelicopterText.text = "Helicopter\n" + HelicopterCurrentPopulation + " / " + unitInfo[7].populationLimit;
        //RaptorText.text = "Raptor\n" + RaptorCurrentPopulation + " / " + unitInfo[8].populationLimit;

    }

    void UnitPopulation()
    {
        for (int i = 0; i < JHW_UnitFactory.instance.myUnits.Count; i++)
        {
            switch(JHW_UnitFactory.instance.myUnits[i].unitinfo.unitName)
            {
                case "RifleMan":
                    RifleManCurrentPopulation++;
                    break;

            }
        }


        //if (RifleManCurrentPopulation < JHW_UnitFactory.instance.units.populationLimit)
        //{
        //    CanProduce_Individual = true;
        //}
        //else CanProduce_Individual = false;
    }

    /*���� ���ó*/
    public void OnClickWholePopulationUp() //�ִ� �α��� ���� ��ư
    {
        if (wholePopulationLimit<33)
        {
            wholePopulationLimit++;
        }
        else
        {
            print("�� �̻� �ִ� �α��� �ø� �� �����ϴ�");
            return;
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

