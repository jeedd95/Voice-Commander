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


    public void UnitMove() // 매시간 앞으로 이동하는 코드
    {
        transform.position += transform.forward * unitinfo.MOVE_SPEED * Time.deltaTime;
    }
}
