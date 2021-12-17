using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{
    public Text Level;
    public Text Score;
    public Text Time;

    float chartime;
    int charLevel;
    int charScore;

    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {
        Level.text = charLevel.ToString();
        Score.text = charScore.ToString();
        Time.text = chartime.ToString();
    }

    public void OnClickPublicContents()
    {

    }

    public void OnClickInsertData()
    {
        chartime = Random.Range(0, 9999);
        charLevel = Random.Range(0, 99);
        charScore = Random.Range(0, 999999);

        Param param = new Param();
        param.Add("PlayTime", chartime);
        param.Add("lv", charLevel);
        param.Add("score", charScore);

        BackendReturnObject BRO = Backend.GameData.Insert("custom", param);

        if (BRO.IsSuccess())
        {
            Debug.Log("동기 방식 데이터 삽입 성공");
        }
        else
        {
            Debug.Log("저장 실패");
        }




    }
}
