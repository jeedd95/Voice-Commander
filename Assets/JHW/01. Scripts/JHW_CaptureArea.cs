using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JHW_CaptureArea : MonoBehaviour
{
    float capturetime = 0; //�ֵ��� �����ؾ��ϴ� �ð�

    private void Start()
    {
        //Invoke("ColliderMoving", JHW_GameManager.instance.RemainTime);
        Destroy(gameObject, JHW_GameManager.instance.RemainTime);
    }

    private void Update()
    {

    }

    //�ֵ��� �ݶ��̴��� �� ������ �ϳ��� ��� ������ ������ ��´�(unitinfo�� isenemy�� ����)
    //IEnumerator OnTriggerEnter(Collider other)
    //{

    //    if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
    //    {
    //        yield return new WaitForSecondsRealtime(capturetime); //�����ð��� ������ ������ �÷��׸� Ʈ��� ������ش�

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

    private void OnTriggerStay(Collider other) // �������� ������ �ִ��� �� �����Ӹ��� Ȯ��
    {

        if (other.CompareTag("Player") && other.gameObject.GetComponentInParent<JHW_UnitInfo>().isEnemy == false)
        {
            temp = other.transform.parent.gameObject; //�ȿ� �ִ� ����

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
    //        //yield return new WaitForSeconds(capturetime); //�����ð��� ������ ������ �÷��׸� Ʈ��� ������ش�
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
   
    void ColliderMoving() //������ �Ʒ��� �ϰ��ϴ� �ڵ�
    {
        for (int i = 0; i < 20; i++)
        {
        gameObject.transform.position += Vector3.down;
        }
    }

}
