using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitFactory : MonoBehaviour
{
    public Transform[] MyCreatePoint; //우리 생성포인트 3개
    public Transform[] EnemyCreatePoint; //적팀 생성 포인트 3개

    public GameObject[] Units; //유닛 프리팹들
    JHW_UnitInfo unitInfo;

    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            unitInfo =  Units[i].GetComponent<JHW_UnitInfo>();
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateUnit();
        }
    }

    void CreateUnit()
    {
        int randomNum = Random.Range(0, 3); // 3개중에 하나를 선택해서 생성
        int randomNum2 = Random.Range(0, 9); // 9개 중에 하나를 선택해서 생성

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9번까지의 유닛중에 하나 생성
        Transform mcp = MyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
        SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다

        if (unitInfo.isEnemy == true) //만약 유닛이 적군이라면 글로벌 좌표의 왼쪽으로 간다
        {
            SelectUnit.transform.forward = Vector3.left; 
        }
        if (unitInfo.isEnemy == false) //만약 유닛이 우리팀이라면 글로벌 좌표의 오른쪽으로 간다
        {
            SelectUnit.transform.forward = Vector3.right; 
        }
    }
}
