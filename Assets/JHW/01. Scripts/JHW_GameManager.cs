using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance; //�̱���
    public int Score = 0; //�÷��̾� ����
    public Text text; //���� �ؽ�Ʈ

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
        text.text = "�÷��̾� ���� : " + Score;
    }

    float currentTime;
    float earnTime = 1; //1�ʸ��� ����

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

