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
        //���ϴ� xz ��ǥ�� ����.
        //--xz��ġ�� �Է� �޴´�.

        rb.velocity = Vector3.right;
    }

}