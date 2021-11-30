using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_Wall : MonoBehaviour
{
    public float wallHp;
    public bool CanDestroy=true; //부술수 있는지

    void Start()
    {
        wallHp = 300f;
    }

    // Update is called once per frame
    void Update()
    {
        WallDamaged();
    }

    public void WallDamaged() //벽의 hp가 0이하로 되면 벽 삭제
    {
        if(wallHp <=0 && CanDestroy==true)
        {
            Destroy(gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //   if(other.CompareTag("Bullet"))
    //    {
    //        wallHp -= 10;
    //    }
    //}
}
