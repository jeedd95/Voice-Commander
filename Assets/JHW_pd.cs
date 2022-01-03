using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;


public class JHW_pd : MonoBehaviour
{
    PlayableDirector pd;
    // Start is called before the first frame update
    void Start()
    {
        pd= gameObject.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {

        //if (pd.time == 374.0667)
        //{
        //    ToplayScene();
        //}
        if (pd.state == PlayState.Paused)
        {
            ToplayScene();
        }
    }
    void ToplayScene()
    {
        SceneManager.LoadScene("JHW_TestScene+Map");
    }
}
