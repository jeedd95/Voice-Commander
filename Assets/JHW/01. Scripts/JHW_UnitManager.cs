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

    enum State // ���� ���¸ӽ�
    {
        Move,
        Hide,
        Attack,
        Die
    }
    State state;

    // Start is called before the first frame update
    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();

        state = State.Move; // �ʱ� ����
    }

    // Update is called once per frame
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

    void UnitAttack() // �����ϴ� �ڵ� + �Ѿ� ����
    {
        switch (gameObject.name) // ���ֿ� ���� �ٸ� �Ѿ��� ������
        {
            case "RifleMan(Clone)":
                bulletnum = 0;
                break;
        }
        StartCoroutine(CreateBullet());
    }

    IEnumerator CreateBullet() // �����ð����� �Ѿ��� ����(���ݼӵ�)
    {

        transform.LookAt(GameObject.FindWithTag("Enemy").transform);  //������ ��Ÿ� �ȿ� ���� ���� �ٶ󺸰���
        GameObject bullet = GameObject.Instantiate(Bullet[bulletnum]);
        bullet.transform.position = FirePos.transform.position; //�Ѿ��� ��ġ�� �߻� ��ġ�� ��ġ
        bullet.transform.up = FirePos.transform.forward; //�Ѿ��� ������ �߻� �����̶� ��ġ
        yield return new WaitForSecondsRealtime(2f);

    }

    void UnitDie()
    {

    }

}
