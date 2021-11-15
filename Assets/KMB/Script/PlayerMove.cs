using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //이동속도 변수 
    public float moveSpeed = 7f; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //WASD키를 누렴 입력하여 캐릭터를 그 방향으로 이동시키고 싶다. 

        //1. 사용자의 입력을 받는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        //2. 이동방향을 설정한다. 
        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        //2-1. 메인 카메라를 기준으로 방향을 변환한다.
        dir = Camera.main.transform.TransformDirection(dir);

        //3.이동속도에 맞춰 이동한다.
        // p = p0+vt

        transform.position += dir * moveSpeed * Time.deltaTime; 
        
    }
}
