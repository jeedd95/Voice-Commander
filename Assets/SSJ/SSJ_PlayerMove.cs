using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSJ_PlayerMove : MonoBehaviour
{
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Go()
    {
        //원하는 xz 좌표로 간다.
        //--xz위치를 입력 받는다.

        rb.velocity = Vector3.right;
    }

}
