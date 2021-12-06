//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    float speed = 50; //총알이 날아가는 속도

    JHW_UnitManager unit; // 총알을 쏜 unit 컴포넌트
    JHW_UnitInfo unitInfo;
    JHW_UnitManager um;
    private float damage;
    private float accuracyRate;
    //string attackerName;

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
    Vector3 pos;
    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
       // print("총알 속도 : " + speed);

        if(JHW_GameManager.instance.Flag_wind)
        {
            WindToBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Wall"))
        {
            //내 유닛이 방어태세이면 내가 쏘는 총알은 벽을 뚫고 간다(적이 쏘는 총알은 그대로)
            if(!unitInfo.isBehindWall && !unitInfo.UseDefensive)
            {
                Destroy(gameObject); //벽에 부딪히면 총알 삭제
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
            //        Destroy(gameObject); //벽에 부딪히면 총알 삭제
            //        other.GetComponent<JHW_Wall>().wallHp -= 10f;
            //    }
            //}
        }

        else if (other.CompareTag("Enemy") || other.CompareTag("Player")) // 적하고 총알이 부딪혔을때 데미지 계산식
        {
            JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>(); //맞은 애의 유닛정보

            Destroy(gameObject); //적에게 부딪힌 총알을 삭제

            if (Random.Range(1, 100) > accuracyRate) //1~100 중에서 하나를 골라 명중률보다 높다면 안맞음으로 처리
            {
                print("빗나감");
                return;
            }

            if (unitInfo.isEnemy == true && hitObj.isEnemy == false && hitObj.UseDefensive==true)
            { //공격한애가 적이고, 맞는사람이 내 유닛이고 방어태세를 켜고있을경우 데미지감소
                print("데미지 경감");
                defensiveDamage = 0.8f;
            }
            else
            {
                defensiveDamage = 1f;
            }

            int type = (int)unitInfo.attackType; //공격한 놈의 타입에 따라 다른공격
            if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * rate[type][0]  * defensiveDamage; //소형
            if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * rate[type][1] *defensiveDamage; //중형
            if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * rate[type][2] * defensiveDamage; //대형

            //switch (unitInfo.attackType) //공격한놈의 공격타입
            //{
            //    case AttackType.normal: //일반형
            //        if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 1; //소형
            //        if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 1; //중형
            //        if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 1; //대형
            //        break;
            //    case AttackType.explosive: //폭발형
            //        if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 0.5f;
            //        if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 0.75f;
            //        if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 1;
            //        break;
            //    case AttackType.concussive: //진탕형
            //        if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 1f;
            //        if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 0.5f;
            //        if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 0.25f;
            //        break;
            //}
            // print("공격 한 유닛 : " + attackerName + "공격 받은 유닛 : " + hitObj.gameObject + "체력 : " + hitObj.health);

          //  print("공격한 유닛 : " + unitInfo + "맞은 유닛 : " + hitObj.gameObject + "맞은 유닛의 체력 : " + (float)hitObj.health);

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

    public void SetCreator(JHW_UnitManager unit)  //총알의 주인이 누구인지 알려주는 함수
    {
        this.unit = unit;
        unitInfo = unit.GetComponent<JHW_UnitInfo>(); //쏜 애의 유닛 정보
        this.damage = unitInfo.damage;
        this.accuracyRate = unitInfo.accuracyRate;
       // attackerName = unit.gameObject.name;
    }

    //바람의 세기가 50미만일때는 쏜놈이 에너미이면 총알의 속도를 증가
    //반대면 우리팀의 총알의 속도를 증가
    //총알의 속도 비율은 바람이 50(변화량0)일때 그대로 50 100이거나 0일때(최대) 75(1.5배)

    void WindToBullet() //총알이 바람의 영향을 받음
    {
        //if(JHW_GameManager.instance.windPower <50 && unitInfo.isEnemy ==true) //쏜 놈이 적
        //{
        //    speed = -0.5f * JHW_GameManager.instance.windPower + 75;
        //}
        //else if (JHW_GameManager.instance.windPower > 50 && unitInfo.isEnemy == false) //쏜놈이 아군
        //{
        //    speed = 0.5f * JHW_GameManager.instance.windPower + 25;
        //}
        //else speed = 50;

       if(unitInfo.isEnemy == true) //적군일때
        {
            speed = -0.5f * JHW_GameManager.instance.windPower + 75.0f;
        }
       else
        {
            speed = 0.5f * JHW_GameManager.instance.windPower + 25.0f;
        }
    }
}
