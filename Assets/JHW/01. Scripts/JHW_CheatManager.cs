using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JHW_CheatManager : MonoBehaviour
{
    public float[] time;
    int index;

    [SerializeField]Text textSpeed;
    // Start is called before the first frame update
    void Start()
    {
        SetSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickSpeed()
    {
        index = (index + 1) % time.Length;
        SetSpeed();
    }
    void SetSpeed()
    {
        Time.timeScale = time[index];
        textSpeed.text = "x" + Time.timeScale;
    }
}
