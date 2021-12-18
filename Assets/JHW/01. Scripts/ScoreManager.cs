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

        var bro = Backend.GameData.Get("custom",new Where(),select);
        if (bro.IsSuccess() == false)
        {
            return;
        }
        bro.FlattenRows();
        print(bro);

        ScoreBoard.text = bro.ToString();
    }

    void Update()
    {

    }
}
