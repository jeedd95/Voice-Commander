using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_Unit : MonoBehaviour
{
    public string unitName; //이름
    public string unitScale; //크기(소형,중형,대형) 
    public string attackType; //공격 타입(일반,폭발,진탕,고정)
    public bool canGroundAttack; //대지
    public bool canSkyAttack; //대공
    public bool isAirForce; //공중 유닛인지
    public float damage; //공격력
    public float health; //체력
    public float moveSpeed; //이동속도
    public float attackSpeed; //공격속도
    public float attackRange; //사거리
    //public float accuracyRate;
    public int coolDown; //쿨타임
    public int price; //가격
    public int populationLimit; //인구 제한
    public int exp; //경험치

    
}

