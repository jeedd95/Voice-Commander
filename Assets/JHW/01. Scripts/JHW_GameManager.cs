using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_GameManager : MonoBehaviour
{
    public static JHW_GameManager instance;
    private void Awake()
    {
        instance = this;
    }

}

