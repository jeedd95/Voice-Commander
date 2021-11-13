using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    float speed=50;

    public GameObject UnitInfo;
    JHW_UnitInfo unitInfo;

    private void Start()
    {
       unitInfo= UnitInfo.GetComponent<JHW_UnitInfo>();
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }
}
