using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JHW_CheatManager : MonoBehaviour
{
    public float[] time;
    int index;
    public GameObject Teamcommand;
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

    public void OnClickCheat()
    {
        JHW_GameManager.instance.Gold   += 999999;
        JHW_GameManager.instance.specialGauge += 90;
        JHW_GameManager.instance.medal += 9999;
        JHW_GameManager.instance.wholePopulationLimit += 100;
    }
    //public void OnClickGG()
    //{
    //    JHW_SoundManager.instance.PlayOneTime(JHW_SoundManager.instance.Btn_Click);

    //    Teamcommand.GetComponent<JHW_Command>().Hp = 0;
    //}
}
