using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject bullet;
    public GameObject capsule;
    public bool Buffon;
    public bool temp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    GameObject a= Instantiate(bullet);
        //    a.transform.position = transform.position;

        //}
        print("buff : "+Buffon);

        Buffon = false; //��ҿ��� �������� ������

        if (temp)
        {
            Buffon = true;
        }
    }



    //IEnumerator OnTriggerEnter(Collider other)
    //{

    //    if (other.name == "Capsule")
    //    {
    //        yield return new WaitForSecondsRealtime(5); //���� �����ð��� ������ �ӽ��� �÷��׸� Ʈ��� ������ش�

    //        temp = true;
    //    }

    //}

    public float curretTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Capsule")
        {
            curretTime += Time.deltaTime * 0.1f;

            if (curretTime >= 5)
            {
                temp = true;
                curretTime = 0;
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Capsule")
        {
            temp = false;
            //curretTime = 0;

        }
    }

    private void OnDestroy()
    {
        //������� �ڱ� �ȿ� ������ �־��ٸ�
    }



}
