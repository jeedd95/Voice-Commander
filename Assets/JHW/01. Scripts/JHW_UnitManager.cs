using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Linq;
using UnityEngine.AI;

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
    Vector3 targetpos; //�� �� ��ǥ

    public enum State // ���� ���¸ӽ�
    {
        Move,
        Hide,
        HideAttack,
        Attack,
        Die
    }
    public State state;

    public GameObject neareastObject; //������ ������ ���������� ���� ����� ��
    public GameObject neareastWall; //���ְ� ���� ����� ����

    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.Warp(transform.position);
        navAgent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance; //���� ��ġ��

        SetState(State.Move); // �ʱ� ����

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
        if (unitinfo.isEnemy == false)
        {
           // print(state);
           //print("�� ���� ��Ÿ� : " + unitinfo.ATTACK_RANGE);
           //print("�� ���� ���ݼӵ� : " + unitinfo.ATTACK_SPEED);
           //print("�� ���� �̵��ӵ� : " + unitinfo.MOVE_SPEED) ;

            // print(state);
        }

        UnitDie();
        navAgent.speed = unitinfo.MOVE_SPEED;

        switch (state)
        {
            case State.Move:
                UnitMove();
                UnitDetect(false);
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
        var cols = Physics.OverlapSphere(transform.position, unitinfo.ATTACK_RANGE, layerMask); //���� ��Ÿ� �ȿ��ִ� �� �ν�
        if (layer == "Wall") cols = Physics.OverlapSphere(transform.position, 500, layerMask); // ������ ã������ �������� �ν�
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

        if (chooseIndex == -1) //��Ÿ� �ȿ� �ִ� ���� ����
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
        //.FirstOrDefault(); //List�� ù��° ��Ҹ� ��ȯ, ��������� null�� ��ȯ
    }

    void UnitMove() // nav mesh�� �̿��Ͽ� �ν��� �� �������� ���� �ڵ�
    {
        //// transform.position += transform.forward * unitinfo.moveSpeed * Time.deltaTime; //�����θ� ���� �ڵ�
        Vector3 offset = Vector3.zero;

        if (unitinfo.isEnemy == false) //�츮���϶�
        {
            offset = enemyCommand.transform.position;
            transform.LookAt(Vector3.right);
        }
        if (unitinfo.isEnemy == true) //�����϶�
        {
            offset = TeamCommand.transform.position;
            transform.LookAt(Vector3.left);
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
        //transform.LookAt(GameObject.FindWithTag("Enemy").transform);

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

        GameObject bullet = GameObject.Instantiate(Bullet[bulletnum]); //���ֿ� ���� �ٸ� �Ѿ˾���
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
                // print("���� ���� :" + unitinfo.gameObject + "�������� : " + unitinfo.score);
                JHW_UnitFactory.instance.enemyUnits.Remove(this); // �� ���� ����Ʈ���� ����
            }
            else
            {
                JHW_UnitFactory.instance.myUnits.Remove(this); //�� ���� ����Ʈ���� ����
               //if()///////////======================================= 11.29 �� ���� �α��� ���̱�
               // JHW_GameManager.instance.wholePopulationLimit -= JHW_GameManager.instance.currentPopulationArray[index];
                JHW_GameManager.instance.currentPopulation--; //�α��� -1
            }

            Destroy(unitinfo.gameObject); //������ ��ٷ� destroy�Ѵ�
        }
    }

}
