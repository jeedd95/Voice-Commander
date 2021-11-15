using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //�̵��ӵ� ���� 
    public float moveSpeed = 7f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WASDŰ�� ���� �Է��Ͽ� ĳ���͸� �� �������� �̵���Ű�� �ʹ�. 

        //1. ������� �Է��� �޴´�.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //2. �̵������� �����Ѵ�. 
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1. ���� ī�޶� �������� ������ ��ȯ�Ѵ�.
        dir = Camera.main.transform.TransformDirection(dir);

        //3.�̵��ӵ��� ���� �̵��Ѵ�.
        // p = p0+vt

        transform.position += dir * moveSpeed * Time.deltaTime; 
        
    }
}
