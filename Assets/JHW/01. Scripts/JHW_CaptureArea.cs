using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JHW_CaptureArea : MonoBehaviour
{
    float capturetime = 10; //주둔지 점령해야하는 시간

    private void Start()
    {
        Invoke("ColliderMoving", JHW_GameManager.instance.RemainTime);
        Destroy(gameObject, JHW_GameManager.instance.RemainTime+1);
    }

    private void Update()
    {

    }

    //주둔지 콜라이더에 내 유닛이 하나라도 닿고 있으면 버프를 얻는다(unitinfo의 isenemy로 검증)
    IEnumerator OnTriggerEnter(Collider other)
    {

            if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
            {
            yield return new WaitForSeconds(capturetime);

            switch (this.name)
                    {
                        case "CaptureArea_Gold(Clone)":
                            JHW_GameManager.instance.isBuff_Gold = true;
                            break;
                        case "CaptureArea_CoolDown(Clone)":
                            JHW_GameManager.instance.isBuff_CoolDown = true;
                            break;
                        case "CaptureArea_SpecialGage(Clone)":
                        JHW_GameManager.instance.isBuff_SpecialGauge = true;
                            break;
                    }
            }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && other.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
        {
            switch (this.name)
            {
                case "CaptureArea_Gold(Clone)":
                    JHW_GameManager.instance.isBuff_Gold = false;
                    break;
                case "CaptureArea_CoolDown(Clone)":
                    JHW_GameManager.instance.isBuff_CoolDown = false;
                    break;
                case "CaptureArea_SpecialGage(Clone)":
                    JHW_GameManager.instance.isBuff_SpecialGauge = false;
                    break;

            }
        }
    }

    void ColliderMoving() //지역이 아래로 하강하는 코드
    {
        for (int i = 0; i < 20; i++)
        {
        gameObject.transform.position += Vector3.down;
        }
    }

}
