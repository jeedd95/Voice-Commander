using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitManager : MonoBehaviour
{
    JHW_UnitInfo unitinfo;

    GameObject EnemyTarget;

    public GameObject[] Bullet; //�Ѿ�
    public GameObject FirePos; //�߻� ������
    int bulletnum; //�Ѿ��� ��ȣ
    bool isfire; // ���ݻ������� �ƴ���

    enum State // ���� ���¸ӽ�
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

        state = State.Move; // �ʱ� ����
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

    void UnitDetect() // ��Ÿ� �ȿ� enemy�±׸� ���� ���������� ���ݻ��·� ���� �ڵ�
    {

        if (Vector3.Distance(gameObject.transform.position,
            GameObject.FindWithTag("Enemy").transform.position) <= unitinfo.attackRange * 0.1f)
        {
            state = State.Attack;
        }
    }

    void UnitMove() // ���� �̵� �ϴ� �ڵ�
    {
        transform.position += transform.forward * unitinfo.moveSpeed * Time.deltaTime;
    }

    void UnitAttack()
    {
        transform.LookAt(GameObject.FindWithTag("Enemy").transform);  //������ ��Ÿ� �ȿ� ���� ���� �ٶ󺸰���

        switch (gameObject.name) // ���ֿ� ���� �ٸ� �Ѿ��� ������
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

    IEnumerator CreateBullet() // �����ð����� �Ѿ��� ����
    {
        yield return new WaitForSeconds(100/unitinfo.attackSpeed); //���ݼӵ��� ���� �ֱ�
        GameObject bullet = GameObject.Instantiate(Bullet[bulletnum]); //���ֿ� ���� �ٸ� �Ѿ˾���
        bullet.transform.position = FirePos.transform.position; //�Ѿ��� ��ġ�� �߻� ��ġ�� ��ġ
        bullet.transform.up = FirePos.transform.forward; //�Ѿ��� ������ �߻� �����̶� ��ġ

        isfire = false;
    }

    void UnitDie()
    {

    }

}
