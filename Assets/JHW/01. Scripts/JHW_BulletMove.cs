//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    float speed = 50; //�Ѿ��� ���ư��� �⺻�ӵ�

    JHW_UnitManager unit; // �Ѿ��� �� unit ������Ʈ
    JHW_UnitInfo unitInfo; //�� ���� ���� ����
    JHW_UnitManager um;

    private float damage;
    private float accuracyRate;
    //string attackerName;
    public float firingAngle; 
    public float gravity = 9.8f;
    public Transform Target; //�Ѿ��� ���� �� (nearestObject)
    public Transform Projectile; // �Ѿ� �� �ڽ�
    private Transform myTransform; //������ (����)

    float[][] rate;
    float defensiveDamage;
    //bool isTeam;

    

    private void Start()
    {

        rate = new float[][] {
                new float[]{1,1,1},
                new float[]{0.5f,0.75f,1},
                new float[]{1,0.5f,0.25f},
            };

     um= GameObject.Find("UnitFactory").GetComponent<JHW_UnitManager>();
    }

    void Update()
    {
        print(Target);

        if(unitInfo.unitName=="Artillery")
        {
            ParabolaBulletMove();
        }
        else
        {
        transform.position += transform.up * speed * Time.deltaTime;
        }

        if (JHW_GameManager.instance.Flag_wind)
        {
            WindToBullet();
        }
    }

    void ParabolaBulletMove()
    {
        Target = um.neareastObject.transform;

        Projectile = transform;
        //Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile.rotation = Quaternion.LookRotation(Target.position - Projectile.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            return;
        }
    }




    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Wall"))
        {
            //�� ������ ����¼��̸� ���� ��� �Ѿ��� ���� �հ� ����(���� ��� �Ѿ��� �״��)
            if(!unitInfo.isBehindWall && !unitInfo.UseDefensive)
            {
                Destroy(gameObject); //���� �ε����� �Ѿ� ����
                other.GetComponent<JHW_Wall>().wallHp -= 10f;
            }

            //if(unitInfo.UseDefensive)
            //{
            //}
            //else if (!unitInfo.UseDefensive)
            //{
            //    if(unitInfo.isBehindWall)
            //    {
            //    }
            //    else
            //    {
            //        Destroy(gameObject); //���� �ε����� �Ѿ� ����
            //        other.GetComponent<JHW_Wall>().wallHp -= 10f;
            //    }
            //}
        }

        else if (other.CompareTag("Enemy") || other.CompareTag("Player")) // ���ϰ� �Ѿ��� �ε������� ������ ����
        {
            JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>(); //���� ���� ��������

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

          //  print("������ ���� : " + unitInfo + "���� ���� : " + hitObj.gameObject + "���� ������ ü�� : " + (float)hitObj.health);

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
        else if (other.CompareTag("PlayerTurret"))
        {
            JHW_Turret hitObj = other.GetComponent<JHW_Turret>();
            hitObj.turretHp -= unitInfo.damage;
            Destroy(gameObject);
        }
        else if (other.CompareTag("EnemyTurret"))
        {
            JHW_Turret hitObj = other.GetComponent<JHW_Turret>();
            hitObj.turretHp -= unitInfo.damage;
            Destroy(gameObject);

        }
    }

    public void SetCreator(JHW_UnitManager unit)  //�Ѿ��� ������ �������� �˷��ִ� �Լ�
    {
        this.unit = unit;
        unitInfo = unit.GetComponent<JHW_UnitInfo>(); //�� ���� ���� ����
        this.damage = unitInfo.damage;
        this.accuracyRate = unitInfo.ACCURACYRATE;
       // attackerName = unit.gameObject.name;
    }

    //�ٶ��� ���Ⱑ 50�̸��϶��� ����� ���ʹ��̸� �Ѿ��� �ӵ��� ����
    //�ݴ�� �츮���� �Ѿ��� �ӵ��� ����
    //�Ѿ��� �ӵ� ������ �ٶ��� 50(��ȭ��0)�϶� �״�� 50 100�̰ų� 0�϶�(�ִ�) 75(1.5��)

    void WindToBullet() //�Ѿ��� �ٶ��� ������ ����
    {
        //if(JHW_GameManager.instance.windPower <50 && unitInfo.isEnemy ==true) //�� ���� ��
        //{
        //    speed = -0.5f * JHW_GameManager.instance.windPower + 75;
        //}
        //else if (JHW_GameManager.instance.windPower > 50 && unitInfo.isEnemy == false) //����� �Ʊ�
        //{
        //    speed = 0.5f * JHW_GameManager.instance.windPower + 25;
        //}
        //else speed = 50;

       if(unitInfo.isEnemy == true) //�����϶�
        {
            speed = -0.5f * JHW_GameManager.instance.windPower + 75.0f;
        }
       else
        {
            speed = 0.5f * JHW_GameManager.instance.windPower + 25.0f;
        }
    }


}
