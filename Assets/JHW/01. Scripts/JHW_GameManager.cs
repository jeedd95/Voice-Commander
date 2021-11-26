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
    public bool[] CoolDownReady; // 유닛 쿨타임이 다 돌았는지

    public Text scoreT; //점수 텍스트
    public Text goldT; //골드 텍스트
    public Text specialgageT; //스폐셜 게이지 텍스트(방어태세)
    public Text text4; //스폐셜 게이지 텍스트(공격태세)
    public Text Population; //인구수 관련 텍스트
    public Text timer; // 플레이 타임 시간 분초

    /* 유닛 개별 UI ============================*/
    public Text RifleManText; //라이플맨의 텍스트
    public Text ScoutText;
    public Text SniperText;
    public Text ArtilleryText;
    public Text HeavyWeaponText;
    public Text ArmouredText;
    public Text TankText;
    public Text HelicopterText;
    public Text RaptorText;

    //public int RifleManCurrentPopulation; //라이플맨의 현재인구
    //public int ScoutCurrentPopulation;
    //public int SniperCurrentPopulation;
    //public int ArtilleryCurrentPopulation;
    //public int HeavyWeaponCurrentPopulation;
    //public int ArmouredCurrentPopulation;
    //public int TankCurrentPopulation;
    //public int HelicopterCurrentPopulation;
    //public int RaptorCurrentPopulation;
    /*=====================================*/

    public List<JHW_UnitManager> hidingUnits;
    public List<JHW_UnitManager> RushUnits;

    // bool ishiding; //벽뒤에 숨었다
    bool isClickSpecialGauge = false; //스폐셜 게이지를 쓰고있는지
    //public bool CanProduce; // 전체 인구가 생산 할 수 있는지
    //public bool CanProduce_Individual; //유닛별 개인 인구가 생산 할 수 있는지

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
   // public int[] _maxUnit = { 3, 2, 2, 999, 999, 999, 999, 5, 5 }; //최대 인구수
    float[] _cooldown = {5,10,12,20,30,40,50,60,70 }; //고정 쿨타임
    public int[] _UnitLoad = { 1, 2, 3, 4, 5, 6, 7, 8, 9 }; //유닛 부하량
    

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
        currentPopulationArray = new int[_UnitLoad.Length]; //현재 인구수 배열 생성

        CoolDownReady = new bool[_cooldown.Length];
        for (int i = 0; i < CoolDownReady.Length; i++)
        {
            CoolDownReady[i] = true;
        }

        currentCool = new float[_cooldown.Length];
        for (int j = 0; j < currentCool.Length; j++) //현재 쿨타임 배열에 각자 유닛 쿨타임 수치를 담음
        {
            currentCool[j] = _cooldown[j];
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
        print("현재 인구수 : " + currentPopulation + "전체 인구수 : " + wholePopulationLimit);

        for (int i = 0; i < currentPopulationArray.Length; i++)
        {
            currentPopulation += currentPopulationArray[i]; //총 인구수에 인구수 배열의 모든 합을 담음
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


   public void CoolTimer(int population, int index) 
    {


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

        //RifleManText.text ="RifleMan\n" + RifleManCurrentPopulation + " / " + "\n" + currentCool[0].ToString("N1");
        //ScoutText.text =  "Scout\n" + ScoutCurrentPopulation + " / " +  "\n" + currentCool[1].ToString("N1");
        //SniperText.text = "Sniper\n" + SniperCurrentPopulation + " / " +  "\n" + currentCool[2].ToString("N1");
        //ArtilleryText.text = "Artillery\n" + ArtilleryCurrentPopulation + " / " +  "\n" + currentCool[3].ToString("N1");
        //HeavyWeaponText.text = "HeavyWeapon\n" + HeavyWeaponCurrentPopulation + " / " +  "\n" + currentCool[4].ToString("N1");
        //ArmouredText.text = "Armoured\n" + ArmouredCurrentPopulation + " / " +  "\n" + currentCool[5].ToString("N1");
        //TankText.text = "Tank\n" + TankCurrentPopulation + " / " +  "\n" + currentCool[6].ToString("N1");
        //HelicopterText.text = "Helicopter\n" + HelicopterCurrentPopulation + " / " +  "\n" + currentCool[7].ToString("N1");
        //RaptorText.text = "Raptor\n" + RaptorCurrentPopulation + " / " +  "\n" + currentCool[8].ToString("N1");

    }

    

    /*훈장 사용처*/
    public void OnClickWholePopulationUp() //최대 인구수 증가 버튼
    {
        if (wholePopulationLimit < 100)
        {
            wholePopulationLimit+=5;
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

