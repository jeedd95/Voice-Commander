using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���

    //public int[] unitMaxCount; // ���� ���������� �� ����

    /// <summary> ���̾� ���� </summary>
    public int Score = 0;
    public int Gold = 25; //�÷��̾� ���
    public float specialGauge; //����� ������
    public int[] currentPopulationArray; //���� �α��� �迭
    public int currentPopulation; //���� �α��� = ���� �α��� �迭�� ��� ��
    public int wholePopulationLimit; //��ü �α��� ���� (�ʱ�4)
    public float playTime; //�÷���Ÿ�� �ð� ��
    public float[] currentCool; //���ֺ� ���� ��Ÿ�� �迭
    public int playerLevel; //�÷��̾� ����
    public float currentExp; //���� ����ġ
    public int maxlevel = 20;

    public bool[] CoolDownReady; // ���� ��Ÿ���� �� ���Ҵ���
    bool isClickSpecialGauge = false; //����� �������� �����ִ���
    public bool populationSum;

    public Text scoreT; //���� �ؽ�Ʈ
    public Text goldT; //��� �ؽ�Ʈ
    public Text specialgageT; //����� ������ �ؽ�Ʈ(����¼�)
    public Text text4; //����� ������ �ؽ�Ʈ(�����¼�)
    public Text Population; //�α��� ���� �ؽ�Ʈ
    public Text timer; // �÷��� Ÿ�� �ð� ����
    public Text RifleManText; //���� ���� UI
    public Text ScoutText;
    public Text SniperText;
    public Text ArtilleryText;
    public Text HeavyWeaponText;
    public Text ArmouredText;
    public Text TankText;
    public Text HelicopterText;
    public Text RaptorText;

    public List<JHW_UnitManager> hidingUnits;
    public List<JHW_UnitManager> RushUnits;

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
    // public int[] _maxUnit = { 3, 2, 2, 999, 999, 999, 999, 5, 5 }; //�ִ� �α���
    public float[] _cooldown = { 5, 7, 10, 10, 15, 17, 18, 24, 27 }; //���� ��Ÿ��
    public int[] _UnitLoad = { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //���� ���Ϸ�
    public float[] amountExp= { }; //�� ����ġ


    //public void SetMaxUnit(UnitType unitType, int amount)
    //{
    //    unitMaxCount[(int)unitType] = amount;
    //}
    //public int GetMaxUnit(UnitType unitType)
    //{
    //    return unitMaxCount[(int)unitType];
    //}

    private void Start()
    {
        currentPopulationArray = new int[_UnitLoad.Length]; //���� �α��� �迭

        CoolDownReady = new bool[_cooldown.Length]; // ��Ÿ�ӷ������ �迭
        for (int i = 0; i < CoolDownReady.Length; i++)
        {
            CoolDownReady[i] = true;
        }

        currentCool = new float[_cooldown.Length];
        for (int j = 0; j < currentCool.Length; j++) //���� ��Ÿ�� �迭
        {
            currentCool[j] = _cooldown[j];
        }

        amountExp = new float[maxlevel+1];
        amountExp[0] = 100; //�ʱ� ����ġ
        for (int i = 0; i < maxlevel; i++) //����ġ �迭
        {
            amountExp[i + 1] = amountExp[i] * 1.75f;
        }

        //unitMaxCount = new int[JHW_UnitFactory.instance.Units.Length];

        //for (int i = 0; i < unitMaxCount.Length; i++)
        //{
        //    SetMaxUnit((UnitType)i, _maxUnit[i]);
        //}
        //int max = GetMaxUnit(UnitType.Raptor);
    }

    private void Update()
    {
        //print("���� �α��� : " + currentPopulation + " ��ü �α��� : " + wholePopulationLimit);

        if (populationSum == false)
        {
            currentPopulation = 0;
            for (int i = 0; i < currentPopulationArray.Length; i++)
            {
                currentPopulation += currentPopulationArray[i]; //�� �α����� �α��� �迭�� ��� ���� ����
            }
            populationSum = true;
        }


        hidingUnits = JHW_UnitFactory.instance.myUnits;
        RushUnits = JHW_UnitFactory.instance.myUnits;

        //if (currentPopulation < wholePopulationLimit) //�����α��� �ִ� �α����� ������ ���� ���� ���·� �������
        //{
        //    CanProduce = true;
        //}
        //else CanProduce = false; //�ƴϸ� �Ұ���

        //CoolTimer(RifleManCurrentPopulation,0);

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


        Text[] tts = { RifleManText, ScoutText, SniperText, ArtilleryText, HeavyWeaponText, ArmouredText, TankText, HelicopterText, RaptorText };

        string[] CDRText = new string[_cooldown.Length];

        for (int i = 0; i < tts.Length; i++)
        {
            if (CoolDownReady[i]) CDRText[i] = "��Ÿ�� �Ϸ�";
            else CDRText[i] = "��Ÿ�� ��...";

            tts[i].text = ((UnitType)i).ToString() + "\n" + "�α��� : "+currentPopulationArray[i] + "\n" + CDRText[i] + "\n" + currentCool[i].ToString("N1");
        }

    }



    /*���� ���ó*/
    public void OnClickWholePopulationUp() //�ִ� �α��� ���� ��ư
    {
        if (wholePopulationLimit < 100)
        {
            wholePopulationLimit += 5;
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

