using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitMove : MonoBehaviour
{
    JHW_UnitInfo unitinfo;

    void Start()
    {
        unitinfo = GetComponent<JHW_UnitInfo>();
    }

    void Update()
    {
        UnitMove();
    }


    public void UnitMove() // �Žð� ������ �̵��ϴ� �ڵ�
    {
        transform.position += transform.forward * unitinfo.MOVE_SPEED * Time.deltaTime;
    }
}
