using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���

    public int[] unitMaxCount; // ���� ���������� �� ����

    /// <summary> ���̾� ���� </summary>
    public int Score = 0;
    public int Gold = 25; //�÷��̾� ���
    public float specialGauge; //����� ������
    public int currentPopulation; //���� �α���
    public int wholePopulationLimit; //��ü �α��� ���� (�� ���ֺ�x) (�ʱ�2 ~ �ִ� 33����)
    public float playTime; //�÷���Ÿ�� �ð� ��
    public float[] currentCool; //���ֺ� ���� ��Ÿ�� �迭
    public bool[] CoolDownComplete; // ���� ��Ÿ���� �� ���Ҵ���

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
    public bool CanProduce; // ��ü �α��� ���� �� �� �ִ���
    //public bool CanProduce_Individual; //���ֺ� ���� �α��� ���� �� �� �ִ���

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
    public enum UnitType
    {
        RifleMan,
        Scout,
        Sniper,
        Artillery,
        Heavy_Weapon,
        Armoured,
        Tank,
        Helicopter,
        Raptor,
    }
    public int[] _maxUnit = { 3, 2, 2, 999, 999, 999, 999, 5, 5 }; //�ִ� �α���
    public float[] _cooldown = {25,10,10,20,30,40,50,60,70 }; //���� ��Ÿ��


    public void SetMaxUnit(UnitType unitType, int amount)
    {
        unitMaxCount[(int)unitType] = amount;
    }
    //public int GetMaxUnit(UnitType unitType)
    //{
    //    return unitMaxCount[(int)unitType];
    //}

    private void Start()
    {
        CoolDownComplete = new bool[_cooldown.Length];
        for (int i = 0; i < CoolDownComplete.Length; i++)
        {
            CoolDownComplete[i] = true;
        }
        currentCool = new float[_cooldown.Length];
        for (int j = 0; j < currentCool.Length; j++) //���� ��Ÿ�� �迭�� ���� ���� ��Ÿ�� ��ġ�� ����
        {
            currentCool[j] = _cooldown[j];
        }

        unitMaxCount = new int[JHW_UnitFactory.instance.Units.Length];

        for (int i = 0; i < unitMaxCount.Length; i++)
        {
            SetMaxUnit((UnitType)i, _maxUnit[i]);
        }
        //int max = GetMaxUnit(UnitType.Raptor);
    }

    private void Update()
    {
        print(_cooldown[0]);

        hidingUnits = JHW_UnitFactory.instance.myUnits;
        RushUnits = JHW_UnitFactory.instance.myUnits;

        currentPopulation = JHW_UnitFactory.instance.myUnits.Count; // ���� �����ִ� �α��� �� ���ֵ��� ����

        if (currentPopulation < wholePopulationLimit) //�����α��� �ִ� �α����� ������ ���� ���� ���·� �������
        {
            CanProduce = true;
        }
        else CanProduce = false; //�ƴϸ� �Ұ���


        CoolTimer(RifleManCurrentPopulation,0);

        SpecialGageManager();
        PlusScore();
        PlusGold();
        Timer();
        TextManager();

        //for (int i = 0; i < unitCurrentCount.Length; i++)
        //{
        //setCurrentUnit((UnitType)i, _currentUnit[i]);
        //}


    }

    float currentTime;
    float currentTime2;
    float currentTime3;

    float scoreEarnTime = 1; //1�ʸ��� ����
    float goldEarnTime = 3; //3�ʸ��� ���
    float specialEarnTime = 0.1f; // 2 �ʸ��� ������ ��

    /// <summary>
    /// �� �ʸ��� ���� �ִ� �ڵ�
    /// </summary>
    void PlusScore()
    {
        currentTime += Time.deltaTime;
        if (currentTime > scoreEarnTime)
        {
            Score += 15;
            currentTime = 0;
        }
    }
    /// <summary>
    /// �� �ʸ��� ��� �ִ� �ڵ�
    /// </summary>
    void PlusGold()
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
        if (isClickSpecialGauge == false && specialGauge > 0)
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

    void Timer() //�÷���Ÿ�� ���
    {
        playTime += Time.deltaTime;
    } 

   

   public void CoolTimer(int population, int index) // ��Ÿ������ �÷��� ����� �Լ�
    {
        if (population == unitMaxCount[index])
        {
                currentCool[index] -= 1f;
        }

          else if (currentCool[index] <= 0)
            {
                CoolDownComplete[index] = true; //��Ÿ���� �� ���Ҵ�
                currentCool[index] = _cooldown[0]; //0�ʰ� �Ǹ� �ٽ� �ǵ�����
            }
            else if (currentCool[index] > 0)
            {
                CoolDownComplete[index] = false;
            }

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

        RifleManText.text ="RifleMan\n" + RifleManCurrentPopulation + " / " + unitMaxCount[0]+"\n" + currentCool[0];
        ScoutText.text =  "Scout\n" + ScoutCurrentPopulation + " / " + unitMaxCount[1] + "\n" + currentCool[1];
        SniperText.text = "Sniper\n" + SniperCurrentPopulation + " / " + unitMaxCount[2] + "\n" + currentCool[2];
        ArtilleryText.text = "Artillery\n" + ArtilleryCurrentPopulation + " / " + unitMaxCount[3] + "\n" + currentCool[3];
        HeavyWeaponText.text = "HeavyWeapon\n" + HeavyWeaponCurrentPopulation + " / " + unitMaxCount[4] + "\n" + currentCool[4];
        ArmouredText.text = "Armoured\n" + ArmouredCurrentPopulation + " / " + unitMaxCount[5] + "\n" + currentCool[5];
        TankText.text = "Tank\n" + TankCurrentPopulation + " / " + unitMaxCount[6] + "\n" + currentCool[6];
        HelicopterText.text = "Helicopter\n" + HelicopterCurrentPopulation + " / " + unitMaxCount[7] + "\n" + currentCool[7];
        RaptorText.text = "Raptor\n" + RaptorCurrentPopulation + " / " + unitMaxCount[8] + "\n" + currentCool[8];

    }

    

    /*���� ���ó*/
    public void OnClickWholePopulationUp() //�ִ� �α��� ���� ��ư
    {
        if (wholePopulationLimit < 33)
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

