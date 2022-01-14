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
    public Transform[] MyCreatePoint_Sky; //우리 공중 생성포인트 3개
    public Transform[] EnemyCreatePoint; //적팀 생성 포인트 3개
    public Transform[] EnemyCreatePoint_Sky;

    public GameObject[] Units; //유닛 프리팹들
    public JHW_UnitInfo units; //유닛 프리팹들의 유닛인포
    public List<JHW_UnitManager> myUnits;
    public List<JHW_UnitManager> enemyUnits;

    //bool producing = false;


    void Start()
    {
    }

    void Update()
    {
        //if (!JHW_OrderManager.instance.inputFieldOrder.isFocused)
        //{
        //    CreateUnit();

        //}
        if (Input.GetKeyDown(KeyCode.Alpha2)) //2번키를 누르면 적 랜덤 생성
        {
            CreateUnit2();
        }


        if (!CoroutineFlag)
        {
            CoroutineFlag = true;
            StartCoroutine("EnemyProduct");
        }

    }


    int unitIndex = -1; //아래 함수에서 쓰이는 변수
    public void CreateUnit(int unitIndex) //아군을 생성하는 코드
    {
        //if (Input.anyKeyDown) //유닛에 해당하는 버튼을 누른다
        //{
        //switch (Input.inputString)
        // {
        //case "q":
        //        unitIndex = 0; // RifleMan
        //        break;
        //    case "w": //Scout
        //        unitIndex = 1;
        //        break;
        //    case "e": //Sniper
        //        unitIndex = 2;
        //        break;
        //    case "r": //Artillery
        //        unitIndex = 3;
        //        break;
        //    case "t": //Armoured
        //        unitIndex = 4;
        //        break;
        //    case "y": //Tank
        //        unitIndex = 5;
        //        break;
        //    case "u": //Helicopter
        //        unitIndex = 6;
        //        break;
        //    case "i": //HeavyWeapon
        //        unitIndex = 7;
        //        break;
        //    case "o": //Raptor
        //        unitIndex = 8;
        //        break;
        //    default:
        //        unitIndex = -1;
        //        break;
        //}

        //if (unitIndex == -1)
        //{
        //    return;
        //}

        units = Units[unitIndex].GetComponent<JHW_UnitInfo>(); //각 유닛의 정보를 가져온다

        //(전체) 가격 검사 , 인구 수 검사
        if (JHW_GameManager.instance.Gold >= units.price && JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance._UnitLoad[unitIndex] <= JHW_GameManager.instance.wholePopulationLimit)
        {
            if (JHW_GameManager.instance.CoolDownReady[unitIndex]) //쿨타임까지 준비됬을때
            {
                InstantiateUnit(unitIndex); //유닛을 생성한다
                ValueChanger(unitIndex); //인구수를 올린다 + 쿨타임을 돌린다
                CoolTimeSetter(unitIndex);
            }
            else //쿨타임이 준비 안됬을때
            {
                print("쿨타임 중입니다 : " + (System.Enum.GetName(typeof(JHW_GameManager.UnitType), unitIndex)) + " " + JHW_GameManager.instance.currentCool[unitIndex] + "초 남았습니다");
            }

            #region 안쓰는 코드
            //if (i == 0 && JHW_GameManager.instance.CoolDownReady[0])
            //{  
            //         JHW_GameManager.instance.currentPopulationArray[0] += JHW_GameManager.instance._UnitLoad[0];
            //         JHW_GameManager.instance.populationSum = false;
            //}
            ////else if (i == 0 && JHW_GameManager.instance.RifleManCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[0])
            ////{
            ////    print("유닛 인구수가 부족합니다");
            ////    return;
            ////}
            //else if (i == 0 && !JHW_GameManager.instance.CoolDownReady[0])
            //{
            //    print("쿨타임 중입니다 : " + JHW_GameManager.instance.currentCool[0] + "초 남았습니다");
            //    return;
            //}


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
            #endregion

        }
        else if (JHW_GameManager.instance.Gold < units.price) //가격 검사 false
        {
            print("돈이 부족합니다");
        }
        else if (JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance._UnitLoad[unitIndex] > JHW_GameManager.instance.wholePopulationLimit) //인구 수 검사 false
        {
            print("최대 인구수가 부족합니다");
        }
        //  }
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

        if (SelectUnit.GetComponent<JHW_UnitInfo>().isAirForce == true)
        {
            Transform mcp = EnemyCreatePoint_Sky[randomNum]; // 1~3번 생성포인트 중에 하나 생성
            SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다
        }
        else
        {
            Transform mcp = EnemyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
            SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다

        }
        enemyUnits.Add(SelectUnit.GetComponent<JHW_UnitManager>());
    }



    void ValueChanger(int index)
    {
        //if (unitIndex == index)
        //{
        JHW_GameManager.instance.currentPopulationArray[index] += JHW_GameManager.instance._UnitLoad[index];
        JHW_GameManager.instance.populationSum = false;
        JHW_GameManager.instance.CoolDownReady[index] = false; //생산할 수 없게 쿨타임레디를 거짓으로 만들어준다
        //}

    }

    void CoolTimeSetter(int index)
    {
        if (!JHW_GameManager.instance.isBuff_CoolDown) JHW_GameManager.instance.currentCool[index] = JHW_GameManager.instance._cooldown[index]; //원래 쿨로 돌려준다
        else JHW_GameManager.instance.currentCool[index] = JHW_GameManager.instance._cooldown[index] * 0.75f;
        //print("쿨타임 시작");
        // JHW_GameManager.instance.currentCool[unitIndex] -= Time.deltaTime;
        StartCoroutine("BB", index);
    }

    IEnumerator BB(int index)
    {
        JHW_GameManager.instance.CoolDownReady[index] = false;

        while (JHW_GameManager.instance.currentCool[index] > 0)
        {
            JHW_GameManager.instance.currentCool[index] -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        // print("쿨타임 끝");
        JHW_GameManager.instance.CoolDownReady[index] = true;
    }

    void InstantiateUnit(int unitIndex)
    {
        int randomNum = Random.Range(0, 3); // 3개중에 하나를 선택해서 생성 (생성 지역)

        JHW_GameManager.instance.Gold -= units.price; // 전체 골드에서 유닛의 값만 큼 뺀다

        // JHW_GameManager.instance.currentPopulation++; //생산하면 인구수를 1 늘린다

        GameObject SelectUnit = Instantiate(Units[unitIndex]); // 1~9번까지의 유닛중에 하나 생성
        SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy = false; //아군이다
        SelectUnit.tag = "Player";
        SelectUnit.layer = LayerMask.NameToLayer("PlayerTeam");
        SelectUnit.GetComponent<NavMeshAgent>().speed = SelectUnit.GetComponent<JHW_UnitInfo>().MOVE_SPEED;
        Collider col = SelectUnit.GetComponentInChildren<Collider>(); //생성한 유닛은 부모가 빈오브젝트임(콜라이더 없음)
        col.gameObject.tag = SelectUnit.tag;
        col.gameObject.layer = SelectUnit.layer;



        if (SelectUnit.GetComponent<JHW_UnitInfo>().isAirForce == true)
        {
            Transform mcp = MyCreatePoint_Sky[randomNum]; // 1~3번 생성포인트 중에 하나 생성
            SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다
        }
        else
        {
            Transform mcp = MyCreatePoint[randomNum]; // 1~3번 생성포인트 중에 하나 생성
            SelectUnit.transform.position = mcp.position; // 유닛들을 생성 포인트에 놓는다
        }

        myUnits.Add(SelectUnit.GetComponent<JHW_UnitManager>()); //생성하면 리스트에 넣는다
    }


    //[Range(1, 100)]
    //public float Chance_3;
    //[Range(1, 100)]
    //public float Chance_2;
    //[Range(1, 100)]
    //public float Chance_1;
    //[Range(1, 100)]
    //public float Chance_0; //맨 처음 초기확률 70퍼센트

    public float second = 3;
    bool CoroutineFlag;
    public float[] Chances;

    IEnumerator EnemyProduct() //코루틴으로 생산타이밍 3초 -> 1.5초 까지 줄어듦
    {
        yield return new WaitForSeconds(second);
        int RandomChoice = Random.Range(1, 101);

        //유닛을 생산하기
        if (JHW_GameManager.instance.WholePlayTime >= 0 && JHW_GameManager.instance.WholePlayTime < 300) //플레이타임 0~5분
        {
            print("0~5분");
            Chances[1] = 100 - Chances[0];
        }
        if (JHW_GameManager.instance.WholePlayTime >= 300 && JHW_GameManager.instance.WholePlayTime < 600) //5~10분
        {
            print("5~10분");

            if(Chances[0] <= 0 && Chances[1] > 0)
            {
                Chances[2] = 100 - Chances[1];
            }
            else
            {
                Chances[2] = (100 - Chances[0]) * 0.5f;
                Chances[1] = Chances[2];
            }

        }
        if (JHW_GameManager.instance.WholePlayTime >= 600) //10분 이후
        {
            print("10분 이후");
            if (Chances[0] <= 0 && Chances[1] <= 0 && Chances[2]>0)
            {
                Chances[3] = 100 - Chances[2];
            }
            if(Chances[0] <=0 &&Chances[1]>0 )
            {
                Chances[3] = (100 - Chances[1]) * 0.5f;
                Chances[2] = Chances[3];
            }
            if(Chances[0] > 0 )
            {
                Chances[3] = (100 - Chances[0]) * 0.5f * 0.5f;
                Chances[2] = Chances[3];
                Chances[1] = (100 - Chances[0]) * 0.5f;
            }
        }

        if (second > 1.5) //생산 시간을 1.5초를 하한선으로 함
        {
            second -= 0.005f;
        }
        else
        {
            second = 1.5f;
        }

        if(Chances[0]>0)
        {
            Chances[0] -= 0.1f;
        }
        else if(Chances[1]>0)
        {
            Chances[0] = 0;
            Chances[1] -= 0.1f;
        }
        else if(Chances[2]>0)
        {
            Chances[0] = 0;
            Chances[1] = 0;
            Chances[2] -= 0.1f;
        }
        else if(Chances[2]<=0)
        {
            Chances[2] = 0;
        }

        CoroutineFlag = false;
    }

}


