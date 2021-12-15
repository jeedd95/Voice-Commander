using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Anim : MonoBehaviour
{
    Animator animator;
    JHW_UnitManager um;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        um = GetComponent<JHW_UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(um.unitinfo.unitName=="RifleMan")
        {
            switch (um.state)
            {
                case JHW_UnitManager.State.Move:
                    animator.SetInteger("Flag", 0);
                    break;
                case JHW_UnitManager.State.Attack:
                    animator.SetInteger("Flag", 1);
                    break;
            }
        }
        /*============================================*/
        if(um.unitinfo.unitName == "Scout")
        {

        }
        /*============================================*/
        if(um.unitinfo.unitName == "Sniper")
        {
            switch (um.state)
            {
                case JHW_UnitManager.State.Move:
                    animator.SetInteger("Flag", 0);
                    break;
                case JHW_UnitManager.State.Attack:
                    animator.SetInteger("Flag", 1);
                    break;
            }
        }
        /*============================================*/
        if(um.unitinfo.unitName == "Artillery")
        {
            switch (um.state)
            {
                case JHW_UnitManager.State.Move:
                    animator.SetInteger("Flag", 0);
                    break;
            }
        }
    }
}
