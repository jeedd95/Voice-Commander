using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class JHW_CaptureArea : MonoBehaviour
{
    float capturetime = 10; //�ֵ��� �����ؾ��ϴ� �ð�

    private void Start()
    {
        Invoke("ColliderMoving", JHW_GameManager.instance.RemainTime);
        Destroy(gameObject, JHW_GameManager.instance.RemainTime+1);
    }

    private void Update()
    {

    }

    //�ֵ��� �ݶ��̴��� �� ������ �ϳ��� ��� ������ ������ ��´�(unitinfo�� isenemy�� ����)
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

    void ColliderMoving() //������ �Ʒ��� �ϰ��ϴ� �ڵ�
    {
        for (int i = 0; i < 20; i++)
        {
        gameObject.transform.position += Vector3.down;
        }
    }

}
