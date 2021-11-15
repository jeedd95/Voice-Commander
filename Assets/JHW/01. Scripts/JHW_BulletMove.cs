using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_BulletMove : MonoBehaviour
{
    float speed=50;

    JHW_UnitInfo unitInfo;

    private void Start()
    {
       unitInfo= GameObject.Find("UnitFactory").GetComponent<JHW_UnitInfo>();
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
}
