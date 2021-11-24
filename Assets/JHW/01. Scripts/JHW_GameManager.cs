using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //싱글톤

    JHW_UnitInfo[] unitInfo;

    public int Score = 0; //플레이어 점수
    public int Gold = 25; //플레이어 골드
    public float specialGauge ; //스폐셜 게이지
    public int currentPopulation; //현재 인구수
    public int wholePopulationLimit; //전체 인구수 제한 (각 유닛별x) (초기2 ~ 최대 33까지)
    public float playTime; //플레이타임 시간 초 

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

    public int RifleManCurrentPopulation; //라이플맨의 현재인구
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

   // bool ishiding; //벽뒤에 숨었다
    bool isClickSpecialGauge = false; //스폐셜 게이지를 쓰고있는지
    public bool CanProduce_Whole; // 전체 인구가 생산 할 수 있는지
    public bool CanProduce_Individual; //유닛별 개인 인구가 생산 할 수 있는지

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
        }  //==================== 잘못된 점 찾기
    }

    private void Update()
    {
            hidingUnits = JHW_UnitFactory.instance.myUnits; 
            RushUnits = JHW_UnitFactory.instance.myUnits;

        currentPopulation = JHW_UnitFactory.instance.myUnits.Count; // 현재 나와있는 인구는 내 유닛들의 갯수

        if (currentPopulation < wholePopulationLimit) //현재인구가 최대 인구보다 작을때 생산 가능 상태로 만들어줌
        {
            CanProduce_Whole = true;
        }
        else CanProduce_Whole = false; //아니면 불가능

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

    float scoreEarnTime = 1; //1초마다 점수
    float goldEarnTime = 3; //3초마다 골드
    float specialEarnTime = 0.1f; // 2 초마다 게이지 업

    void PlusScore() //몇 초마다 점수 주는 코드
    {
        currentTime += Time.deltaTime;
        if (currentTime > scoreEarnTime)
        {
            Score += 15;
            currentTime = 0;
        }
    }

    void PlusGold() //몇 초마다 골드 주는 코드
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
        if (isClickSpecialGauge == false && specialGauge >0)
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

    /*훈장 사용처*/
    public void OnClickWholePopulationUp() //최대 인구수 증가 버튼
    {
        if (wholePopulationLimit<33)
        {
            wholePopulationLimit++;
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

