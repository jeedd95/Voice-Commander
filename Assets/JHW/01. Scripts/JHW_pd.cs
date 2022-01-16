using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class JHW_pd : MonoBehaviour
{
    PlayableDirector pd;
    public GameObject antena;
    public GameObject trunc;

    // Start is called before the first frame update
    void Start()
    {
        pd= gameObject.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pd.time>=140 && pd.time<=210)
        {
            antena.transform.Rotate(Vector3.up, Time.deltaTime*20);
        }

        if(pd.time >=228 && pd.time<=250)
        {
            trunc.transform.Rotate(Vector3.right, -Time.deltaTime);
        }


        if (pd.time >= 370)
        {
            ToplayScene();
        }
        
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    print(pd.time);
        //}
    }
    public void ToplayScene()
    {
        
        SceneManager.LoadScene("JHW_TestScene+Map");
    }

}
