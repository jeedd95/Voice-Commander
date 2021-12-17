using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using BackEnd;

public class JHW_Command : MonoBehaviour
{
    public float Hp;
    Camera MainCam;
    public GameObject CamPos1;

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
            print("게임 오버");
            if(flag==false)
            {
                DataInsert();
                flag = true;
            }

            MainCam.transform.position = CamPos1.transform.position;
            MainCam.transform.rotation = CamPos1.transform.rotation;
            Invoke("MainSceneToScoreBoard", 5f);
        }
    }

    void DataInsert()
    {
        Param param = new Param();
        param.Add("Level", JHW_GameManager.instance.playerLevel);
        param.Add("score", JHW_GameManager.instance.Score);
        param.Add("playTime", JHW_GameManager.instance.playTime);

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

    void MainSceneToScoreBoard()
    {
        SceneManager.LoadScene("JHW_ScoreBoard");
    }
}
