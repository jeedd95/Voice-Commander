using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���� ĳ��Ʈ�� ����Ͽ� ������� ������ �����ϰ� �����ϰ� �ϰ�ʹ�
//�ʿ� ��� : ���� ĳ��Ʈ, �����ð� 0.1f , �߻� �ӵ� ������ , ��Ÿ�, �������� ���������� ü��

public class JHW_Turret : MonoBehaviour
{
    JHW_UnitManager um;

    public float turretHp;
    public GameObject firePos;
    RaycastHit hit; //������
    float MaxDistance; //�ִ� ��Ÿ� = �����Ÿ�
    [SerializeField]
    GameObject nearestUnit;

    // Start is called before the first frame update
    void Start()
    {
        turretHp = 5000f;
        um = JHW_UnitFactory.instance.GetComponent<JHW_UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShootRay();
        }
        TurretDamaged();

    }

    void TurretDetect()
    {
        if (this.name == "TeamTurret") um.FindNearestObjectzByLayer("EnemyTeam");
        if (this.name == "EnemyTurret") um.FindNearestObjectzByLayer("PlayerTeam");
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
        Debug.DrawRay(firePos.transform.position, transform.forward * MaxDistance, Color.red, 0.5f);
         //
    }
    
}
