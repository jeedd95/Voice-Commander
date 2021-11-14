using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitManager : MonoBehaviour
{
    JHW_UnitInfo unitinfo;

    GameObject EnemyTarget;

    public GameObject[] Bullet; //총알
    public GameObject FirePos; //발사 포지션
    int bulletnum; //총알의 번호
    bool isfire; // 공격상태인지 아닌지

    enum State // 유닛 상태머신
    {
        Move,
        Hide,
        Attack,
        Die
    }
    State state;

    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();

        state = State.Move; // 초기 상태
    }

    void Update()
    {
        switch (state)
        {
            case State.Move:
                UnitMove();
                UnitDetect();
                break;

            case State.Hide:
                break;

            case State.Attack:
                UnitAttack();
                if(isfire==false)
                {
                    isfire = true;
                    StartCoroutine(CreateBullet());
                }
                break;

            case State.Die:
                break;
        }
    }

    void UnitDetect() // 사거리 안에 enemy태그를 가진 적이있으면 공격상태로 가는 코드
    {

        if (Vector3.Distance(gameObject.transform.position,
            GameObject.FindWithTag("Enemy").transform.position) <= unitinfo.attackRange * 0.1f)
        {
            state = State.Attack;
        }
    }

    void UnitMove() // 전진 이동 하는 코드
    {
        transform.position += transform.forward * unitinfo.moveSpeed * Time.deltaTime;
    }

    void UnitAttack()
    {
        transform.LookAt(GameObject.FindWithTag("Enemy").transform);  //유닛이 사거리 안에 들어온 적을 바라보게함

        switch (gameObject.name) // 유닛에 따라 다른 총알을 쓰도록
        {
            case "RifleMan(Clone)" :
                bulletnum = 0;
                break;
            case "Scout(Clone)":
                bulletnum = 0;
                break;
            case "Sniper(Clone)":
                bulletnum = 0;
                break;
            case "Artillery(Clone)":
                bulletnum = 1;
                break;
            case "Heavy Weapon(Clone)":
                bulletnum = 0;
                break;
            case "Armoured(Clone)":
                bulletnum = 0;
                break;
            case "Tank(Clone)":
                bulletnum = 1;
                break;
            case "Helicopter(Clone)":
                bulletnum = 2;
                break;
            case "Raptor(Clone)":
                bulletnum = 2;
                break;
        }

        if(Vector3.Distance(gameObject.transform.position,
            GameObject.FindWithTag("Enemy").transform.position) > unitinfo.attackRange * 0.1f)
        {
            state = State.Move;
        }
    }

    IEnumerator CreateBullet() // 일정시간마다 총알을 생성
    {
        yield return new WaitForSeconds(100/unitinfo.attackSpeed); //공격속도에 따른 주기
        GameObject bullet = GameObject.Instantiate(Bullet[bulletnum]); //유닛에 따라 다른 총알쓰기
        bullet.transform.position = FirePos.transform.position; //총알의 위치를 발사 위치랑 일치
        bullet.transform.up = FirePos.transform.forward; //총알의 방향을 발사 방향이랑 일치

        isfire = false;
    }

    void UnitDie()
    {

    }

}
