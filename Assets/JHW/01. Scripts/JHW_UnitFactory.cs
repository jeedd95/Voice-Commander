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

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            CreateUnit2();
        }
    }

    public void CreateUnit() //아군을 생성하는 코드
    {
        int randomNum = Random.Range(0, 3); // 3개중에 하나를 선택해서 생성
        int randomNum2 = Random.Range(0, 9); // 9개 중에 하나를 선택해서 생성

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9번까지의 유닛중에 하나 생성
        SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy=false; //아군이다
        SelectUnit.tag = "Player";
        SelectUnit.layer = LayerMask.NameToLayer("PlayerTeam");
        Collider col = SelectUnit.GetComponentInChildren<Collider>();
        col.gameObject.tag = SelectUnit.tag;
        col.gameObject.layer = SelectUnit.layer;
        Transform mcp = MyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
        SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다
    }

    public void CreateUnit2() //적을 생성하는 코드
    {
        int randomNum = Random.Range(0, 3); // 3개중에 하나를 선택해서 생성
        int randomNum2 = Random.Range(0, 9); // 9개 중에 하나를 선택해서 생성

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9번까지의 유닛중에 하나 생성
        SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy = true; //적이다
        SelectUnit.tag = "Enemy";
        SelectUnit.layer = LayerMask.NameToLayer("EnemyTeam");
        Collider col = SelectUnit.GetComponentInChildren<Collider>();
        col.gameObject.tag = SelectUnit.tag;
        col.gameObject.layer = SelectUnit.layer;
        Transform mcp = EnemyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
        SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다
    }
}
