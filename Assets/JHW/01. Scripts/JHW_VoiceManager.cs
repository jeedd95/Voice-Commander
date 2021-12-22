using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JHW_VoiceManager : MonoBehaviour
{
    public bool isRecord;
    public Button Button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Test();   
    }

    void Test()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            isRecord = true;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            isRecord = false;
        }
    }
}
