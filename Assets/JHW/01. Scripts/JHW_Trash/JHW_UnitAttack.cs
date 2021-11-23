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
        //만약 내 위치와 enemy 태그를 가진 적과 위치의 거리가 내 사거리보다 같거나 작을때
        //멈춰서
        //공격
        //그 적이 제거됬을경우 다시 기지로 공격
        if (Vector3.Distance(gameObject.transform.position,
            GameObject.FindWithTag("Enemy").transform.position) <= unitinfo.ATTACK_RANGE * 0.1f)
        {
            print("사거리 이내 적 포착");
            gameObject.GetComponent<JHW_UnitMove>().enabled = false;
        }
    }
}
