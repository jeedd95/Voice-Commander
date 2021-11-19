using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        CreateUnit();

        if (Input.GetKeyDown(KeyCode.Alpha2)) //2번키를 누르면 적 랜덤 생성
        {
            CreateUnit2();
        }
    }


    public void CreateUnit() //아군을 생성하는 코드
    {
        int randomNum = Random.Range(0, 3); // 3개중에 하나를 선택해서 생성 (생성 지역)
        //int randomNum2 = Random.Range(0, 9); // 9개 중에 하나를 선택해서 생성 
        int i = -1;

        if (Input.anyKeyDown)
        {
            switch (Input.inputString)
            {
                case "q":
                    i = 0; // RifleMan
                    break;
                case "w": //Scout
                    i = 1;
                    break;
                case "e": //Sniper
                    i = 2;
                    break;
                case "r": //Artillery
                    i = 3;
                    break;
                case "t": //Armoured
                    i = 4;
                    break;
                case "y": //Tank
                    i = 5;
                    break;
                case "u": //Helicopter
                    i = 6;
                    break;
                case "i": //HeavyWeapon
                    i = 7;
                    break;
                case "o": //Raptor
                    i = 8;
                    break;
            }

            if (i == -1)
            {
                return;
            }

            if (JHW_GameManager.instance.Gold >= Units[i].GetComponent<JHW_UnitInfo>().price)
            {
                JHW_GameManager.instance.Gold -= Units[i].GetComponent<JHW_UnitInfo>().price; //전체 골드에서 유닛의 값만 큼 뺀다

                GameObject SelectUnit = Instantiate(Units[i]); // 1~9번까지의 유닛중에 하나 생성
                SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy = false; //아군이다
                SelectUnit.tag = "Player";
                SelectUnit.layer = LayerMask.NameToLayer("PlayerTeam");
                SelectUnit.GetComponent<NavMeshAgent>().speed = SelectUnit.GetComponent<JHW_UnitInfo>().moveSpeed;
                Collider col = SelectUnit.GetComponentInChildren<Collider>(); //생성한 유닛은 부모가 빈오브젝트임(콜라이더 없음)
                col.gameObject.tag = SelectUnit.tag;
                col.gameObject.layer = SelectUnit.layer;

                Transform mcp = MyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
                SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다
            }
            else
            {
                print("돈이 부족합니다");
            }
        }
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
