using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_ExploArea : MonoBehaviour
{
    float defensiveDamage;
    JHW_BulletMove bm;
    JHW_BulletMove bulletinfo;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>();

            if (hitObj.isEnemy == false && hitObj.UseDefensive == true)
            { //�����Ѿְ� ���̰�, �´»���� �� �����̰� ����¼��� �Ѱ�������� ����������
                print("������ �氨");
                defensiveDamage = 0.8f;
            }
            else
            {
                defensiveDamage = 1f;
            }

            if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 0.5f;
            if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 0.75f;
            if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 1;
        }
    }

    public void SetCreator(JHW_BulletMove unit)  //���� �������� �Ѿ� �� �˷��ִ� �ڵ�
    {
        this.bm = unit;
        bulletinfo = unit.GetComponent<JHW_BulletMove>(); //�� ���� ���� ����
        this.damage = bulletinfo.damage;
        //this.accuracyRate = unitInfo.ACCURACYRATE;
        // attackerName = unit.gameObject.name;
    }
}
