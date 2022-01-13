using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //싱글톤
    public Slider slider;
    Animation anim;

    //public int[] unitMaxCount; // 나온 개별유닛의 총 개수

    /// <summary> 레이어 점수 </summary>
    public int Score = 0;
    public int Gold = 25; //플레이어 시작골드
    public int GoldRate = 60; //상승 되는 골드
    public float specialGauge; //스폐셜 게이지
    public int[] currentPopulationArray; //현재 인구수 배열
    public int currentPopulation; //현재 인구수 = 현재 인구수 배열의 모든 합
    public int wholePopulationLimit; //전체 인구수 제한 (초기4)
    public float playTime; //플레이타임 시간 전체 second
    int _Min; //플레이타임 시간 분으로 환산
    public float WholePlayTime;
    public float[] currentCool; //유닛별 현재 쿨타임 배열
    public int playerLevel; //플레이어 레벨
    public float currentExp; //현재 경험치
    public float amountExp; //총 경험치
    public int maxlevel = 9999999; //만렙
    public int medal;
    public GameObject[] CaptureAreaType; //주둔지역 타입 Gold, Cooldown,Special Gauge
    public GameObject CaptureArea;
    public float windPower;
    public GameObject PlayerSkill_Bomb_prefabs;
    public GameObject PlayerSkill_Smoke_prefabs;
    public GameObject TeamCommand;
    JHW_Command command;


    public bool[] CoolDownReady; // 유닛 쿨타임이 다 돌았는지
    bool isClickSpecialGauge = false; //스폐셜 게이지를 쓰고있는지
    public bool populationSum;
    public bool isBuff_Gold;
    public bool isBuff_CoolDown;
    public bool isBuff_SpecialGauge;
    public bool isCaptureCreateMode;
    public bool Flag_wind;
    public bool isPlayerSkillMode;


    public Text scoreT; //점수 텍스트
    public Text goldT; //골드 텍스트
    public Text ExpT;
    public Text HpT;
    public Text specialgageT; //스폐셜 게이지 텍스트(방어태세)
    public Text text4; //스폐셜 게이지 텍스트(공격태세)
    public Text Population; //인구수 관련 텍스트
    public Text timer; // 플레이 타임 시간 분초
    public Text Bomb_CoolT;
    public Text Smoke_CoolT;

    //=========유닛 개별 UI
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
    // List<GameObject> NowCaptureAreasList;  //현재 맵에 나와있는 주둔지역 리스트

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

    // public int[] _maxUnit = { 3, 2, 2, 999, 999, 999, 999, 5, 5 }; //최대 인구수
    public float[] _cooldown = { 5, 7, 10, 10, 15, 17, 18, 24, 27 }; //고정 쿨타임
    public int[] _UnitLoad = { 1, 1, 2, 2, 3, 4, 6, 7, 7 }; //유닛 부하량
    public float[] amountExpArray= { }; //총 경험치


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

            currentPopulationArray = new int[_UnitLoad.Length]; //현재 인구수 배열

            CoolDownReady = new bool[_cooldown.Length]; // 쿨타임레디상태 배열
            for (int i = 0; i < CoolDownReady.Length; i++)
            {
                CoolDownReady[i] = true;
            }

            currentCool = new float[_cooldown.Length];
            for (int j = 0; j < currentCool.Length; j++) //현재 쿨타임 배열
            {
                currentCool[j] = _cooldown[j];
            }

            amountExpArray = new float[maxlevel + 1];
            amountExpArray[0] = 100; //초기 경험치
            amountExp = amountExpArray[0];
            for (int i = 0; i < maxlevel; i++) //경험치 배열
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

        //print("현재 인구수 : " + currentPopulation + " 전체 인구수 : " + wholePopulationLimit);
        // print("isCaptureCreateMode : " + isCaptureCreateMode);

        slider.value = windPower; //바람의 세기를 슬라이더로 표현


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
                currentPopulation += currentPopulationArray[i]; //총 인구수에 인구수 배열의 모든 합을 담음
            }
            populationSum = true;
        }

        hidingUnits = JHW_UnitFactory.instance.myUnits;
        RushUnits = JHW_UnitFactory.instance.myUnits;

        //if (currentPopulation < wholePopulationLimit) //현재인구가 최대 인구보다 작을때 생산 가능 상태로 만들어줌
        //{
        //    CanProduce = true;
        //}
        //else CanProduce = false; //아니면 불가능

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

    float scoreEarnTime = 1; //1초마다 점수
    float goldEarnTime = 3; //3초마다 골드
    float specialEarnTime = 0.1f; // 2 초마다 게이지 업

    /// <summary>
    /// 몇 초마다 점수 주는 코드
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
    /// 몇 초마다 골드 주는 코드
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

    void PlusSpecialGauge() //몇 초마다 게이지 올려주는 코드
    {
        currentTime3 += Time.deltaTime;
        if (currentTime3 > specialEarnTime)
        {
            if (!isBuff_SpecialGauge) specialGauge += 0.05f;
            else specialGauge += 0.5f;
            currentTime3 = 0;
        }
    }

    public void OnClickDefense() //방어태세 버튼클릭이벤트
    {
        if (isClickSpecialGauge == false && specialGauge > 0)
        {
            isClickSpecialGauge = true;
            ChangePosture(JHW_UnitManager.State.Hide);
        }

    }
    public void NotClickDefense() //방어태세를 뗄 때 골랐던 유닛들을 Move상태로
    {
        if (isClickSpecialGauge == true)
        {
            isClickSpecialGauge = false;
            ChangePosture(JHW_UnitManager.State.Move);
        }
    }

    public void OnClickOffense() //공격태세 버튼클릭이벤트
    {
        if (isClickSpecialGauge == false)
        {
            isClickSpecialGauge = true;
            print("공격태세 온");
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
            print("공격태세 오프");

            for (int i = 0; i < RushUnits.Count; i++)
            {
                RushUnits[i].unitinfo.UseOffensive = false;
            }
        }
    }

    void Timer() //플레이타임 기록
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
        if (isClickSpecialGauge && specialGauge >= 0) // 스폐셜 게이지를 누르고 있을때 계속 호출  
        {
            specialGauge -= 0.01f;
        }

        if (specialGauge < 0) //스폐셜 게이지가 0밑으로 가면 모든 상태를 move로 바꾸고 Useoffensive도 끈다
        {
            print("스폐셜 게이지가 부족합니다");

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

        if (isClickSpecialGauge == false && specialGauge <= 100) //스폐셜 게이지를 누르고 있지않을때만 계속 호출
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
            if (CoolDownReady[i]) CDRText[i] = "쿨타임 완료";
            else CDRText[i] = "쿨타임 중...";

            if(!isBuff_CoolDown) 
            {
                tts[i].text = /*((UnitType)i).ToString() + "\n" + "인구수 : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n" + */currentCool[i].ToString("N1");
                if (currentCool[i] <=0)
                {
                    // tts[i].text = ((UnitType)i).ToString() + "\n" + "인구수 : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n";
                    tts[i].text = "";
                }
            }
            else 
            {
                tts[i].text = /*((UnitType)i).ToString() + "\n" + "인구수 : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n" + */(currentCool[i] * 0.75f).ToString("N1");
                if (currentCool[i] <= 0)
                {
                    //tts[i].text = ((UnitType)i).ToString() + "\n" + "인구수 : " + currentPopulationArray[i] + "\n" + CDRText[i] + "\n";
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

    void ChangePosture(JHW_UnitManager.State state) //하이어라키에있는 모든 Player 유닛을 검색하고 상태변경
    {
        List<JHW_UnitManager> playerUnits = JHW_UnitFactory.instance.myUnits;

        for (int i = 0; i < playerUnits.Count; i++)
        {
            if(playerUnits[i].GetComponent<JHW_UnitInfo>().isCaptureUnit==false && playerUnits[i].GetComponent<JHW_UnitInfo>().isAirForce == false) // 점령중인 유닛은 스폐셜게이지가 안먹게함 ,공중유닛은 스폐셜 게이지가 안먹게함
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
            float temp  = currentExp - amountExp; //초과량 이관 시켜주기
            currentExp = 0;
            currentExp += temp;
        }
    }

    //훈장 사용처 1.인구수 증가, 2. 돈 획득, 2. 돈의 획득량 증가
    public void OnClickWholePopulationUp() //최대 인구수 증가 버튼
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
                print("더 이상 최대 인구를 늘릴 수 없습니다");
            }
        }
        else
        {
            print("훈장이 부족합니다");
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
            print("훈장이 부족합니다");
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
                print("더 이상 비율을 늘릴 수 없습니다");
            }
        }
        else
        {
            print("훈장이 부족합니다");
        }
    }
    public float CaptureInstantiateTime ; //주둔지 생성 시간
    public float RemainTime ; //주둔지 유지 시간
    float currentTime4;

    [SerializeField]
    List<Transform> allTiles;

    void InstantiateCaptureArea()
    {
        currentTime4 += Time.deltaTime;
       //주둔지(3개중 랜덤)를 시작한지 10초 후 생성하고 이후에는 2분마다 생성하고 싶다
        if(currentTime4 > CaptureInstantiateTime)
        {
            //Tiles의 자식들을 배열에다가 넣음
            //그 배열중에 랜덤으로 뽑아다가
            //뽑은 타일위 조금 떨어진 곳에 생성

           

            int rand = Random.Range(0, 31);
            Transform FinalTile = allTiles[rand];

            GameObject RandomCaptureType = CaptureAreaType[Random.Range(0, CaptureAreaType.Length)];  // 세개중에 하나
            CaptureArea = Instantiate(RandomCaptureType);
            CaptureArea.transform.rotation = FinalTile.rotation;
            CaptureArea.transform.position = new Vector3(FinalTile.position.x, FinalTile.position.y + 0.6f, FinalTile.position.z);


            //=========================================
            //랜덤 위치를 만들자
            //float xRange = Random.Range(-40, 40);
            //float zRange = Random.Range(-20, 20);
            //Vector3 RandomCapturePos = GameObject.Find("Tiles").transform.position + new Vector3(xRange,0,zRange);
            ////new Vector3(-258.1f+xRange, 0, -234.7f+zRange);

            ////주둔지 뭘 만들지 랜덤으로 정하기
            //GameObject RandomCaptureType = CaptureAreaType[Random.Range(0, CaptureAreaType.Length)];
            //CaptureArea = Instantiate(RandomCaptureType);
            //CaptureArea.transform.position = RandomCapturePos;
            //CaptureArea.transform.rotation = GameObject.Find("Tiles").transform.rotation;
            ////리스트에 넣기
            ////NowCaptureAreasList.Add(CaptureArea);

            currentTime4 = 0;
        }

    }

    public void ToggleCaptureMode() //버튼 클릭으로 점령유닛을 생성할 수 있음
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
            print("플레이어 스킬 _ 폭격");
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
            print("플레이어 스킬 _ 연막");
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
        //스킬
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
    ////처음 0에서 시작 해서 시간에 따라 60까지 증가
    ////60에 도달하면 더이상 늘어나지 않고 사용하면 다시 0으로 초기화
    
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

