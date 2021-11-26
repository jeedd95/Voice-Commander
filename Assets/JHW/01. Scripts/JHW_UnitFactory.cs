using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JHW_UnitFactory : MonoBehaviour
{
    public static JHW_UnitFactory instance;
    void Awake()
    {
        instance = this;
        myUnits = new List<JHW_UnitManager>(); // 내 유닛 리스트
        enemyUnits = new List<JHW_UnitManager>(); //적 유닛 리스트
    }


    public Transform[] MyCreatePoint; //우리 생성포인트 3개
    public Transform[] EnemyCreatePoint; //적팀 생성 포인트 3개

    public GameObject[] Units; //유닛 프리팹들
    public JHW_UnitInfo units; //유닛 프리팹들의 유닛인포
    public List<JHW_UnitManager> myUnits;
    public List<JHW_UnitManager> enemyUnits;

    bool producing=false;


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

            units = Units[i].GetComponent<JHW_UnitInfo>();
            if (JHW_GameManager.instance.Gold >= units.price && JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance.currentPopulationArray[i] <= JHW_GameManager.instance.wholePopulationLimit) //가지고 있는 골드가 뽑으려는 유닛 가격보다 많고, 현재 인구 + 누른 부하수가 총 인구보다 낮을때
            {
                
                if (i == 0 && JHW_GameManager.instance.CoolDownReady[0])
                {   if(producing==false)
                    {
                        JHW_GameManager.instance.currentPopulationArray[0] += JHW_GameManager.instance._UnitLoad[0];
                        producing = true;
                    }

                }
                //else if (i == 0 && JHW_GameManager.instance.RifleManCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[0])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                else if (i == 0 && !JHW_GameManager.instance.CoolDownReady[0])
                {
                    print("쿨타임 중입니다 : " + JHW_GameManager.instance.currentCool[0] + "초 남았습니다");
                    return;
                }


                //if (i == 1 && JHW_GameManager.instance.ScoutCurrentPopulation < JHW_GameManager.instance.unitMaxCount[1])
                //{
                //    JHW_GameManager.instance.ScoutCurrentPopulation++;
                //}
                //else if (i == 1 && JHW_GameManager.instance.ScoutCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[1])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 2 && JHW_GameManager.instance.SniperCurrentPopulation < JHW_GameManager.instance.unitMaxCount[2])
                //{
                //    JHW_GameManager.instance.SniperCurrentPopulation++;
                //}
                //else if (i == 2 && JHW_GameManager.instance.SniperCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[2])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 3 && JHW_GameManager.instance.ArtilleryCurrentPopulation < JHW_GameManager.instance.unitMaxCount[3])
                //{
                //    JHW_GameManager.instance.ArtilleryCurrentPopulation++;
                //}
                //else if (i == 3 && JHW_GameManager.instance.ArtilleryCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[3])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 4 && JHW_GameManager.instance.HeavyWeaponCurrentPopulation < JHW_GameManager.instance.unitMaxCount[4])
                //{
                //    JHW_GameManager.instance.HeavyWeaponCurrentPopulation++;
                //}
                //else if (i == 4 && JHW_GameManager.instance.HeavyWeaponCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[4])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 5 && JHW_GameManager.instance.ArmouredCurrentPopulation < JHW_GameManager.instance.unitMaxCount[5])
                //{
                //    JHW_GameManager.instance.ArmouredCurrentPopulation++;
                //}
                //else if (i == 5 && JHW_GameManager.instance.ArmouredCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[5])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 6 && JHW_GameManager.instance.TankCurrentPopulation < JHW_GameManager.instance.unitMaxCount[6])
                //{
                //    JHW_GameManager.instance.TankCurrentPopulation++;
                //}
                //else if (i == 6 && JHW_GameManager.instance.TankCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[6])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 7 && JHW_GameManager.instance.HelicopterCurrentPopulation < JHW_GameManager.instance.unitMaxCount[7])
                //{
                //    JHW_GameManager.instance.HelicopterCurrentPopulation++;
                //}
                //else if (i == 7 && JHW_GameManager.instance.HelicopterCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[7])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}
                //if (i == 8 && JHW_GameManager.instance.RaptorCurrentPopulation < JHW_GameManager.instance.unitMaxCount[8])
                //{
                //    JHW_GameManager.instance.RaptorCurrentPopulation++;
                //}
                //else if (i == 8 && JHW_GameManager.instance.RaptorCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[8])
                //{
                //    print("유닛 인구수가 부족합니다");
                //    return;
                //}

                JHW_GameManager.instance.Gold -= units.price; //전체 골드에서 유닛의 값만 큼 뺀다

                // JHW_GameManager.instance.currentPopulation++; //생산하면 인구수를 1 늘린다

                GameObject SelectUnit = Instantiate(Units[i]); // 1~9번까지의 유닛중에 하나 생성
                SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy = false; //아군이다
                SelectUnit.tag = "Player";
                SelectUnit.layer = LayerMask.NameToLayer("PlayerTeam");
                SelectUnit.GetComponent<NavMeshAgent>().speed = SelectUnit.GetComponent<JHW_UnitInfo>().MOVE_SPEED;
                Collider col = SelectUnit.GetComponentInChildren<Collider>(); //생성한 유닛은 부모가 빈오브젝트임(콜라이더 없음)
                col.gameObject.tag = SelectUnit.tag;
                col.gameObject.layer = SelectUnit.layer;

                Transform mcp = MyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
                SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다

                myUnits.Add(SelectUnit.GetComponent<JHW_UnitManager>()); //생성하면 리스트에 넣는다
            }
            else if (JHW_GameManager.instance.Gold < units.price)
            {
                print("돈이 부족합니다");
            }
            else if (JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance.currentPopulationArray[i] > JHW_GameManager.instance.wholePopulationLimit)
            {
                print("최대 인구수가 부족합니다");
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

        enemyUnits.Add(SelectUnit.GetComponent<JHW_UnitManager>());
    }

}


