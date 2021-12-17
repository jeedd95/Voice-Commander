using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    // Update is called once per frame
    void Update()
    {
        if(Hp<=0)
        {
            print("게임 오버");
            MainCam.transform.position = CamPos1.transform.position;
            MainCam.transform.rotation = CamPos1.transform.rotation;
            Invoke("MainSceneToScoreBoard", 5f);
        }
    }

    void MainSceneToScoreBoard()
    {
        SceneManager.LoadScene("JHW_ScoreBoard");
    }
}
