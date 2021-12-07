using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitScale
{
    small,
    middle,
    large
}
public enum AttackType
{
    normal,
    explosive,
    concussive,
    fixedDamage
}

public class JHW_UnitInfo : MonoBehaviour
{
    public bool isBehindWall; //벽 뒤에 있는지 없는지
    public bool UseDefensive; //방어태세를 썼는지 안썼는지
    public bool UseOffensive; //공격 태세를 썼는지 안썼는지
    public bool isCaptureUnit; //점령유닛인지
    public bool inSmoke;

    public bool isEnemy; //우리팀인지 적팀인지
    public string unitName; //이름
    public UnitScale unitScale; //크기(소형,중형,대형) 
    public AttackType attackType; //공격 타입(일반,폭발,진탕,고정)
    public bool canGroundAttack; //대지
    public bool canSkyAttack; //대공
    public bool isAirForce; //공중 유닛인지
    public float damage; //공격력
    public float health; //체력
    public float moveSpeed; //이동속도
    public float attackSpeed; //공격속도
    public float attackRange; //사거리
    public float accuracyRate; //명중률
    public int coolDown; //쿨타임 GM
    public int price; //가격
    public int populationLimit; //각 유닛 별 인구 제한 GM
    public int exp; //경험치
    public int score; //점수

    // property
    public float ATTACK_RANGE //isBehindWall이 true면 사거리 30퍼 증가 or 원래대로
    {
        get
        {
            if (isBehindWall)
            {
                return attackRange * 1.3f;
            }
            return attackRange;
        }
    }

    public float MOVE_SPEED //UseOffensive를 썻을 경우 이동속도 50퍼 증가
    {
        get
        {
            if (UseOffensive)
            {
                return moveSpeed * 1.5f;
            }
            return moveSpeed;
        }
    }
    public float ATTACK_SPEED  //UseOffensive를 썻을 경우 공격속도 30퍼 증가
    {
        get
        {
            if (UseOffensive)
            {
                return attackSpeed * 1.3f;
            }
            return attackSpeed;
        }
    }

    public float ACCURACYRATE
    {
        get
        {
            if (inSmoke)
            {
                return accuracyRate * 0.5f;
            }
            return accuracyRate;
        }
    }



}

