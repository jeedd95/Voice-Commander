using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitAttack : MonoBehaviour
{
    JHW_UnitInfo unitinfo;
    JHW_UnitMove unitmove;

    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        UnitAttack();
    }

    void UnitAttack()
    {
        //���� �� ��ġ�� enemy �±׸� ���� ���� ��ġ�� �Ÿ��� �� ��Ÿ����� ���ų� ������
        //���缭
        //����
        //�� ���� ���ŉ������ �ٽ� ������ ����
        if (Vector3.Distance(gameObject.transform.position,
            GameObject.FindWithTag("Enemy").transform.position) <= unitinfo.ATTACK_RANGE * 0.1f)
        {
            print("��Ÿ� �̳� �� ����");
            gameObject.GetComponent<JHW_UnitMove>().enabled = false;
        }
    }
}
