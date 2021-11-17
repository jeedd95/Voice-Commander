using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //싱글톤
    public int Score = 0; //플레이어 점수
    public Text text; //점수 텍스트

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
        ScoreCalc();
        text.text = "플레이어 점수 : " + Score;
    }

    float currentTime;
    float earnTime = 1; //1초마다 점수

    void ScoreCalc()
    {
        currentTime += Time.deltaTime;
        if (currentTime > earnTime)
        {
            Score += 5;
            currentTime = 0;
        }
    }
}

