using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
using UnityEngine.AI;
using UnityEngine.UI;

public class JHW_UnitManager : MonoBehaviour
{
    public JHW_UnitInfo unitinfo;
    // GameObject enemyTarget;
    NavMeshAgent navAgent;

    public GameObject[] Bullet; //총알
    public GameObject FirePos; //발사 포지션
    int bulletnum; //총알의 번호
    bool isfire; // 공격상태인지 아닌지
    public GameObject TeamCommand; //우리의 기지
    public GameObject enemyCommand; //적의 기지
    public Vector3 targetpos; //벽 뒤 좌표
    // public bool closedWall;
    public Collider[] cols;
    public List<Collider> colsTemp;
    [SerializeField]
    Collider[] cols2;


    Vector3 CapturePos;

    public enum State // 유닛 상태머신
    {
        Move,
        Hide,
        HideAttack,
        Attack,
        CaptureMove,
        CaputureDetect,
        CaptureAttack,
    }
    public State state;

    public GameObject neareastObject; //유닛이 공격을 시작했을때 가장 가까운 적
    public GameObject neareastWall; //유닛과 가장 가까운 엄폐물

    void Start()
    {
        colsTemp = new List<Collider>();

        unitinfo = GetComponent<JHW_UnitInfo>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.Warp(transform.position);
        navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance; //유닛 겹치기

        SetState(State.Move); // 초기 상태
        if (JHW_GameManager.instance.isCaptureCreateMode && unitinfo.isEnemy==false)
        {
            SetState(State.CaptureMove); //점령 모드로 생성된 유닛은 점령상태로 초기화
            unitinfo.isCaptureUnit = true;
            GameObject.Find("Canvas/CaptureMode").GetComponent<Toggle>().isOn = false;
        } 
    }

