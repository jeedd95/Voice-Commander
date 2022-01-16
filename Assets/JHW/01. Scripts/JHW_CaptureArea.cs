using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JHW_CaptureArea : MonoBehaviour
{
    float capturetime = 0; //주둔지 점령해야하는 시간

    private void Start()
    {
        //Invoke("ColliderMoving", JHW_GameManager.instance.RemainTime);
        Destroy(gameObject, JHW_GameManager.instance.RemainTime);
    }

    private void Update()
    {

    }

    //주둔지 콜라이더에 내 유닛이 하나라도 닿고 있으면 버프를 얻는다(unitinfo의 isenemy로 검증)
    //IEnumerator OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
    //    {
    //        yield return new WaitForSecondsRealtime(capturetime); //일정시간이 지나면 버프의 플래그를 트루로 만들어준다

    //        switch (this.name)
    //        {
    //            case "CaptureArea_Gold(Clone)":
    //                JHW_GameManager.instance.isBuff_Gold = true;
    //                break;
    //            case "CaptureArea_CoolDown(Clone)":
    //                JHW_GameManager.instance.isBuff_CoolDown = true;
    //                break;
    //            case "CaptureArea_SpecialGage(Clone)":
    //                JHW_GameManager.instance.isBuff_SpecialGauge = true;
    //                break;
    //        }
    //    }
    //}


    GameObject temp;

    private void OnTriggerStay(Collider other) // 점령지에 유닛이 있는지 매 프레임마다 확인
    {

        if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
        {
            temp = other.transform.parent.gameObject; //안에 있는 유닛

            switch (this.name)
            {
                case "CaptureArea_Gold(Clone)":
                    JHW_GameManager.instance.isBuff_Gold = true;
                    other.gameObject.GetComponentInParent<JHW_UnitInfo>().inCaputureBox_Gold = true;
                    break;
                case "CaptureArea_CoolDown(Clone)":
                    JHW_GameManager.instance.isBuff_CoolDown = true;
                    other.gameObject.GetComponentInParent<JHW_UnitInfo>().inCaputureBox_Cool = true;
                    break;
                case "CaptureArea_SpecialGage(Clone)":
                    JHW_GameManager.instance.isBuff_SpecialGauge = true;
                    other.gameObject.GetComponentInParent<JHW_UnitInfo>().inCaputureBox_Spe = true;
                    break;
            }
        }
    }


    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player") && other.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
    //    {
    //        switch (this.name)
    //        {
    //            case "CaptureArea_Gold(Clone)":
    //                JHW_GameManager.instance.isBuff_Gold = false;
    //                break;
    //            case "CaptureArea_CoolDown(Clone)":
    //                JHW_GameManager.instance.isBuff_CoolDown = false;
    //                break;
    //            case "CaptureArea_SpecialGage(Clone)":
    //                JHW_GameManager.instance.isBuff_SpecialGauge = false;
    //                break;

    //        }
    //    }
    //}

    private void OnDestroy()
    {
        if (temp.GetComponent<JHW_UnitInfo>().inCaputureBox_Gold && JHW_GameManager.instance.isBuff_Gold) JHW_GameManager.instance.isBuff_Gold = false;
        if (temp.GetComponent<JHW_UnitInfo>().inCaputureBox_Cool && JHW_GameManager.instance.isBuff_CoolDown) JHW_GameManager.instance.isBuff_CoolDown = false;
        if (temp.GetComponent<JHW_UnitInfo>().inCaputureBox_Spe &&JHW_GameManager.instance.isBuff_SpecialGauge) JHW_GameManager.instance.isBuff_SpecialGauge = false;

    }

    
    //void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
    //    {
    //        //yield return new WaitForSeconds(capturetime); //일정시간이 지나면 버프의 플래그를 트루로 만들어준다
    //            switch (this.name)
    //            {
    //                case "CaptureArea_Gold(Clone)":
    //                    JHW_GameManager.instance.isBuff_Gold = true;
    //                    break;
    //                case "CaptureArea_CoolDown(Clone)":
    //                    JHW_GameManager.instance.isBuff_CoolDown = true;
    //                    break;
    //                case "CaptureArea_SpecialGage(Clone)":
    //                    JHW_GameManager.instance.isBuff_SpecialGauge = true;
    //                    break;
    //            }
    //    }
    //}
   
    void ColliderMoving() //지역이 아래로 하강하는 코드
    {
        for (int i = 0; i < 20; i++)
        {
        gameObject.transform.position += Vector3.down;
        }
    }

}
