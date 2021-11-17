using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
using UnityEngine.AI;

public class JHW_UnitManager : MonoBehaviour
{
    JHW_UnitInfo unitinfo;
    GameObject enemyTarget;
    NavMeshAgent navAgent;

    public GameObject[] Bullet; //총알
    public GameObject FirePos; //발사 포지션
    int bulletnum; //총알의 번호
    bool isfire; // 공격상태인지 아닌지
    public GameObject TeamCommand; //우리의 기지
    public GameObject enemyCommand; //적의 기지

    enum State // 유닛 상태머신
    {
        Move,
        Hide,
        Attack,
        Die
    }
    State state;

    public GameObject neareastObject; //유닛이 공격을 시작했을때 가장 가까운 적

    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.Warp(transform.position);

        state = State.Move; // 초기 상태

    }
    void Update()
    {
        UnitDie();

        switch (state)
        {
            case State.Move:
                UnitMove();
                UnitDetect();
                break;

            case State.Attack:
                if (UnitAttack() == false)
                {
                    if (isfire == false)
                    {
                        isfire = true;
                        StartCoroutine("CreateBullet");
                    }
                }
                break;

            case State.Hide:
                break;
        }
    }

    float currentTime;
    float detectTime = 0.01f; //감지시간
    void UnitDetect() // 사거리 안에 enemy태그를 가진 적이있으면 공격상태로 가는 코드
    {

        currentTime += Time.deltaTime;
        if (currentTime > detectTime)
        {
            if (unitinfo.isEnemy == false) neareastObject = FindNearestObjectzByLayer("EnemyTeam");
            if (unitinfo.isEnemy == true) neareastObject = FindNearestObjectzByLayer("PlayerTeam");
            currentTime = 0;

            //가장 가까운 오브젝트가 있고 거리가 공격 사거리 안에 있으면
            if (neareastObject != null && Vector3.Distance(gameObject.transform.position,
                neareastObject.transform.position) <= unitinfo.attackRange)
            {
                state = State.Attack;
                isfire = false;
            }
        }
    }

    public GameObject FindNearestObjectzByLayer(string layer) //가장 가까운 오브젝트 태그로 찾기
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        var cols = Physics.OverlapSphere(transform.position, unitinfo.attackRange, layerMask); //공격 사거리 안에있는 적 인식

        float dist = float.MaxValue;
        int chooseIndex = -1;
        for (int i = 0; i < cols.Length; i++)
        {
            float temp = Vector3.Distance(transform.position, cols[i].transform.position);
            if (temp < dist)
            {
                dist = temp;
                chooseIndex = i;
            }
        }

        if (chooseIndex == -1) //사거리 안에 있는 적이 없음
        {
            return null;
        }

        return cols[chooseIndex].gameObject;

        //return cols.OrderBy(obj =>
        //{
        //    return Vector3.Distance(transform.position, obj.transform.position);
        //}).FirstOrDefault().gameObject;

        //var objects = GameObject.FindGameObjectsWithTag(tag).ToList();
        //return objects.OrderBy(obj =>
        //{
        //    return Vector3.Distance(transform.position, obj.transform.position);
        //})
        //.FirstOrDefault(); //List의 첫번째 요소를 반환, 비어있으면 null을 반환
    }

    void UnitMove() // nav mesh를 이용하여 인식한 적 방향으로 가는 코드
    {
        //// transform.position += transform.forward * unitinfo.moveSpeed * Time.deltaTime; //앞으로만 가는 코드
        Vector3 offset = Vector3.zero;

        if (unitinfo.isEnemy == false) //우리팀일때
        {
            offset = enemyCommand.transform.position;
            transform.LookAt(Vector3.right);
        }
        if (unitinfo.isEnemy == true) //적팀일때
        {
            offset = TeamCommand.transform.position;
            transform.LookAt(Vector3.left);
        }
        navAgent.SetDestination(offset);
    }

    void UnitHide()
    {

    }


    bool UnitAttack()
    {
        navAgent.SetDestination(transform.position); //공격할땐 그자리에 멈춤

        //유닛이 사거리 안에 들어온 적을 바라보게함
        if (neareastObject != null)
        {
            transform.LookAt(neareastObject.transform);
        }
        //transform.LookAt(GameObject.FindWithTag("Enemy").transform);

        switch (gameObject.name) // 유닛에 따라 다른 총알을 쓰도록
        {
            case "RifleMan(Clone)":
                bulletnum = 0;
                break;
            case "Scout(Clone)":
                bulletnum = 1;
                break;
            case "Sniper(Clone)":
                bulletnum = 2;
                break;
            case "Artillery(Clone)":
                bulletnum = 3;
                break;
            case "Heavy Weapon(Clone)":
                bulletnum = 4;
                break;
            case "Armoured(Clone)":
                bulletnum = 5;
                break;
            case "Tank(Clone)":
                bulletnum = 6;
                break;
            case "Helicopter(Clone)":
                bulletnum = 7;
                break;
            case "Raptor(Clone)":
                bulletnum = 8;
                break;
        }

        // 사거리 안에 적이 없거나 (=가장 가까운 적이 있고= 나와 적의 거리가 사거리보다 멀때)움직이는 상태로 만듬
        if (neareastObject == null || (/*neareastObject != null &&*/ Vector3.Distance(gameObject.transform.position,
            neareastObject.transform.position) > unitinfo.attackRange))
        {
            state = State.Move;
            StopCoroutine("CreateBullet");
            return true;
        }

        return false; //사거리 안에 적이있음
    }

    IEnumerator CreateBullet() // 일정시간마다 총알을 생성
    {
        yield return new WaitForSeconds(100 / unitinfo.attackSpeed); //공격속도에 따른 주기

        if (neareastObject == null)
            yield break;

        GameObject bullet = GameObject.Instantiate(Bullet[bulletnum]); //유닛에 따라 다른 총알쓰기
        bullet.transform.position = FirePos.transform.position; //총알의 위치를 발사 위치랑 일치
        Vector3 dir = neareastObject.transform.position - FirePos.transform.position; //제일 가까운애랑 나랑의 벡터
        dir.Normalize();
        bullet.transform.up = dir; //총알의 방향을 발사 방향이랑 일치
        bullet.GetComponent<JHW_BulletMove>().SetCreator(this); // 총알에게 나(총알을 쏜놈)를 알려주고싶다.

        bullet.layer = LayerMask.NameToLayer(unitinfo.isEnemy ? "EnemyBullet" : "PlayerBullet"); //총알의 레이어 설정

        isfire = false;
    }

    void UnitDie() //피가 닳면 죽는 함수
    {
        if (unitinfo.health <= 0)
        {
            Destroy(unitinfo.gameObject);
        }
    }

}
