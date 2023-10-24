using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JHW_SoundManager : MonoBehaviour
{
    public static JHW_SoundManager instance; //싱글톤
    public AudioSource[] MyAudio;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioClip StartScene_BG;
    public AudioClip Btn_Click;
    public AudioClip MainScene_BG;
    public AudioClip MainScene_Noise;
    public AudioClip ScoreScene_BG;
    public AudioClip[] MainScene_UnitProduce;
    public AudioClip[] MainScene_ShotSound; //일반 총소리
    public AudioClip Nagative;

    public AudioClip[] UnitDeadSound_human;
    public AudioClip[] UnitDeadSound_mechanic;

    public AudioClip GoldSound;
    public AudioClip DefensiveSound;
    public AudioClip OffensiveSound;
    public AudioClip SpecialStop;
    public AudioClip TankSound;
    public AudioClip Helicopter;
    public AudioClip Raptor;
    public AudioClip Scout;
    public AudioClip Sniper;
    public AudioClip Armourd;
    // Start is called before the first frame update
    void Start()
    {
        //state = State.LoginScene;

        for (int i = 0; i < MyAudio.Length; i++)
        {
        MyAudio[i] = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame

    public enum State
    {
        Idle,
        LoginScene,
        MainScene,
        ScoreScene
    }

    public State state;

    public bool flag;
    void Update()
    {
        //if (SceneManager.GetActiveScene().name == "JHW_Start")
        //{
        //    state = State.LoginScene;
        //}
        //if(SceneManager.GetActiveScene().name == "JHW_StartCine")
        //{
        //    state = State.Idle;
        //}
        //if(SceneManager.GetActiveScene().name == "JHW_TestScene+Map")
        //{
        //    state = State.MainScene;
        //}
        //if (SceneManager.GetActiveScene().name == "JHW_ScoreBoard")
        //{
        //    state = State.ScoreScene;
        //}
        if(!flag)
        {
            bgm();
            flag = true;
        }
        print(state);
    }

    public void bgm() //0번이 배경음 1번이 클릭음 같은 1회성
    {
        switch (state)
        {
            case State.Idle:
                MyAudio[0].Stop();
                break;
            case State.LoginScene:
                MyAudio[0].PlayOneShot(StartScene_BG, 0.5f);
                break;
            case State.MainScene:
                MyAudio[0].PlayOneShot(MainScene_BG, 0.3f);
                break;
            case State.ScoreScene:
                MyAudio[0].PlayOneShot(ScoreScene_BG, 1f);
                break;
        }

    }

    public void PlayOneTime(AudioClip audio)
    {
        MyAudio[1].PlayOneShot(audio);
    }
    public void PlayOneTimeRandom(AudioClip[] audio)
    {
        int i = Random.Range(0, audio.Length-1);

        if(audio == MainScene_ShotSound || audio == UnitDeadSound_human)
        {
            MyAudio[1].PlayOneShot(audio[i],0.2f);
        }
        else
        {
            MyAudio[1].PlayOneShot(audio[i]);
        }
    }

    

}
