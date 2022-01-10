using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    public float moveSpeed;
    int count=0;
    Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //target = new Vector3(transform.position.x, transform.position.y - 21.2f, transform.position.z + 9.41444f);
        CameraMove();
    }

    void CameraMove()
    {
        if (Input.GetKey(KeyCode.A) && transform.position.x > -986.6161f )
        {
            this.transform.Translate(-moveSpeed, 0, 0);
        }
        if(Input.GetKey(KeyCode.D) && transform.position.x < -857.7236f)
        {
            this.transform.Translate(moveSpeed, 0, 0);
        }

        if((Input.GetKeyDown(KeyCode.W) || Input.GetAxis("Mouse ScrollWheel") > 0) && count ==0)
        {
            //transform.position = Vector3.Lerp(transform.position, target, 0.01f);
            //transform.position = Vector3.SmoothDamp(transform.position, new Vector3(transform.position.x, transform.position.y - 21.2f, transform.position.z + 9.41444f), ref velo, 1f);
            this.transform.Translate(0, -21.2f, 9.41444f,Space.World);
            count = 1;
        }
        if((Input.GetKeyDown(KeyCode.S) || Input.GetAxis("Mouse ScrollWheel") < 0) && count==1)
        {
            this.transform.Translate(0, 21.2f, -9.41444f, Space.World);
            count = 0;
        }

    }
}
