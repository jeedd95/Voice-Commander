using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    float speed=50;

    JHW_UnitManager unit; // ÃÑ¾ËÀ» ½ð unit ÄÄÆ÷³ÍÆ®

    private void Start()
    {
      
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wall") || other.CompareTag("Enemy"))
        {
            Destroy(gameObject); //º®¿¡ ºÎµúÈ÷¸é ÃÑ¾Ë »èÁ¦
        }
        
    }

    public void SetCreator(JHW_UnitManager unit)
    {
        this.unit = unit;
    }
}
