using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //싱글톤


    public int Score = 0; //플레이어 점수
    public int Gold = 25; //플레이어 골드
    public float specialGauge = 0f; //스폐셜 게이지

    public Text scoreT; //점수 텍스트
    public Text goldT; //골드 텍스트
    public Text specialgageT; //스폐셜 게이지 텍스트
    public Text text4; //스폐셜 게이지 텍스트

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
        Mathf.Clamp(specialGauge, 0, 100); //스폐셜 게이지를 0~100 까지로 제한

        if (isClickSpecialGauge)
        {
            specialGauge -= 0.01f;
        }


        PlusScore();
        PlusGold();

        if (isClickSpecialGauge == false) PlusSpecialGauge(); //스폐셜 게이지를 누르고 있지않을때만 실행

        scoreT.text = "플레이어 점수 : " + Score;
        goldT.text = "골드 : " + Gold;
        specialgageT.text = string.Format("{0,3:N0}", specialGauge) + " %";
        text4.text = specialgageT.text;

    }

    float currentTime;
    float currentTime2;
    float currentTime3;

    float scoreEarnTime = 1; //1초마다 점수
    float goldEarnTime = 3; //3초마다 골드
    float specialEarnTime = 2f; // 2 초마다 게이지 업

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
            specialGauge += 1f;
            currentTime3 = 0;
        }
    }

    public void OnClickDefense() //방어태세 버튼클릭이벤트
    {
        if (isClickSpecialGauge == false)
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
        isClickSpecialGauge = true;
    }
    public void NotClickOffense()
    {
        isClickSpecialGauge = false;
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

