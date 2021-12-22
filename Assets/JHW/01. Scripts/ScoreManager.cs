using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;
using LitJson;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{

    public Text MyText;
    public Text MyRank;
    public Text ScoreBoard_rank;
    public Text ScoreBoard_gamerInDate;
    public Text ScoreBoard_nickname;
    public Text ScoreBoard_score;
    public Text ScoreBoard_playTime;

    void Start()
    {
        string[] select = { "Level", "score", "playTime" ,"updatedAt","NickName"};
        Where where = new Where();

        var bro = Backend.GameData.Get("custom",new Where(),select); //custom이라는 테이블의 모든 데이터 (select조건에 의해)
        if (bro.IsSuccess() == false)
        {
            print("점수 불러오기 실패");
            return;
        }
        
        JsonData json = bro.FlattenRows(); 
        MyText.text = "NickName : "+ json[0]["NickName"].ToString()+  " / Level : " + json[0]["Level"].ToString() + " / Score : " + json[0]["score"].ToString() + " / PlayTime : " + json[0]["playTime"].ToString(); // 내 레벨,스코어,플레이타임
        Param param = new Param();
        //param.Add("updatedAt", json[0]["updatedAt"].ToString());
        //param.Add("Level", JHW_GameManager.instance.playerLevel);
        param.Add("nickname", json[0]["NickName"]);
        param.Add("score", json[0]["score"]);
        param.Add("playTime", json[0]["playTime"]);

        Backend.URank.User.UpdateUserScore("85eb2800-6158-11ec-85ad-571b56ff94ac", "custom", json[0]["inDate"].ToString(), param); //전체 랭킹을 업데이트함

        var bro2 =  Backend.URank.User.GetRankList("85eb2800-6158-11ec-85ad-571b56ff94ac", 100); 
        JsonData json2 = bro2.FlattenRows(); //json2는 모든 스코어를 담아둠
        JsonData totalCount = bro2.GetFlattenJSON(); //totoalCountjson에는 다른방식으로 파싱함
        var bro3 = Backend.URank.User.GetMyRank("85eb2800-6158-11ec-85ad-571b56ff94ac");
        JsonData json3 = bro3.FlattenRows();
        string b = json3[0]["rank"].ToString();
        string a = totalCount["totalCount"].ToString();


        MyRank.text = b + " / " + a;
        for (int i = 0; i < json2.Count; i++)
        {
            ScoreBoard_rank.text += "\n" + json2[i]["rank"];
            ScoreBoard_gamerInDate.text += "\n" + json2[i]["gamerInDate"].ToString().Substring(0, 10);
            ScoreBoard_nickname.text += "\n" + json2[i]["nickname"];
            ScoreBoard_score.text += "\n" + json2[i]["score"];
            ScoreBoard_playTime.text += "\n" + json2[i]["playTime"];
        }


        //for (int i = 0; i < json2.Count; i++)
        //{
        //    ScoreBoard.text += "\n\n" +"        "+
        //                              json2[i]["rank"] + "  \t\t" +
        //                              json2[i]["gamerInDate"].ToString().Substring(0,10) + "\t\t        " +
        //                              json2[i]["nickname"].ToString() + "\t\t\t                   " +
        //                              json2[i]["score"].ToString() + "\t\t\t              " +
        //                              json2[i]["playTime"].ToString();
        //}


    }

    void Update()
    {

    }

    public void OnClickExit()
    {
        Application.Quit();
    }

    public void OnClickRetry()
    {
        SceneManager.LoadScene("JHW_TestScene+Map");
    }
    public void OnClickToMain()
    {
        SceneManager.LoadScene("JHW_Start");
    }
}
