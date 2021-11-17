//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    public float speed=50; //�Ѿ��� ���ư��� �ӵ�

    JHW_UnitManager unit; // �Ѿ��� �� unit ������Ʈ
    JHW_UnitInfo unitInfo;

    private void Start()
    {
        
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Wall"))
        {
            Destroy(gameObject); //���� �ε����� �Ѿ� ����
            other.GetComponent<JHW_Wall>().wallHp -= 10f;
        }

        else if (other.CompareTag("Enemy") || other.CompareTag("Player")) // ���ϰ� �Ѿ��� �ε������� ������ ����
        {
            JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>(); //���� ���� ��������
            unitInfo = unit.GetComponent<JHW_UnitInfo>(); //�� ���� ���� ����

            if (Random.Range(1, 100) > unitInfo.accuracyRate) //1~100 �߿��� �ϳ��� ��� ���߷����� ���ٸ� �ȸ������� ó��
            {
                print("������");
                return; 
            }

            switch(unitInfo.attackType) //�����ѳ��� ����Ÿ��
            {
                case AttackType.normal: //�Ϲ���
                    if (hitObj.unitScale == UnitScale.small) hitObj.health -= unitInfo.damage * 1; //����
                    if (hitObj.unitScale == UnitScale.middle) hitObj.health -= unitInfo.damage * 1; //����
                    if (hitObj.unitScale == UnitScale.large) hitObj.health -= unitInfo.damage * 1; //����
                    break;
                case AttackType.explosive: //������
                    if (hitObj.unitScale == UnitScale.small) hitObj.health -= unitInfo.damage * 0.5f; 
                    if (hitObj.unitScale == UnitScale.middle) hitObj.health -= unitInfo.damage * 0.75f;
                    if (hitObj.unitScale == UnitScale.large) hitObj.health -= unitInfo.damage * 1;
                    break;
                case AttackType.concussive : //������
                    if (hitObj.unitScale == UnitScale.small) hitObj.health -= unitInfo.damage * 1f;
                    if (hitObj.unitScale == UnitScale.middle) hitObj.health -= unitInfo.damage * 0.5f;
                    if (hitObj.unitScale == UnitScale.large) hitObj.health -= unitInfo.damage * 0.25f;
                    break;
            }
            print("���� �� ���� : " + unitInfo.gameObject + "���� ���� ���� : " + hitObj.gameObject + "ü�� : "+ hitObj.health);
            Destroy(gameObject); //������ �ε��� �Ѿ��� ����
        }
        else if(other.CompareTag("PlayerCommand"))
        {
            Destroy(gameObject);
        }
        else if(other.CompareTag("EnemyCommand"))
        {
            Destroy(gameObject);
        }

    }

    public void SetCreator(JHW_UnitManager unit)
    {
        this.unit = unit;
    }


    void HitRate()
    {
    }
}
