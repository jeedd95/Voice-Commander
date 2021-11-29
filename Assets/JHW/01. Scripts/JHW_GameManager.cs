using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //싱글톤

    //public int[] unitMaxCount; // 나온 개별유닛의 총 개수

    /// <summary> 레이어 점수 </summary>
    public int Score = 0;
    public int Gold = 25; //플레이어 골드
    public float specialGauge; //스폐셜 게이지
    public int[] currentPopulationArray; //현재 인구수 배열
    public int currentPopulation; //현재 인구수 = 현재 인구수 배열의 모든 합
    public int wholePopulationLimit; //전체 인구수 제한 (초기4)
    public float playTime; //플레이타임 시간 초
    public float[] currentCool; //유닛별 현재 쿨타임 배열
    public int playerLevel; //플레이어 레벨
    public float currentExp; //현재 경험치
    public int maxlevel = 20;

    public bool[] CoolDownReady; // 유닛 쿨타임이 다 돌았는지
    bool isClickSpecialGauge = false; //스폐셜 게이지를 쓰고있는지
    public bool populationSum;

    public Text scoreT; //점수 텍스트
    public Text goldT; //골드 텍스트
    public Text specialgageT; //스폐셜 게이지 텍스트(방어태세)
    public Text text4; //스폐셜 게이지 텍스트(공격태세)
    public Text Population; //인구수 관련 텍스트
    public Text timer; // 플레이 타임 시간 분초
    public Text RifleManText; //유닛 개별 UI
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
    // public int[] _maxUnit = { 3, 2, 2, 999, 999, 999, 999, 5, 5 }; //최대 인구수
    public float[] _cooldown = { 5, 7, 10, 10, 15, 17, 18, 24, 27 }; //고정 쿨타임
    public int[] _UnitLoad = { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //유닛 부하량
    public float[] amountExp= { }; //총 경험치


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

        amountExp = new float[maxlevel+1];
        amountExp[0] = 100; //초기 경험치
        for (int i = 0; i < maxlevel; i++) //경험치 배열
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
        //print("현재 인구수 : " + currentPopulation + " 전체 인구수 : " + wholePopulationLimit);

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
            Gold += 5;
            currentTime2 = 0;
        }
    }

    void PlusSpecialGauge() //몇 초마다 게이지 올려주는 코드
    {
        currentTime3 += Time.deltaTime;
        if (currentTime3 > specialEarnTime)
        {
            specialGauge += 0.05f;
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
        scoreT.text = "플레이어 점수 : " + Score;
        goldT.text = "골드 : " + Gold;
        specialgageT.text = string.Format("{0,3:N0}", specialGauge) + " %";
        text4.text = specialgageT.text;
        Population.text = currentPopulation + " / " + wholePopulationLimit;


        Text[] tts = { RifleManText, ScoutText, SniperText, ArtilleryText, HeavyWeaponText, ArmouredText, TankText, HelicopterText, RaptorText };

        string[] CDRText = new string[_cooldown.Length];

        for (int i = 0; i < tts.Length; i++)
        {
            if (CoolDownReady[i]) CDRText[i] = "쿨타임 완료";
            else CDRText[i] = "쿨타임 중...";

            tts[i].text = ((UnitType)i).ToString() + "\n" + "인구수 : "+currentPopulationArray[i] + "\n" + CDRText[i] + "\n" + currentCool[i].ToString("N1");
        }

    }



    /*훈장 사용처*/
    public void OnClickWholePopulationUp() //최대 인구수 증가 버튼
    {
        if (wholePopulationLimit < 100)
        {
            wholePopulationLimit += 5;
        }
        else
        {
            print("더 이상 최대 인구를 늘릴 수 없습니다");
            return;
        }
    }

    void ChangePosture(JHW_UnitManager.State state) //하이어라키에있는 모든 Player 유닛을 검색하고 상태변경
    {
        List<JHW_UnitManager> playerUnits = JHW_UnitFactory.instance.myUnits;

        for (int i = 0; i < playerUnits.Count; i++)
        {
            playerUnits[i].SetState(state);
        }

    }

}

