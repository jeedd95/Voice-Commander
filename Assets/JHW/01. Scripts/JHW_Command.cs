using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;
using System;

public class JHW_Command : MonoBehaviour
{
    public float OriginHp=5000;
    public float Hp;
    Camera MainCam;
    public GameObject CamPos1;
    public GameObject CommandExploEffect;
    public Canvas MainCanvas;
    public Canvas GameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Hp = OriginHp;
        MainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    bool flag;
    // Update is called once per frame
    void Update()
    {
        if(Hp<=0)
        {
            JHW_SoundManager.instance.flag = false;
            JHW_SoundManager.instance.state = JHW_SoundManager.State.Idle;
            GG();
        }
    }

    void GG()
    {
        print("게임 오버");
        if (flag == false)
        {
          //  DataInsert();
            flag = true;
        }

        MainCanvas.enabled=false;
        MainCam.transform.position = CamPos1.transform.position;
        MainCam.transform.rotation = CamPos1.transform.rotation;
        CommandExploEffect.SetActive(true);
        Invoke("StopTime", 5.3f);
        // Invoke("MainSceneToScoreBoard", 6f);
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
            Debug.Log("동기 방식 데이터 삽입 성공");
        }
        else
        {
            Debug.Log("저장 실패");
        }
    }

    void StopTime()
    {
        GameOverCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


    public void MainSceneToScoreBoard()
    {
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);
        //JHW_SoundManager.instance.flag = false;
        //JHW_SoundManager.instance.state = JHW_SoundManager.State.ScoreScene;
        SceneManager.LoadScene("JHW_ScoreBoard");
    }

    public void OnClickGG()
    {
        Time.timeScale = 1;
        JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);
        Hp = 0;
    }
}
