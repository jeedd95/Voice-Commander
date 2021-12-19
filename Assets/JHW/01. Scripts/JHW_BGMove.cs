using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JHW_BGMove : MonoBehaviour
{
    public Material[] BGs;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(1, 14);
        gameObject.GetComponent<Image>().material = BGs[index-1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
