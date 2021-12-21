using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;
using System;

public class JHW_Command : MonoBehaviour
{
    public float Hp;
    Camera MainCam;
    public GameObject CamPos1;
    public GameObject CommandExploEffect;
    public Canvas MainCanvas;
    public Canvas GameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Hp = 5000f;
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    bool flag;
    // Update is called once per frame
    void Update()
    {
        if(Hp<=0)
        {
            print("���� ����");
            if(flag==false)
            {
                DataInsert();
                flag = true;
            }

            MainCanvas.enabled = false;
            MainCam.transform.position = CamPos1.transform.position;
            MainCam.transform.rotation = CamPos1.transform.rotation;
            CommandExploEffect.SetActive(true);
            Invoke("StopTime", 5.3f);
           // Invoke("MainSceneToScoreBoard", 6f);
        }
    }

    void DataInsert()
    {
        Param param = new Param();
        param.Add("NickName", Login.instance.Nickname.text);
        param.Add("Level", JHW_GameManager.instance.playerLevel);
        param.Add("score", JHW_GameManager.instance.Score);
        param.Add("playTime",Math.Round(JHW_GameManager.instance.playTime,3));

        BackendReturnObject BRO = Backend.GameData.Insert("custom", param);
        if (BRO.IsSuccess())
        {
            Debug.Log("���� ��� ������ ���� ����");
        }
        else
        {
            Debug.Log("���� ����");
        }
    }

    void StopTime()
    {
        GameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


    public void MainSceneToScoreBoard()
    {
        SceneManager.LoadScene("JHW_ScoreBoard");
    }
}