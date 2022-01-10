using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mic : MonoBehaviour
{
    public AudioSource mic; //마이크로 녹음된 소리를 재생할 오디오소스 컴포넌트

    void Start()
    {
        //오디오 소스 클립에 마이크값을 지정
        mic.clip = Microphone.Start(Microphone.devices[0].ToString(), true,500, 44100);

        //딜레이를 줄이기 위해 추가한 코드
        while (!(Microphone.GetPosition(null) > 0)) { }

        //마이크 녹음 재생
        mic.Play();
    }
}
