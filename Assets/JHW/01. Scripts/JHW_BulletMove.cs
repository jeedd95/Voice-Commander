//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    float speed = 50; //�Ѿ��� ���ư��� �⺻�ӵ�

    JHW_UnitManager unit; // �Ѿ��� �� unit ������Ʈ
    JHW_UnitInfo unitInfo; //�� ���� ���� ����

    public float damage;
    public float accuracyRate;
    //string attackerName;
    public float firingAngle; 
    public float gravity = 9.8f;
    public Transform Target; //�Ѿ��� ���� �� (nearestObject)
    public Transform Projectile; // �Ѿ�
    private Transform myTransform; //������ (����)
    public GameObject exploArea;

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

        if (unitInfo.unitName == "Artillery" || unitInfo.unitName == "Tank")
        {
            StartCoroutine(ParabolaBulletMove());
        }
    }

    void Update()
    {

        if(unitInfo.unitName=="Artillery" || unitInfo.unitName == "Tank")
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

    bool flag;
    IEnumerator ParabolaBulletMove() //�����
    {
        if(flag==false)
        {
            Target = unitInfo.GetComponent<JHW_UnitManager>().neareastObject.transform;
            flag = true;
        }

        Projectile = transform;

       // Projectile.position = myTransform.position + new Vector3(0, 0.0f, 0);

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
            Projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime , Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }
    }




    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Wall"))
        {
            //�� ������ ����¼��̸� ���� ��� �Ѿ��� ���� �հ� ����(���� ��� �Ѿ��� �״��)
            if(!unitInfo.isBehindWall && !unitInfo.UseDefensive)
            {
                other.GetComponent<JHW_Wall>().wallHp -= 10f;
                Destroy(gameObject); //���� �ε����� �Ѿ� ����
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


            if (Random.Range(1, 101) > accuracyRate) //1~100 �߿��� �ϳ��� ��� ���߷����� ���ٸ� �ȸ������� ó��
            {
                print("������");
                Destroy(gameObject);
                return;
            }

            if (unitInfo.unitName == "Artillery" || unitInfo.unitName == "Tank" || unitInfo.unitName =="Raptor") //������ ���ֵ�
            {
                GameObject explo= Instantiate(exploArea);
                explo.transform.position = other.transform.position;
                explo.GetComponent<JHW_ExploArea>().SetCreator(this); //���� �������� �Ѿ��� �˷��ְ� �ʹ�
                Destroy(explo, 0.3f);
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
            #region ����������ڵ�
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
            #endregion

            Destroy(gameObject); //������ �ε��� �Ѿ��� ����

        }
        else if (other.CompareTag("PlayerCommand"))
        {
            GameObject.Find("TeamCommand").GetComponentInChildren<JHW_Command>().Hp -= (int)damage;
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
        else if(other != null) //���� �ε����� 10�ʾȿ��� �������
        {
            Destroy(gameObject,30f);
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
    //�Ѿ��� �ӵ� ������ �ٶ��� 50(��ȭ��0)�϶� �״�� 50, 100�̰ų� 0�϶�(�ִ�) 75(1.5��)
    void WindToBullet() //�Ѿ��� �ٶ��� ������ ����
    {
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
