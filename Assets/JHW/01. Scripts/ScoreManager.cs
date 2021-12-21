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
    public Text ScoreBoard;
    

    void Start()
    {
        string[] select = { "Level", "score", "playTime" ,"updatedAt","NickName"};
        Where where = new Where();

        var bro = Backend.GameData.Get("custom",new Where(),select); //custom�̶�� ���̺��� ��� ������ (select���ǿ� ����)
        if (bro.IsSuccess() == false)
        {
            print("���� �ҷ����� ����");
            return;
        }
        
        JsonData json = bro.FlattenRows(); 
        MyText.text = "NickName : "+ json[0]["NickName"].ToString()+  " / Level : " + json[0]["Level"].ToString() + " / Score : " + json[0]["score"].ToString() + " / PlayTime : " + json[0]["playTime"].ToString(); // �� ����,���ھ�,�÷���Ÿ��

        Param param = new Param();
        //param.Add("updatedAt", json[0]["updatedAt"].ToString());
        //param.Add("Level", JHW_GameManager.instance.playerLevel);
        param.Add("nickname", json[0]["NickName"]);
        param.Add("score", json[0]["score"]);
        param.Add("playTime", json[0]["playTime"]);

        Backend.URank.User.UpdateUserScore("85eb2800-6158-11ec-85ad-571b56ff94ac", "custom", json[0]["inDate"].ToString(), param); //��ü ��ŷ�� ������Ʈ��

        var bro2 =  Backend.URank.User.GetRankList("85eb2800-6158-11ec-85ad-571b56ff94ac", 100); 
        JsonData json2 = bro2.FlattenRows(); //json2�� ��� ���ھ ��Ƶ�
        JsonData totalCount = bro2.GetFlattenJSON(); //totoalCountjson���� �ٸ�������� �Ľ���
        string a = totalCount["totalCount"].ToString();


        for (int i = 0; i < json2.Count; i++)
        {
            ScoreBoard.text += "\n\n" +"        "+
                                      json2[i]["rank"] + "  \t\t" +
                                      json2[i]["gamerInDate"].ToString().Substring(0,10) + "\t\t        " +
                                      json2[i]["nickname"].ToString() + "\t\t\t                   " +
                                      json2[i]["score"].ToString() + "\t\t\t              " +
                                      json2[i]["playTime"].ToString();
        }


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
