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
    float currentTime;

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
                currentTime += Time.deltaTime;
                if(currentTime>(1f/unitinfo.attackSpeed))
                {
                    CreateBullet();
                    currentTime = 0f;
                }
                break;

            case State.Die:
                break;
        }
        print(state);
    }

    void UnitDetect() // ��Ÿ� �ȿ� enemy�±׸� ���� ���������� ���ݻ��·� ���� �ڵ�
    {

        if (Vector3.Distance(gameObject.transform.position,
            GameObject.FindWithTag("Enemy").transform.position) <= unitinfo.attackRange * 0.1f)
        {
            print("��Ÿ� �̳� �� ����");
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
            case "RifleMan(Clone)":
                bulletnum = 0;
                break;
        }
    }

    void CreateBullet() // �����ð����� �Ѿ��� ����(���ݼӵ�)
    {
        GameObject bullet = GameObject.Instantiate(Bullet[bulletnum]);
        bullet.transform.position = FirePos.transform.position; //�Ѿ��� ��ġ�� �߻� ��ġ�� ��ġ
        bullet.transform.up = FirePos.transform.forward; //�Ѿ��� ������ �߻� �����̶� ��ġ
    }

    void UnitDie()
    {

    }

}
