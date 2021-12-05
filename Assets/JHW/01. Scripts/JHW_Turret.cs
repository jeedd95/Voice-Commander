using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ĳ��Ʈ�� ����Ͽ� ������� ������ �����ϰ� �����ϰ� �ϰ�ʹ�
//�ʿ� ��� : ���� ĳ��Ʈ, �����ð� 0.1f , �߻� �ӵ� ������ , ��Ÿ�, �������� ���������� ü��

public class JHW_Turret : MonoBehaviour
{
    //JHW_UnitManager um;
    public GameObject firePos; //��� ��
    RaycastHit hit; //������

    public float turretHp;
    float MaxDistance = 40; //�ִ� ��Ÿ� = �����Ÿ�
    bool isfire;

    Vector3 dir;
    [SerializeField]
    GameObject nearestUnit;
    [SerializeField]
    Collider[] cols;

    enum State
    {
        Detect,
        Attack
    }
    State state;


    // Start is called before the first frame update
    void Start()
    {
        turretHp = 5000f;
        //um = JHW_UnitFactory.instance.GetComponent<JHW_UnitManager>();
        state = State.Detect;
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.name == "TeamTurret") print(state);
        if(hit.transform !=null) print(hit.transform);

        //if (this.name == "EnemyTurret") print("���� �ͷ� ���� : " + state);

        switch (state)
        {
            case State.Detect:
                TurretDetect();
                break;
            case State.Attack:
                ShootRay();
                if(isfire ==false)
                {
                    isfire = true;
                    StartCoroutine("FireRay");
                }
                break;
        }

        TurretDamaged();
    }

    void TurretDetect()
    {
        isfire = false;
        if (this.name == "TeamTurret") nearestUnit =  FindNearestObjectzByLayer("EnemyTeam");
        if (this.name == "EnemyTurret") nearestUnit =  FindNearestObjectzByLayer("PlayerTeam");

        if (nearestUnit != null) state = State.Attack;
    }

    public void TurretDamaged() //���� hp�� 0���Ϸ� �Ǹ� �� ����
    {
        if (turretHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ShootRay()
    {

        if (nearestUnit == null || Vector3.Distance(firePos.transform.position, nearestUnit.transform.position) > MaxDistance)
        {
            state = State.Detect;
            StopCoroutine("FireRay");
        }
    }

    IEnumerator FireRay()
    {
        dir = nearestUnit.transform.position - firePos.transform.position;

        yield return new WaitForSeconds(2);

        if (nearestUnit == null) //�����̿� �ִ� ������ ���ٸ� ��ٸ��°� ����
            yield break;

        if (Physics.Raycast(firePos.transform.position, dir, out hit, MaxDistance))
        {
            Debug.DrawRay(firePos.transform.position , dir , Color.red,0.1f);
            if(hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Player"))
            {
                hit.transform.gameObject.GetComponent<JHW_UnitInfo>().health -= 50; //������ 50 ����������
            }
        }

        isfire = false;
    }


    GameObject FindNearestObjectzByLayer(string layer) //���� ����� ������Ʈ ���̾�� ã��
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        cols = Physics.OverlapSphere(transform.position, MaxDistance, layerMask); //���� ��Ÿ� �ȿ��ִ� �� �ν�

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
    }
}
