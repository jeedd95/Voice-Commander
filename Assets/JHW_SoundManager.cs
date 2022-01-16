using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JHW_SoundManager : MonoBehaviour
{
    public static JHW_SoundManager instance; //教臂沛
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
    public AudioClip[] MainScene_UnitProduce;
    public AudioClip[] MainScene_ShotSound; //老馆 醚家府
    public AudioClip[] UnitDeadSound_human;
    public AudioClip GoldSound;
    public AudioClip DefensiveSound;
    public AudioClip OffensiveSound;
    public AudioClip SpecialStop;

    // Start is called before the first frame update
    void Start()
    {
        flag1=false;

        for (int i = 0; i < MyAudio.Length; i++)
        {
        MyAudio[i] = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame

    bool flag1;

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "JHW_Start" && !flag1)
        {
            MyAudio[0].PlayOneShot(StartScene_BG,0.5f);
            flag1 = true;
        }
        //else if((SceneManager.GetActiveScene().name!= "JHW_Start"))
        //{
        //    MyAudio[0].Stop();
        //}

        if (SceneManager.GetActiveScene().name == "JHW_TestScene+Map" && !flag1)
        {
            MyAudio[0].PlayOneShot(MainScene_BG,0.3f);
            flag1 = true;
        }
        //else if ((SceneManager.GetActiveScene().name != "JHW_TestScene+Map"))
        //{
        //    MyAudio[0].Stop();
        //}
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
