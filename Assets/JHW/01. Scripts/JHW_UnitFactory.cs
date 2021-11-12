using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitFactory : MonoBehaviour
{

    public Transform[] MyCreatePoint; //우리 생성포인트 3개
    public Transform[] EnemyCreatePoint; //적팀 생성 포인트 3개

    public GameObject[] Units; //유닛 프리팹들

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateUnit();
        }
    }

    public void CreateUnit()
    {
        int randomNum = Random.Range(0, 2); // 3개중에 하나를 선택해서 생성
        int randomNum2 = Random.Range(0, 9); // 9개 중에 하나를 선택해서 생성

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9번까지의 유닛중에 하나 생성
        Transform mcp = MyCreatePoint[randomNum];
        SelectUnit.transform.position = mcp.position;
        //if(isEnemy == true)
        //{
            SelectUnit.transform.forward = Vector3.right;
        //}
        //if(isEnemy ==false)
        //{
        //    SelectUnit.transform.forward = Vector3.right;
        //}
    }
}
