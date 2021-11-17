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

    public GameObject[] Bullet; //�Ѿ�
    public GameObject FirePos; //�߻� ������
    int bulletnum; //�Ѿ��� ��ȣ
    bool isfire; // ���ݻ������� �ƴ���
    public GameObject TeamCommand; //�츮�� ����
    public GameObject enemyCommand; //���� ����

    enum State // ���� ���¸ӽ�
    {
        Move,
        Hide,
        Attack,
        Die
    }
    State state;

    public GameObject neareastObject; //������ ������ ���������� ���� ����� ��

    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.Warp(transform.position);

        state = State.Move; // �ʱ� ����

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
    float detectTime = 0.01f; //�����ð�
    void UnitDetect() // ��Ÿ� �ȿ� enemy�±׸� ���� ���������� ���ݻ��·� ���� �ڵ�
    {

        currentTime += Time.deltaTime;
        if (currentTime > detectTime)
        {
            if (unitinfo.isEnemy == false) neareastObject = FindNearestObjectzByLayer("EnemyTeam");
            if (unitinfo.isEnemy == true) neareastObject = FindNearestObjectzByLayer("PlayerTeam");
            currentTime = 0;

            //���� ����� ������Ʈ�� �ְ� �Ÿ��� ���� ��Ÿ� �ȿ� ������
            if (neareastObject != null && Vector3.Distance(gameObject.transform.position,
                neareastObject.transform.position) <= unitinfo.attackRange)
            {
                state = State.Attack;
                isfire = false;
            }
        }
    }

    public GameObject FindNearestObjectzByLayer(string layer) //���� ����� ������Ʈ ���̾�� ã��
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        var cols = Physics.OverlapSphere(transform.position, unitinfo.attackRange, layerMask); //���� ��Ÿ� �ȿ��ִ� �� �ν�

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
        navAgent.SetDestination(offset);
    }

    void UnitHide()
    {

    }


    bool UnitAttack()
    {
        navAgent.SetDestination(transform.position); //�����Ҷ� ���ڸ��� ����

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

        // ��Ÿ� �ȿ� ���� ���ų� (=���� ����� ���� �ְ�= ���� ���� �Ÿ��� ��Ÿ����� �ֶ�)�����̴� ���·� ����
        if (neareastObject == null || (/*neareastObject != null &&*/ Vector3.Distance(gameObject.transform.position,
            neareastObject.transform.position) > unitinfo.attackRange))
        {
            state = State.Move;
            StopCoroutine("CreateBullet");
            return true;
        }

        return false; //��Ÿ� �ȿ� ��������
    }

    IEnumerator CreateBullet() // �����ð����� �Ѿ��� ����
    {
        yield return new WaitForSeconds(100 / unitinfo.attackSpeed); //���ݼӵ��� ���� �ֱ�

        if (neareastObject == null)
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
            Destroy(unitinfo.gameObject);
            if(unitinfo.isEnemy==true)
            {
                JHW_GameManager.instance.Score += unitinfo.score;
                print("���� ���� :" + unitinfo.gameObject + "�������� : " + unitinfo.score);
            }
        }
    }

}
