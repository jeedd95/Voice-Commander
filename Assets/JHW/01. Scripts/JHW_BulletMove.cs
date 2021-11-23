//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    public float speed = 50; //�Ѿ��� ���ư��� �ӵ�

    JHW_UnitManager unit; // �Ѿ��� �� unit ������Ʈ
    JHW_UnitInfo unitInfo;
    private float damage;
    private float accuracyRate;
    string attackerName;

    float[][] rate;
    float defensiveDamage;
    bool isTeam;

    private void Start()
    {
        rate = new float[][] {
                new float[]{1,1,1},
                new float[]{0.5f,0.75f,1},
                new float[]{1,0.5f,0.25f},
            };
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>(); //���� ���� ��������


        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); //���� �ε����� �Ѿ� ����
            other.GetComponent<JHW_Wall>().wallHp -= 10f;
        }

        else if (other.CompareTag("Enemy") || other.CompareTag("Player")) // ���ϰ� �Ѿ��� �ε������� ������ ����
        {
            Destroy(gameObject); //������ �ε��� �Ѿ��� ����

            if (Random.Range(1, 100) > accuracyRate) //1~100 �߿��� �ϳ��� ��� ���߷����� ���ٸ� �ȸ������� ó��
            {
                print("������");
                return;
            }

            if (unitInfo.isEnemy == true && hitObj.isEnemy == false && hitObj.UseDefensive==true)
            { //�����Ѿְ� ���̰�, �´»���� �� �����̰� ����¼��� �Ѱ�������� ����������
                print("������ �氨");
                defensiveDamage = 0.8f;
            }
            else
            {
                defensiveDamage = 1f;
            }

            int type = (int)unitInfo.attackType; //������ ���� Ÿ�Կ� ���� �ٸ�����
            if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * rate[type][0]  * defensiveDamage; //����
            if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * rate[type][1] *defensiveDamage; //����
            if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * rate[type][2] * defensiveDamage; //����

            //switch (unitInfo.attackType) //�����ѳ��� ����Ÿ��
            //{
            //    case AttackType.normal: //�Ϲ���
            //        if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 1; //����
            //        if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 1; //����
            //        if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 1; //����
            //        break;
            //    case AttackType.explosive: //������
            //        if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 0.5f;
            //        if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 0.75f;
            //        if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 1;
            //        break;
            //    case AttackType.concussive: //������
            //        if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 1f;
            //        if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 0.5f;
            //        if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 0.25f;
            //        break;
            //}
            // print("���� �� ���� : " + attackerName + "���� ���� ���� : " + hitObj.gameObject + "ü�� : " + hitObj.health);

            print("������ ���� : " + unitInfo + "���� ���� : " + hitObj + "���� ������ ü�� : " + (float)hitObj.health);

        }
        else if (other.CompareTag("PlayerCommand"))
        {
            Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyCommand"))
        {
            JHW_GameManager.instance.Score += (int)damage;
            Destroy(gameObject);
        }

    }

    public void SetCreator(JHW_UnitManager unit)  //�Ѿ��� ������ �������� �˷��ִ� �Լ�
    {
        this.unit = unit;
        unitInfo = unit.GetComponent<JHW_UnitInfo>(); //�� ���� ���� ����
        this.damage = unitInfo.damage;
        this.accuracyRate = unitInfo.accuracyRate;
        attackerName = unit.gameObject.name;
    }

}
