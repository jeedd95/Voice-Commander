using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JHW_SoundManager : MonoBehaviour
{
    public static JHW_SoundManager instance; //ΩÃ±€≈Ê
    public AudioSource MyAudio;

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


    // Start is called before the first frame update
    void Start()
    {
        MyAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame

    bool flag1;
    void Update()
    {
        if(SceneManager.GetActiveScene().name=="JHW_Start" && !flag1)
        {
            MyAudio.PlayOneShot(StartScene_BG);
            flag1 = true;
        }
    }

    
}
