using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //싱글톤
    //JHW_UnitManager unitManager;
    GameObject[] MyUnits; //씬에 나와있는 모든 내 유닛들
    GameObject[] MyUnits2;

    public int Score = 0; //플레이어 점수
    public int Gold = 25 ; //플레이어 골드
    public float specialGauge = 0f; //스폐셜 게이지

    public Text text; //점수 텍스트
    public Text text2; //골드 텍스트
    public Text text3; //스폐셜 게이지 텍스트
    public Text text4; //스폐셜 게이지 텍스트

    bool isClickSpecialGauge=false;

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
        PlusScore();
        PlusGold();
        
        Mathf.Clamp(specialGauge, 0, 100); //스폐셜 게이지를 0~100 까지로 제한
        if (isClickSpecialGauge == false) PlusSpecialGauge(); //스폐셜 게이지를 누르고 있지않을때만 실행
        if (isClickSpecialGauge == true) DefensivePosture(); //스폐셜 게이지를 누르고 있을때 실행

        text.text = "플레이어 점수 : " + Score;
        text2.text = "골드 : " + Gold;
        text3.text = string.Format("{0,3:N0}", specialGauge)  + " %";
        text4.text = text3.text;
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

    void DefensivePosture() // 방어태세, 하이어라키에있는 모든 Player 태그 유닛을 검색하고 State를 Hide로 만듬
    {
        MyUnits = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < MyUnits.Length; i++) 
        {
            if(MyUnits[i].transform.childCount == 0)
            {
                //MyUnits[i].
            }

            MyUnits2[i] = MyUnits[i].transform.parent.gameObject;
            MyUnits2[i].GetComponent<JHW_UnitManager>().state = JHW_UnitManager.State.Hide;
        }
        specialGauge -= 0.01f; //게이지 다운
    }

    public void OnClickDefense() //방어태세 버튼클릭이벤트
    {
        isClickSpecialGauge = true;
    }
    public void NotClickDefense()
    {
        isClickSpecialGauge = false;

        MyUnits = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < MyUnits.Length; i++) //하이어라키에있는 모든 유닛을 검색하고 State를 Move로 만듬
        {
            MyUnits[i].GetComponent<JHW_UnitManager>().state = JHW_UnitManager.State.Move;
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


}

