using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_PlayerSkill : MonoBehaviour
{
    public float bombDamage=300;

    //public GameObject AirPlane;
    //public GameObject Bomb;
    //Transform StartPoint;
    //Transform FinishPoint;


    //bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        //StartPoint = GameObject.Find("StartPoint").transform;
        //FinishPoint = GameObject.Find("FinishPoint").transform;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //void PlayerSkill_Bomb_Anim()
    //{
    //    Vector3 fireStart = new Vector3(JHW_OrderManager.instance.DesinationAreaObj.transform.position.x-5, JHW_OrderManager.instance.DesinationAreaObj.transform.position.y+15, JHW_OrderManager.instance.DesinationAreaObj.transform.position.z);
    //    GameObject airplane = Instantiate(AirPlane);
    //   // airplane.transform.position = StartPoint.transform.position;
    //    airplane.transform.position = Vector3.MoveTowards(transform.position, fireStart, 10 * Time.deltaTime);
    //}


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if (gameObject.name == "BombHit")
            {
                other.GetComponentInParent<JHW_UnitInfo>().health -= bombDamage;
            }
            if (gameObject.name == "Smoke")
            {
                other.GetComponentInParent<JHW_UnitInfo>().inSmoke = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponentInParent<JHW_UnitInfo>().inSmoke = false;

    }
}