    public void SetState(State next)
    {
        // 플레이어의 유닛인데 숨는상태로 전환하려고할때
        if (unitinfo.isEnemy == false && next == State.Hide)
        {
            neareastWall = FindNearestObjectzByLayer("Wall");

            if (neareastWall == null) //벽이 하나도 없다
                return;
        }

        state = next;

        if (navAgent.isOnNavMesh)
        {
            if (state == State.Attack || state == State.HideAttack) //공격일때
            {
                navAgent.isStopped = true;  // 멈춘다
            }
            else
            {
                navAgent.isStopped = false;  // 이동한다
            }
            
        }

        if (state == State.Hide || state == State.HideAttack) //방어태세를 했을때
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.UseDefensive = true;
            }
        }
        else //방어태세가 아닐때
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.UseDefensive = false;
            }
        }

        if (state == State.HideAttack) //엄폐물에 숨어서 공격할때 트리거 on
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.isBehindWall = true;
            }
        }
        else //엄폐물에 숨지않았을때 트리거 off
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.isBehindWall = false;
            }
        }
        
    }

    void Update()
    {
        if (unitinfo.isEnemy)
        {
            //print(state);
            // print(navAgent.remainingDistance);
            //print("내 유닛 사거리 : " + unitinfo.ATTACK_RANGE);
            //print("내 유닛 공격속도 : " + unitinfo.ATTACK_SPEED);
            //print("내 유닛 이동속도 : " + unitinfo.MOVE_SPEED) ;
        }


        navAgent.speed = unitinfo.MOVE_SPEED; //nav mesh와 속도 동기화

        if(unitinfo.isEnemy==false && Vector3.Distance(gameObject.transform.position,targetpos) <=1)
        {
            unitinfo.isBehindWall = true;
        }
        else
        {
            unitinfo.isBehindWall = false;
        }

        switch (state)
        {
            case State.Move:
                UnitMove();
                UnitDetect(false);
                break;

            case State.CaptureMove:
                if (JHW_OrderManager.instance.DesinationAreaObj != null)
                {
                    UnitCaptureMoving();
                }
                break;

            case State.CaputureDetect:
                UnitCapturerDectecting();
                isfire = false;
                break;

            case State.CaptureAttack:
                if(UnitCaptureAttackting()==false)
                {
                    if (isfire == false) //총을 쏘고있지 않다면 총을 쏜다
                    {
                        isfire = true;
                        StartCoroutine("CreateBullet");
                    }
                }
                break;

            case State.HideAttack:
            case State.Attack:
                if (UnitAttack() == false)
                {
                    if (isfire == false) //총을 쏘고있지 않다면 총을 쏜다
                    {
                        isfire = true;
                        StartCoroutine("CreateBullet");
                    }

                    if (unitinfo.isEnemy == false) AllNearestObjectByLayer("EnemyTeam");
                    if (unitinfo.isEnemy == true) AllNearestObjectByLayer("PlayerTeam");
                }
                break;

            case State.Hide:
                UnitHide();

                // 벽뒤에 다 숨었다면(유닛이 벽이랑 가까이 있을때)
                if (Vector3.Distance(gameObject.transform.position, targetpos) == 0)
                {
                    // 공격상대를 찾고싶다.
                    // 찾았다면 HideAttack으로 상태를 전이하고싶다.
                    UnitDetect(true);
                }
                break;
        }
        UnitDie();
    }

    private void UnitCaptureMoving() // 어느 한 지점으로 이동유닛 주둔 상태 / 죽을때까지 홀딩 상태 / 적이 가까이 오면 감지하고 공격
    {
        CapturePos = JHW_OrderManager.instance.DesinationAreaObj.transform.position;

        navAgent.SetDestination(CapturePos);

        if (navAgent.velocity.sqrMagnitude > 0.1f * 0.1f && navAgent.remainingDistance <= 1.0f) //도달했다
        {
            print("주둔지역 도착");
            SetState(State.CaputureDetect);
        }
    }

    void UnitCapturerDectecting()
    {
        navAgent.isStopped = true;
        StopCoroutine("CreateBullet");
        neareastObject = FindNearestObjectzByLayer("EnemyTeam");

        if(neareastObject!=null && Vector3.Distance(gameObject.transform.position, neareastObject.transform.position) <= unitinfo.ATTACK_RANGE)
        {
            SetState(State.CaptureAttack);
        }
    }

    bool UnitCaptureAttackting()
    {
        if (neareastObject != null) transform.LookAt(neareastObject.transform);

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
        } //유닛마다 다른 총알


        if (neareastObject == null || (Vector3.Distance(gameObject.transform.position,
            neareastObject.transform.position) > unitinfo.ATTACK_RANGE)) 
            //가까이 있는 오브젝트가 없거나 가까운 오브젝트가 사거리 안에 없을때
        {
            SetState(State.CaputureDetect); // 탐지상태로 간다
            return true;
        }

        return false;
    }


    float currentTime;
    float detectTime = 0.01f; //감지시간
    void UnitDetect(bool isHide) // 사거리 안에 enemy레이어를 가진 적이있으면 공격상태로 가는 코드
    {
        currentTime += Time.deltaTime;
        if (currentTime > detectTime)
        {
            if (unitinfo.isEnemy == false) neareastObject = FindNearestObjectzByLayer("EnemyTeam");
            if (unitinfo.isEnemy == true) neareastObject = FindNearestObjectzByLayer("PlayerTeam");

            currentTime = 0;


            //가장 가까운 오브젝트가 있고 거리가 공격 사거리 안에 있으면
            if (neareastObject != null && Vector3.Distance(gameObject.transform.position,
                neareastObject.transform.position) <= unitinfo.ATTACK_RANGE)
            {
                StopCoroutine("CreateBullet");

                if (isHide == true)
                {
                    SetState(State.HideAttack); //숨어있으면 hideattack
                }
                else
                {
                    SetState(State.Attack); //안 숨어잇으면 그냥 attack
                }
                isfire = false;
            }
        }
    }


    public GameObject FindNearestObjectzByLayer(string layer) //가장 가까운 오브젝트 레이어로 찾기
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        cols = Physics.OverlapSphere(transform.position, unitinfo.ATTACK_RANGE, layerMask); //공격 사거리 안에있는 적 인식
        if (layer == "Wall") cols = Physics.OverlapSphere(transform.position, float.MaxValue, layerMask); // 엄폐물을 찾을때는 전범위로 인식
        float dist = float.MaxValue;
        int chooseIndex = -1;

        for (int i = 0; i < cols.Length; i++) //가장 가까운 애의 인덱스 부여
        {
            float temp = Vector3.Distance(transform.position, cols[i].transform.position);
            
                if (temp < dist)
                {
                    dist = temp;
                    chooseIndex = i;
                }
        }

        //if (!unitinfo.canSkyAttack && cols[i].gameObject.GetComponentInParent<JHW_UnitInfo>().isAirForce)
        //{
        //    print("11111111111111111111");
        //}
        //if (!unitinfo.canGroundAttack && !cols[i].gameObject.GetComponentInParent<JHW_UnitInfo>().isAirForce)
        //{
        //    print("222222222222222222");
        //}


        if (chooseIndex == -1) //사거리 안에 있는 적이 없음
        {
            return null;
        }
        else
        {
            return cols[chooseIndex].gameObject; // = nearestObject
        }
    }

    public void AllNearestObjectByLayer(string layer)
    {
        //모든 적들을 찾았다 근데 배열의 첫번째 요소가 커맨드센터이고 두번째가 병력일경우는 공격을 병력한테 한다
        //nearestObject를 두번째 요소로 한다
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        cols2 = Physics.OverlapSphere(transform.position, unitinfo.ATTACK_RANGE, layerMask);

        if(cols2.Length>1) //cols2가 없지 않다
        {
            if (cols2[0].name == "EnemyCommand"  || cols2[0].name == "EnemyTurret")
            {
                if (cols2[1] != null)
                {
                    neareastObject = cols2[1].gameObject;
                    
                }
            }

            if (cols2[0].name == "TeamCommand" || cols2[0].name == "TeamTurret")
            {
                if (cols2[1] != null)
                {
                    neareastObject = cols2[1].gameObject;
                }
            }
        }

        if (cols2.Length <1)
        {
            return;
        }
        //int chooseIndex = -1;

        //if (chooseIndex == -1) //사거리 안에 있는 적이 없음
        //{
        //    return null;
        //}
        //return cols[chooseIndex].gameObject;
    }

    void UnitMove() // nav mesh를 이용하여 인식한 적 방향으로 가는 코드
    {
        //// transform.position += transform.forward * unitinfo.moveSpeed * Time.deltaTime; //앞으로만 가는 코드
        Vector3 offset = Vector3.zero;

        if (unitinfo.isEnemy == false) //우리팀일때
        {
            offset = enemyCommand.transform.position;
           // transform.LookAt(Vector3.right);
        }
        if (unitinfo.isEnemy == true) //적팀일때
        {
            offset = TeamCommand.transform.position;
           // transform.LookAt(Vector3.left);
        }
        if (navAgent.isOnNavMesh) navAgent.SetDestination(offset); // 각자의 적진으로 이동
    }

    void UnitHide()
    {
        neareastWall = FindNearestObjectzByLayer("Wall");
        if (neareastWall == null)
            return;
        targetpos = new Vector3(neareastWall.transform.position.x - 5, transform.position.y, neareastWall.transform.position.z);

        if (unitinfo.isEnemy == false) // 플레이어 팀일때 벽의 왼쪽으로 숨음
        {
            navAgent.SetDestination(targetpos);
        }
    }


    bool UnitAttack()
    {
        //유닛이 사거리 안에 들어온 적을 바라보게함
        if (neareastObject != null)
        {
            transform.LookAt(neareastObject.transform);
        }

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

        // 사거리 안에 적이 없거나 나와 적의 거리가 사거리보다 멀때 움직이는 상태로 만듬
        if (neareastObject == null || (Vector3.Distance(gameObject.transform.position,
            neareastObject.transform.position) > unitinfo.ATTACK_RANGE))
        {
            SetState(State.Move); //move상태로 만들고 움직이게
            if (navAgent.isOnNavMesh)
                navAgent.isStopped = false;
            StopCoroutine("CreateBullet");
            return true;
        }

        return false; //사거리 안에 적이있음
    }

    IEnumerator CreateBullet() // 일정시간마다 총알을 생성
    {
        yield return new WaitForSeconds(100 / unitinfo.ATTACK_SPEED); //공격속도에 따른 주기

        if (neareastObject == null) //가까이에 있는 유닛이 없다면 기다리는걸 끝냄
            yield break;

        GameObject bullet = Instantiate(Bullet[bulletnum]); //유닛에 따라 다른 총알쓰기
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
            if (unitinfo.isEnemy == true) //죽은애가 적일경우
            {
                JHW_GameManager.instance.Score += unitinfo.score;
                JHW_GameManager.instance.currentExp += unitinfo.exp;
                // print("죽은 유닛 :" + unitinfo.gameObject + "얻은점수 : " + unitinfo.score);
                JHW_UnitFactory.instance.enemyUnits.Remove(this); // 적 유닛 리스트에서 삭제

            }
            else //죽은 애가 우리팀일 경우
            {
                JHW_UnitFactory.instance.myUnits.Remove(this); //내 유닛 리스트에서 삭제
               // JHW_GameManager.instance.wholePopulationLimit -= JHW_GameManager.instance.currentPopulationArray[index];
               // JHW_GameManager.instance.currentPopulation--; //인구수 -1

                for (int i = 0; i < System.Enum.GetValues(typeof(JHW_GameManager.UnitType)).Length; i++)
                {
                    if (unitinfo.unitName == (System.Enum.GetName(typeof(JHW_GameManager.UnitType), i)))
                    {
                        JHW_GameManager.instance.currentPopulationArray[i] -= JHW_GameManager.instance._UnitLoad[i];
                        JHW_GameManager.instance.populationSum = false;
                    }
                }
                
            }
            Destroy(unitinfo.gameObject); //죽으면 곧바로 destroy한다
        }
    }

}
