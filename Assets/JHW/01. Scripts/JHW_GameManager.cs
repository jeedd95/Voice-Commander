using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���
    public Slider slider;
    Animation anim;

    //public int[] unitMaxCount; // ���� ���������� �� ����

    /// <summary> ���̾� ���� </summary>
    public int Score = 0;
    public int Gold = 25; //�÷��̾� ���۰��
    public int GoldRate = 60; //��� �Ǵ� ���
    public float specialGauge; //����� ������
    public int[] currentPopulationArray; //���� �α��� �迭
    public int currentPopulation; //���� �α��� = ���� �α��� �迭�� ��� ��
    public int wholePopulationLimit; //��ü �α��� ���� (�ʱ�4)
    public float playTime; //�÷���Ÿ�� �ð� ��ü second
    int _Min; //�÷���Ÿ�� �ð� ������ ȯ��
    public float WholePlayTime;
    public float[] currentCool; //���ֺ� ���� ��Ÿ�� �迭
    public int playerLevel; //�÷��̾� ����
    public float currentExp; //���� ����ġ
    public float amountExp; //�� ����ġ
    public int maxlevel = 9999999; //����
    public int medal;
    public GameObject[] CaptureAreaType; //�ֵ����� Ÿ�� Gold, Cooldown,Special Gauge
    public GameObject CaptureArea;
    public float windPower;
    public GameObject PlayerSkill_Bomb_prefabs;
    public GameObject PlayerSkill_Smoke_prefabs;
    public GameObject TeamCommand;
    JHW_Command command;


    public bool[] CoolDownReady; // ���� ��Ÿ���� �� ���Ҵ���
    bool isClickSpecialGauge = false; //����� �������� �����ִ���
    public bool populationSum;
    public bool isBuff_Gold;
    public bool isBuff_CoolDown;
    public bool isBuff_SpecialGauge;
    public bool isCaptureCreateMode;
    public bool Flag_wind;
    public bool isPlayerSkillMode;


    public Text scoreT; //���� �ؽ�Ʈ
    public Text goldT; //��� �ؽ�Ʈ
    public Text ExpT;
    public Text HpT;
    public Text specialgageT; //����� ������ �ؽ�Ʈ(����¼�)
    public Text text4; //����� ������ �ؽ�Ʈ(�����¼�)
    public Text Population; //�α��� ���� �ؽ�Ʈ
    public Text timer; // �÷��� Ÿ�� �ð� ����
    public Text Bomb_CoolT;
    public Text Smoke_CoolT;

    //=========���� ���� UI
    public Text RifleManText; 
    public Text ScoutText;
    public Text SniperText;
    public Text ArtilleryText;
    public Text HeavyWeaponText;
    public Text ArmouredText;
    public Text TankText;
    public Text HelicopterText;
    public Text RaptorText;
    public Image RifleManPortrait;
    public Image ScoutPortrait;
    public Image SniperPortrait;
    public Image ArtilleryPortrait;
    public Image HeavyWeaponPortrait;
    public Image ArmouredPortrait;
    public Image TankPortrait;
    public Image HelicopterPortrait;
    public Image RaptorPortrait;
    //===================

    public Text levelText;
    public Text MedalText;
    public Text GoldRateUpText;
    public Text WindText;

    public Image BuffGold;
    public Image BuffCool;
    public Image BuffSpecial;
    public Image PauseMsgBox;

    public List<JHW_UnitManager> hidingUnits;
    public List<JHW_UnitManager> RushUnits;
    // List<GameObject> NowCaptureAreasList;  //���� �ʿ� �����ִ� �ֵ����� ����Ʈ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
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
    public int[] _UnitLoad = { 1, 1, 2, 2, 3, 4, 6, 7, 7 }; //���� ���Ϸ�
    public float[] amountExpArray= { }; //�� ����ġ


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
        for (int i = 0; i < 32; i++)
        {
            allTiles.Add(GameObject.Find("Tiles").transform.GetChild(i));
        }

        windPower = 50.0f;

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

            amountExpArray = new float[maxlevel + 1];
            amountExpArray[0] = 100; //�ʱ� ����ġ
            amountExp = amountExpArray[0];
            for (int i = 0; i < maxlevel; i++) //����ġ �迭
            {
                amountExpArray[i + 1] = amountExpArray[i] * 1.125f;
            }

        command = TeamCommand.GetComponent<JHW_Command>();

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
        // print("isCaptureCreateMode : " + isCaptureCreateMode);

        slider.value = windPower; //�ٶ��� ���⸦ �����̴��� ǥ��


        if (!Flag_wind)
        {
            Flag_wind = true;
            StartCoroutine("RandomWind");
        }



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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMsgBox.gameObject.activeSelf == false) OnClickPause();
            else OnClickPauseDis();
        }

        SpecialGageManager();
        PlusScore();
        PlusGold();
        Timer();
        TextManager();
        LevelUp();
        InstantiateCaptureArea();
        WindTextMove();
        SpecialGageFilled();
        EXPGageFilled();

        //PlayerSkill_Bomb();
        //PlayerSkill_Smoke();

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
            if (isBuff_Gold) Gold += 50;
            else Gold += 25;

            currentTime2 = 0;
        }
    }

    void PlusSpecialGauge() //�� �ʸ��� ������ �÷��ִ� �ڵ�
    {
        currentTime3 += Time.deltaTime;
        if (currentTime3 > specialEarnTime)
        {
            if (!isBuff_SpecialGauge) specialGauge += 0.05f;
            else specialGauge += 0.5f;
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
        WholePlayTime += Time.deltaTime;

        timer.text = string.Format("{0:D2} : {1:D2}",_Min,(int)playTime);
        if((int)playTime > 59)
        {
            playTime = 0;
            _Min++;
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
        scoreT.text = "SCORE :  " + Score;
        goldT.text = Gold.ToString();
        specialgageT.text = string.Format("{0,3:N0}", specialGauge) + "%";
        text4.text = specialgageT.text;
        Population.text = currentPopulation + " / " + wholePopulationLimit;
        levelText.text = "LEVEL : " + playerLevel;
        MedalText.text = medal.ToString();
        GoldRateUpText.text = GoldRate.ToString();
        ExpT.text = currentExp + " / " + amountExp;
        HpT.text = command.Hp.ToString() + " / " + command.OriginHp.ToString();
        Bomb_CoolT.text = PlayerSkill_BombCurrentCool.ToString("N0");
        if (PlayerSkill_BombCurrentCool <=0)
        {
            Bomb_CoolT.text = "";
        }
        Smoke_CoolT.text = PlayerSkill_SmokeCurrentCool.ToString("N0");
        if (PlayerSkill_SmokeCurrentCool <= 0)
        {
            Smoke_CoolT.text = "";
        }

        BuffGold.color = isBuff_Gold ? Color.yellow: Color.white;
        BuffCool.color = isBuff_CoolDown ? Color.blue : Color.white;
        BuffSpecial.color = isBuff_SpecialGauge ? Color.green :Color.white;


        Text[] tts = { RifleManText, ScoutText, SniperText, ArtilleryText, HeavyWeaponText, ArmouredText, TankText, HelicopterText, RaptorText };
        Image[] uts = { RifleManPortrait, ScoutPortrait, SniperPortrait, ArtilleryPortrait, HeavyWeaponPortrait, ArmouredPortrait, TankPortrait, HelicopterPortrait, RaptorPortrait };

        string[] CDRText = new string[_cooldown.Length];

        for (int i = 0; i < tts.Length; i++)
        {
            if (CoolDownReady[i]) CDRText[i] = "��Ÿ�� �Ϸ�";
            else CDRText[i] = "��Ÿ�� ��...";

            if(!isBuff_CoolDown) 
            {
                tts[i].text = /*((UnitType)i).ToString() + "\n" + "�α��� : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n" + */currentCool[i].ToString("N1");
                if (currentCool[i] <=0)
                {
                    // tts[i].text = ((UnitType)i).ToString() + "\n" + "�α��� : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n";
                    tts[i].text = "";
                }
            }
            else 
            {
                tts[i].text = /*((UnitType)i).ToString() + "\n" + "�α��� : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n" + */(currentCool[i] * 0.75f).ToString("N1");
                if (currentCool[i] <= 0)
                {
                    //tts[i].text = ((UnitType)i).ToString() + "\n" + "�α��� : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n";
                    tts[i].text = "";
                }
            }
            if(_cooldown[i] == currentCool[i])
            {
                tts[i].text = "";
            }

            if(_cooldown[i] - currentCool[i] != 0)
            {
            uts[i].GetComponent<Image>().fillAmount =  (_cooldown[i]-currentCool[i])/_cooldown[i];
            }

        }
    }

    void ChangePosture(JHW_UnitManager.State state) //���̾��Ű���ִ� ��� Player ������ �˻��ϰ� ���º���
    {
        List<JHW_UnitManager> playerUnits = JHW_UnitFactory.instance.myUnits;

        for (int i = 0; i < playerUnits.Count; i++)
        {
            if(playerUnits[i].GetComponent<JHW_UnitInfo>().isCaptureUnit==false && playerUnits[i].GetComponent<JHW_UnitInfo>().isAirForce == false) // �������� ������ ����Ȱ������� �ȸ԰��� ,���������� ����� �������� �ȸ԰���
            {
                playerUnits[i].SetState(state);
            }
        }

    }

    void LevelUp()
    {
        amountExp = amountExpArray[playerLevel - 1];

        if (currentExp >= amountExp)
        {
            playerLevel++;
            medal++;
            float temp  = currentExp - amountExp; //�ʰ��� �̰� �����ֱ�
            currentExp = 0;
            currentExp += temp;
        }
    }

    //���� ���ó 1.�α��� ����, 2. �� ȹ��, 2. ���� ȹ�淮 ����
    public void OnClickWholePopulationUp() //�ִ� �α��� ���� ��ư
    {
        if(medal >=1)
        {
            if (wholePopulationLimit < 100)
            {
                medal--;
                wholePopulationLimit += 7;
            }
            else
            {
                print("�� �̻� �ִ� �α��� �ø� �� �����ϴ�");
            }
        }
        else
        {
            print("������ �����մϴ�");
        }
    }
    public void OnClickGetGold()
    {
        if (medal >= 1)
        {
            medal--;
            Gold += GoldRate;   
        }
        else
        {
            print("������ �����մϴ�");
        }
    }


    public void OnClickGetGoldRateUP()
    {
        if (medal >= 1)
        {
            if(GoldRate <=260)
            {
                medal--;
                GoldRate += 25;
            }
            else
            {
                print("�� �̻� ������ �ø� �� �����ϴ�");
            }
        }
        else
        {
            print("������ �����մϴ�");
        }
    }
    public float CaptureInstantiateTime ; //�ֵ��� ���� �ð�
    public float RemainTime ; //�ֵ��� ���� �ð�
    float currentTime4;

    [SerializeField]
    List<Transform> allTiles;

    void InstantiateCaptureArea()
    {
        currentTime4 += Time.deltaTime;
       //�ֵ���(3���� ����)�� �������� 10�� �� �����ϰ� ���Ŀ��� 2�и��� �����ϰ� �ʹ�
        if(currentTime4 > CaptureInstantiateTime)
        {
            //Tiles�� �ڽĵ��� �迭���ٰ� ����
            //�� �迭�߿� �������� �̾ƴٰ�
            //���� Ÿ���� ���� ������ ���� ����

           

            int rand = Random.Range(0, 31);
            Transform FinalTile = allTiles[rand];

            GameObject RandomCaptureType = CaptureAreaType[Random.Range(0, CaptureAreaType.Length)];  // �����߿� �ϳ�
            CaptureArea = Instantiate(RandomCaptureType);
            CaptureArea.transform.rotation = FinalTile.rotation;
            CaptureArea.transform.position = new Vector3(FinalTile.position.x, FinalTile.position.y + 0.6f, FinalTile.position.z);


            //=========================================
            //���� ��ġ�� ������
            //float xRange = Random.Range(-40, 40);
            //float zRange = Random.Range(-20, 20);
            //Vector3 RandomCapturePos = GameObject.Find("Tiles").transform.position + new Vector3(xRange,0,zRange);
            ////new Vector3(-258.1f+xRange, 0, -234.7f+zRange);

            ////�ֵ��� �� ������ �������� ���ϱ�
            //GameObject RandomCaptureType = CaptureAreaType[Random.Range(0, CaptureAreaType.Length)];
            //CaptureArea = Instantiate(RandomCaptureType);
            //CaptureArea.transform.position = RandomCapturePos;
            //CaptureArea.transform.rotation = GameObject.Find("Tiles").transform.rotation;
            ////����Ʈ�� �ֱ�
            ////NowCaptureAreasList.Add(CaptureArea);

            currentTime4 = 0;
        }

    }

    public void ToggleCaptureMode() //��ư Ŭ������ ���������� ������ �� ����
    {
        isCaptureCreateMode = !isCaptureCreateMode;
    }

    public void TogglePlayerSkillMode()
    {
        isPlayerSkillMode = !isPlayerSkillMode;
    }

    IEnumerator RandomWind()
    {
        yield return new WaitForSeconds(5);
        windPower += Random.Range(-10.0f, 10.0f);
        if (windPower <= 0) windPower = 0;
        if (windPower >= 100) windPower = 100;
        Flag_wind = false;
    }

    public bool isWindOn;

    void WindTextMove()
    {
        if(windPower<50)
        {
            if(isWindOn==false)
            {
                isWindOn = true;
                StartCoroutine(WindTextAnim("<"));

            }
        }
        else if (windPower ==50)
        {
            WindText.text = "-";
        }
        else
        {
            if (isWindOn == false)
            {
                isWindOn = true;
                StartCoroutine(WindTextAnim(">"));
            }
        }
    }

    IEnumerator WindTextAnim(string dir)
    {
        WindText.text = dir + "\t"+(-50+windPower).ToString("F1");
        yield return new WaitForSeconds(1);
        WindText.text = dir + "\t" + dir + "\t"+(-50 + windPower).ToString("F1");
        yield return new WaitForSeconds(1);
        isWindOn = false;
    }

    float PlayerSkill_BombCoolTime = 60f;
    float PlayerSkill_BombCurrentCool=0f;
    bool PlayerSkill_Bomb_IsReady=true;

    float PlayerSkill_SmokeCoolTime = 45f;
    float PlayerSkill_SmokeCurrentCool=0f;
    bool PlayerSkill_Smoke_IsReady=true;

    public void PlayerSkill_Bomb()
    {
        if(/*isPlayerSkillMode && */JHW_OrderManager.instance.DesinationAreaObj !=null && PlayerSkill_Bomb_IsReady /*&& Input.GetKeyDown(KeyCode.Z)*/)
        {
            print("�÷��̾� ��ų _ ����");
            PlayerSkill_Bomb_IsReady = false;
            //isPlayerSkillMode = false;
            //GameObject.Find("MainCanvas/PlayerSkillMode").GetComponent<Toggle>().isOn = false;
            //isPlayerSkillMode = true;
            GameObject PS_B = Instantiate(PlayerSkill_Bomb_prefabs);
            PS_B.transform.rotation = GameObject.Find("Tiles").transform.rotation;
            PS_B.transform.position = JHW_OrderManager.instance.DesinationAreaObj.transform.position;
            anim = PS_B.GetComponent<Animation>();
            anim.Play();
            if(anim.isPlaying==false)
            {
                Destroy(PS_B);
            }
            PlayerSkill_BombCurrentCool = PlayerSkill_BombCoolTime;
            StartCoroutine("CD_Bomb");
        }
       
    }
    public void PlayerSkill_Smoke()
    {
        if (/*isPlayerSkillMode &&*/ JHW_OrderManager.instance.DesinationAreaObj != null && PlayerSkill_Smoke_IsReady/*&& Input.GetKeyDown(KeyCode.X)*/)
        {
            print("�÷��̾� ��ų _ ����");
            PlayerSkill_Smoke_IsReady = false;
            //isPlayerSkillMode = false;
            //GameObject.Find("MainCanvas/PlayerSkillMode").GetComponent<Toggle>().isOn = false;
            GameObject PS_S = Instantiate(PlayerSkill_Smoke_prefabs);
            PS_S.transform.rotation = GameObject.Find("Tiles").transform.rotation;
            PS_S.transform.position = JHW_OrderManager.instance.DesinationAreaObj.transform.position;
            anim = PS_S.GetComponent<Animation>();
            anim.Play();
            if (anim.isPlaying == false)
            {
                Destroy(PS_S);
            }
            PlayerSkill_SmokeCurrentCool = PlayerSkill_SmokeCoolTime;
            StartCoroutine("CD_Smoke");
        }
    }

    IEnumerator CD_Bomb()
    {
        //PlayerSkill_Bomb_IsReady = false;

        while(PlayerSkill_BombCurrentCool>0)
        {
            PlayerSkill_BombCurrentCool -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        PlayerSkill_Bomb_IsReady = true;
    }

    IEnumerator CD_Smoke()
    {
        //PlayerSkill_Smoke_IsReady = false;

        while (PlayerSkill_SmokeCurrentCool > 0)
        {
            PlayerSkill_SmokeCurrentCool -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        PlayerSkill_Smoke_IsReady = true;
    }

    void SpecialGageFilled()
    {
        GameObject.Find("Defensive").GetComponent<Image>().fillAmount = specialGauge / 100;
        GameObject.Find("Offensive").GetComponent<Image>().fillAmount = specialGauge / 100;
        //��ų
        GameObject.Find("skill_bomb").GetComponent<Image>().fillAmount = (PlayerSkill_BombCoolTime-PlayerSkill_BombCurrentCool) / PlayerSkill_BombCoolTime;
        GameObject.Find("skill_smokeIcon").GetComponent<Image>().fillAmount = (PlayerSkill_SmokeCoolTime - PlayerSkill_SmokeCurrentCool) / PlayerSkill_SmokeCoolTime;
        GameObject.Find("skill_smokeIcon2").GetComponent<Image>().fillAmount = (PlayerSkill_SmokeCoolTime - PlayerSkill_SmokeCurrentCool) / PlayerSkill_SmokeCoolTime;
    }

    void EXPGageFilled()
    {
        GameObject.Find("EXP").GetComponent<Slider>().value = currentExp/amountExp*100f;
        GameObject.Find("Commandhp").GetComponent<Slider>().value = command.Hp / command.OriginHp;
    }
    //[SerializeField]
    //bool isSkill_Bomb_Ready;
    //bool isSkill_Smoke_Ready;
    //[SerializeField]
    //float Skill_Bomb_CoolTime = 0;
    //float Skill_Smoke_CoolTime= 0;
    ////ó�� 0���� ���� �ؼ� �ð��� ���� 60���� ����
    ////60�� �����ϸ� ���̻� �þ�� �ʰ� ����ϸ� �ٽ� 0���� �ʱ�ȭ
    
    //public void skill_Bomb_Cool()
    //{
    //    if(!isSkill_Bomb_Ready)
    //    {
    //        StartCoroutine("CoolDown");
    //    }
    //}
    //IEnumerator CoolDown()
    //{
    //    isSkill_Bomb_Ready = false;

    //    while (Skill_Bomb_CoolTime <= 60)
    //    {
    //        Skill_Bomb_CoolTime += 0.01f;
    //        yield return new WaitForSeconds(0.01f);
    //    }

    //    Skill_Bomb_CoolTime = 60;
    //    isSkill_Bomb_Ready = true;
    //}

    public void OnClickPause()
    {
        

            PauseMsgBox.gameObject.SetActive(true);
            Time.timeScale = 0;
    }
    public void OnClickPauseDis()
    {
        PauseMsgBox.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}

