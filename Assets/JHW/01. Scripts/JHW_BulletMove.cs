//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    public float speed=50; //총알이 날아가는 속도

    JHW_UnitManager unit; // 총알을 쏜 unit 컴포넌트
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
            Destroy(gameObject); //벽에 부딪히면 총알 삭제
            other.GetComponent<JHW_Wall>().wallHp -= 10f;
        }

        else if (other.CompareTag("Enemy") || other.CompareTag("Player")) // 적하고 총알이 부딪혔을때 데미지 계산식
        {
            JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>(); //맞은 애의 유닛정보
            unitInfo = unit.GetComponent<JHW_UnitInfo>(); //쏜 애의 유닛 정보

            if (Random.Range(1, 100) > unitInfo.accuracyRate) //1~100 중에서 하나를 골라 명중률보다 높다면 안맞음으로 처리
            {
                print("빗나감");
                return; 
            }

            switch(unitInfo.attackType) //공격한놈의 공격타입
            {
                case AttackType.normal: //일반형
                    if (hitObj.unitScale == UnitScale.small) hitObj.health -= unitInfo.damage * 1; //소형
                    if (hitObj.unitScale == UnitScale.middle) hitObj.health -= unitInfo.damage * 1; //중형
                    if (hitObj.unitScale == UnitScale.large) hitObj.health -= unitInfo.damage * 1; //대형
                    break;
                case AttackType.explosive: //폭발형
                    if (hitObj.unitScale == UnitScale.small) hitObj.health -= unitInfo.damage * 0.5f; 
                    if (hitObj.unitScale == UnitScale.middle) hitObj.health -= unitInfo.damage * 0.75f;
                    if (hitObj.unitScale == UnitScale.large) hitObj.health -= unitInfo.damage * 1;
                    break;
                case AttackType.concussive : //진탕형
                    if (hitObj.unitScale == UnitScale.small) hitObj.health -= unitInfo.damage * 1f;
                    if (hitObj.unitScale == UnitScale.middle) hitObj.health -= unitInfo.damage * 0.5f;
                    if (hitObj.unitScale == UnitScale.large) hitObj.health -= unitInfo.damage * 0.25f;
                    break;
            }
            print("공격 한 유닛 : " + unitInfo.gameObject + "공격 받은 유닛 : " + hitObj.gameObject + "체력 : "+ hitObj.health);
            Destroy(gameObject); //적에게 부딪힌 총알을 삭제
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
