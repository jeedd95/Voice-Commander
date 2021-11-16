using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_Wall : MonoBehaviour
{
    public float wallHp;

    void Start()
    {
        wallHp = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        WallDamaged();
    }

    void WallDamaged() //���� hp�� 0���Ϸ� �Ǹ� �� ����
    {
        if(wallHp <=0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.CompareTag("Bullet"))
        {
            wallHp -= 10;
        }
    }
}
