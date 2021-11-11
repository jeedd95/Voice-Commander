using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitMove : MonoBehaviour
{
    JHW_UnitInfo unit;

    void Start()
    {
        unit =GetComponent<JHW_UnitInfo>();
    }

    void Update()
    {
        UnitMove();
    }


    void UnitMove()
    {
        transform.position += transform.forward * unit.moveSpeed * Time.deltaTime;
    }
}
