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

    public GameObject[] Bullet; //�Ѿ�
    public GameObject FirePos; //�߻� ������
    int bulletnum; //�Ѿ��� ��ȣ
    bool isfire; // ���ݻ������� �ƴ���
    public GameObject TeamCommand; //�츮�� ����
    public GameObject enemyCommand; //���� ����
    public Vector3 targetpos; //�� �� ��ǥ
    // public bool closedWall;
    public Collider[] cols;
    public List<Collider> colsTemp;
    [SerializeField]
    Collider[] cols2;


    Vector3 CapturePos;

    public enum State // ���� ���¸ӽ�
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

    public GameObject neareastObject; //������ ������ ���������� ���� ����� ��
    public GameObject neareastWall; //���ְ� ���� ����� ����

    void Start()
    {
        colsTemp = new List<Collider>();

        unitinfo = GetComponent<JHW_UnitInfo>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.Warp(transform.position);
        navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance; //���� ��ġ��

        SetState(State.Move); // �ʱ� ����
        if (JHW_GameManager.instance.isCaptureCreateMode && unitinfo.isEnemy==false)
        {
            SetState(State.CaptureMove); //���� ���� ������ ������ ���ɻ��·� �ʱ�ȭ
            unitinfo.isCaptureUnit = true;
            GameObject.Find("Canvas/CaptureMode").GetComponent<Toggle>().isOn = false;
        } 
    }

    public void SetState(State next)
    {
        // �÷��̾��� �����ε� ���»��·� ��ȯ�Ϸ����Ҷ�
        if (unitinfo.isEnemy == false && next == State.Hide)
        {
            neareastWall = FindNearestObjectzByLayer("Wall");

            if (neareastWall == null) //���� �ϳ��� ����
                return;
        }

        state = next;

        if (navAgent.isOnNavMesh)
        {
            if (state == State.Attack || state == State.HideAttack) //�����϶�
            {
                navAgent.isStopped = true;  // �����
            }
            else
            {
                navAgent.isStopped = false;  // �̵��Ѵ�
            }
            
        }

        if (state == State.Hide || state == State.HideAttack) //����¼��� ������
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.UseDefensive = true;
            }
        }
        else //����¼��� �ƴҶ�
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.UseDefensive = false;
            }
        }

        if (state == State.HideAttack) //���󹰿� ��� �����Ҷ� Ʈ���� on
        {
            for (int i = 0; i < JHW_GameManager.instance.hidingUnits.Count; i++)
            {
                JHW_GameManager.instance.hidingUnits[i].unitinfo.isBehindWall = true;
            }
        }
        else //���󹰿� �����ʾ����� Ʈ���� off
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
            //print("�� ���� ��Ÿ� : " + unitinfo.ATTACK_RANGE);
            //print("�� ���� ���ݼӵ� : " + unitinfo.ATTACK_SPEED);
            //print("�� ���� �̵��ӵ� : " + unitinfo.MOVE_SPEED) ;
        }


        navAgent.speed = unitinfo.MOVE_SPEED; //nav mesh�� �ӵ� ����ȭ

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
                    if (isfire == false) //���� ������� �ʴٸ� ���� ���
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
                    if (isfire == false) //���� ������� �ʴٸ� ���� ���
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

                // ���ڿ� �� �����ٸ�(������ ���̶� ������ ������)
                if (Vector3.Distance(gameObject.transform.position, targetpos) == 0)
                {
                    // ���ݻ�븦 ã��ʹ�.
                    // ã�Ҵٸ� HideAttack���� ���¸� �����ϰ�ʹ�.
                    UnitDetect(true);
                }
                break;
        }
        UnitDie();
    }

    private void UnitCaptureMoving() // ��� �� �������� �̵����� �ֵ� ���� / ���������� Ȧ�� ���� / ���� ������ ���� �����ϰ� ����
    {
        CapturePos = JHW_OrderManager.instance.DesinationAreaObj.transform.position;

        navAgent.SetDestination(CapturePos);

        if (navAgent.velocity.sqrMagnitude > 0.1f * 0.1f && navAgent.remainingDistance <= 1.0f) //�����ߴ�
        {
            print("�ֵ����� ����");
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

        switch (gameObject.name) // ���ֿ� ���� �ٸ� �Ѿ��� ������
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
        } //���ָ��� �ٸ� �Ѿ�


        if (neareastObject == null || (Vector3.Distance(gameObject.transform.position,
            neareastObject.transform.position) > unitinfo.ATTACK_RANGE)) 
            //������ �ִ� ������Ʈ�� ���ų� ����� ������Ʈ�� ��Ÿ� �ȿ� ������
        {
            SetState(State.CaputureDetect); // Ž�����·� ����
            return true;
        }

        return false;
    }


    float currentTime;
    float detectTime = 0.01f; //�����ð�
    void UnitDetect(bool isHide) // ��Ÿ� �ȿ� enemy���̾ ���� ���������� ���ݻ��·� ���� �ڵ�
    {
        currentTime += Time.deltaTime;
        if (currentTime > detectTime)
        {
            if (unitinfo.isEnemy == false) neareastObject = FindNearestObjectzByLayer("EnemyTeam");
            if (unitinfo.isEnemy == true) neareastObject = FindNearestObjectzByLayer("PlayerTeam");

            currentTime = 0;


            //���� ����� ������Ʈ�� �ְ� �Ÿ��� ���� ��Ÿ� �ȿ� ������
            if (neareastObject != null && Vector3.Distance(gameObject.transform.position,
                neareastObject.transform.position) <= unitinfo.ATTACK_RANGE)
            {
                StopCoroutine("CreateBullet");

                if (isHide == true)
                {
                    SetState(State.HideAttack); //���������� hideattack
                }
                else
                {
                    SetState(State.Attack); //�� ���������� �׳� attack
                }
                isfire = false;
            }
        }
    }


    public GameObject FindNearestObjectzByLayer(string layer) //���� ����� ������Ʈ ���̾�� ã��
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        cols = Physics.OverlapSphere(transform.position, unitinfo.ATTACK_RANGE, layerMask); //���� ��Ÿ� �ȿ��ִ� �� �ν�
        if (layer == "Wall") cols = Physics.OverlapSphere(transform.position, float.MaxValue, layerMask); // ������ ã������ �������� �ν�
        float dist = float.MaxValue;
        int chooseIndex = -1;

        for (int i = 0; i < cols.Length; i++) //���� ����� ���� �ε��� �ο�
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


        if (chooseIndex == -1) //��Ÿ� �ȿ� �ִ� ���� ����
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
        //��� ������ ã�Ҵ� �ٵ� �迭�� ù��° ��Ұ� Ŀ�ǵ弾���̰� �ι�°�� �����ϰ��� ������ �������� �Ѵ�
        //nearestObject�� �ι�° ��ҷ� �Ѵ�
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        cols2 = Physics.OverlapSphere(transform.position, unitinfo.ATTACK_RANGE, layerMask);

        if(cols2.Length>1) //cols2�� ���� �ʴ�
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

        //if (chooseIndex == -1) //��Ÿ� �ȿ� �ִ� ���� ����
        //{
        //    return null;
        //}
        //return cols[chooseIndex].gameObject;
    }

    void UnitMove() // nav mesh�� �̿��Ͽ� �ν��� �� �������� ���� �ڵ�
    {
        //// transform.position += transform.forward * unitinfo.moveSpeed * Time.deltaTime; //�����θ� ���� �ڵ�
        Vector3 offset = Vector3.zero;

        if (unitinfo.isEnemy == false) //�츮���϶�
        {
            offset = enemyCommand.transform.position;
           // transform.LookAt(Vector3.right);
        }
        if (unitinfo.isEnemy == true) //�����϶�
        {
            offset = TeamCommand.transform.position;
           // transform.LookAt(Vector3.left);
        }
        if (navAgent.isOnNavMesh) navAgent.SetDestination(offset); // ������ �������� �̵�
    }

    void UnitHide()
    {
        neareastWall = FindNearestObjectzByLayer("Wall");
        if (neareastWall == null)
            return;
        targetpos = new Vector3(neareastWall.transform.position.x - 5, transform.position.y, neareastWall.transform.position.z);

        if (unitinfo.isEnemy == false) // �÷��̾� ���϶� ���� �������� ����
        {
            navAgent.SetDestination(targetpos);
        }
    }


    bool UnitAttack()
    {
        //������ ��Ÿ� �ȿ� ���� ���� �ٶ󺸰���
        if (neareastObject != null)
        {
            transform.LookAt(neareastObject.transform);
        }

        switch (gameObject.name) // ���ֿ� ���� �ٸ� �Ѿ��� ������
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

        // ��Ÿ� �ȿ� ���� ���ų� ���� ���� �Ÿ��� ��Ÿ����� �ֶ� �����̴� ���·� ����
        if (neareastObject == null || (Vector3.Distance(gameObject.transform.position,
            neareastObject.transform.position) > unitinfo.ATTACK_RANGE))
        {
            SetState(State.Move); //move���·� ����� �����̰�
            if (navAgent.isOnNavMesh)
                navAgent.isStopped = false;
            StopCoroutine("CreateBullet");
            return true;
        }

        return false; //��Ÿ� �ȿ� ��������
    }

    IEnumerator CreateBullet() // �����ð����� �Ѿ��� ����
    {
        yield return new WaitForSeconds(100 / unitinfo.ATTACK_SPEED); //���ݼӵ��� ���� �ֱ�

        if (neareastObject == null) //�����̿� �ִ� ������ ���ٸ� ��ٸ��°� ����
            yield break;

        GameObject bullet = Instantiate(Bullet[bulletnum]); //���ֿ� ���� �ٸ� �Ѿ˾���
        bullet.transform.position = FirePos.transform.position; //�Ѿ��� ��ġ�� �߻� ��ġ�� ��ġ
        Vector3 dir = neareastObject.transform.position - FirePos.transform.position; //���� �����ֶ� ������ ����
        dir.Normalize();
        bullet.transform.up = dir; //�Ѿ��� ������ �߻� �����̶� ��ġ
        bullet.GetComponent<JHW_BulletMove>().SetCreator(this); // �Ѿ˿��� ��(�Ѿ��� ���)�� �˷��ְ�ʹ�.

        bullet.layer = LayerMask.NameToLayer(unitinfo.isEnemy ? "EnemyBullet" : "PlayerBullet"); //�Ѿ��� ���̾� ����

        isfire = false;
    }

    void UnitDie() //�ǰ� ��� �״� �Լ�
    {
        if (unitinfo.health <= 0)
        {
            if (unitinfo.isEnemy == true) //�����ְ� ���ϰ��
            {
                JHW_GameManager.instance.Score += unitinfo.score;
                JHW_GameManager.instance.currentExp += unitinfo.exp;
                // print("���� ���� :" + unitinfo.gameObject + "�������� : " + unitinfo.score);
                JHW_UnitFactory.instance.enemyUnits.Remove(this); // �� ���� ����Ʈ���� ����

            }
            else //���� �ְ� �츮���� ���
            {
                JHW_UnitFactory.instance.myUnits.Remove(this); //�� ���� ����Ʈ���� ����
               // JHW_GameManager.instance.wholePopulationLimit -= JHW_GameManager.instance.currentPopulationArray[index];
               // JHW_GameManager.instance.currentPopulation--; //�α��� -1

                for (int i = 0; i < System.Enum.GetValues(typeof(JHW_GameManager.UnitType)).Length; i++)
                {
                    if (unitinfo.unitName == (System.Enum.GetName(typeof(JHW_GameManager.UnitType), i)))
                    {
                        JHW_GameManager.instance.currentPopulationArray[i] -= JHW_GameManager.instance._UnitLoad[i];
                        JHW_GameManager.instance.populationSum = false;
                    }
                }
                
            }
            Destroy(unitinfo.gameObject); //������ ��ٷ� destroy�Ѵ�
        }
    }

}
