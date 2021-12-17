using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using UnityEngine.UI;


public class ScoreManager : MonoBehaviour
{

    public Text ScoreBoard;

    void Start()
    {
        string[] select = { "Level", "score", "playTime" };
        Where where = new Where();

        BackendReturnObject bro = Backend.GameData.Get("custom", where,select);
       // BackendReturnObject bro2 = Backend.GameData.GetMyData("custom", "rowIndate", select);
        if (bro.IsSuccess() == false)
        {
            return;
        }
        int level = (int)bro.Rows()[4]["Level"]["N"];
        print(level);
    }

    void Update()
    {

    }
}
