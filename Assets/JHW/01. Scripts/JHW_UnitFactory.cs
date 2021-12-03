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
        myUnits = new List<JHW_UnitManager>(); // �� ���� ����Ʈ
        enemyUnits = new List<JHW_UnitManager>(); //�� ���� ����Ʈ
    }


    public Transform[] MyCreatePoint; //�츮 ��������Ʈ 3��
    public Transform[] EnemyCreatePoint; //���� ���� ����Ʈ 3��

    public GameObject[] Units; //���� �����յ�
    public JHW_UnitInfo units; //���� �����յ��� ��������
    public List<JHW_UnitManager> myUnits;
    public List<JHW_UnitManager> enemyUnits;

    //bool producing = false;


    void Start()
    {
    }

    void Update()
    {
        if (!JHW_OrderManager.instance.inputFieldOrder.isFocused)
        {
            CreateUnit();

            if (Input.GetKeyDown(KeyCode.Alpha2)) //2��Ű�� ������ �� ���� ����
            {
                CreateUnit2();
            }
        }
    }


    int unitIndex = -1; //�Ʒ� �Լ����� ���̴� ����
    public void CreateUnit() //�Ʊ��� �����ϴ� �ڵ�
    {
        if (Input.anyKeyDown) //���ֿ� �ش��ϴ� ��ư�� ������
        {
            switch (Input.inputString)
            {
                case "q":
                    unitIndex = 0; // RifleMan
                    break;
                case "w": //Scout
                    unitIndex = 1;
                    break;
                case "e": //Sniper
                    unitIndex = 2;
                    break;
                case "r": //Artillery
                    unitIndex = 3;
                    break;
                case "t": //Armoured
                    unitIndex = 4;
                    break;
                case "y": //Tank
                    unitIndex = 5;
                    break;
                case "u": //Helicopter
                    unitIndex = 6;
                    break;
                case "i": //HeavyWeapon
                    unitIndex = 7;
                    break;
                case "o": //Raptor
                    unitIndex = 8;
                    break;
                default:
                    unitIndex = -1;
                    break;
            }

            if (unitIndex == -1)
            {
                return;
            }

            units = Units[unitIndex].GetComponent<JHW_UnitInfo>(); //�� ������ ������ �����´�

            //(��ü) ���� �˻� , �α� �� �˻�
            if (JHW_GameManager.instance.Gold >= units.price && JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance._UnitLoad[unitIndex] <= JHW_GameManager.instance.wholePopulationLimit)
            {
                if (JHW_GameManager.instance.CoolDownReady[unitIndex]) //��Ÿ�ӱ��� �غ������
                {
                    InstantiateUnit(unitIndex); //������ �����Ѵ�
                    ValueChanger(unitIndex); //���ݰ� �α����� �ø���
                    CoolTimeSetter(unitIndex);
                }
                else //��Ÿ���� �غ� �ȉ�����
                {
                    print("��Ÿ�� ���Դϴ� : " + (System.Enum.GetName(typeof(JHW_GameManager.UnitType), unitIndex)) + " " + JHW_GameManager.instance.currentCool[unitIndex] + "�� ���ҽ��ϴ�");
                }

                #region �Ⱦ��� �ڵ�
                //if (i == 0 && JHW_GameManager.instance.CoolDownReady[0])
                //{  
                //         JHW_GameManager.instance.currentPopulationArray[0] += JHW_GameManager.instance._UnitLoad[0];
                //         JHW_GameManager.instance.populationSum = false;
                //}
                ////else if (i == 0 && JHW_GameManager.instance.RifleManCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[0])
                ////{
                ////    print("���� �α����� �����մϴ�");
                ////    return;
                ////}
                //else if (i == 0 && !JHW_GameManager.instance.CoolDownReady[0])
                //{
                //    print("��Ÿ�� ���Դϴ� : " + JHW_GameManager.instance.currentCool[0] + "�� ���ҽ��ϴ�");
                //    return;
                //}


                //if (i == 1 && JHW_GameManager.instance.ScoutCurrentPopulation < JHW_GameManager.instance.unitMaxCount[1])
                //{
                //    JHW_GameManager.instance.ScoutCurrentPopulation++;
                //}
                //else if (i == 1 && JHW_GameManager.instance.ScoutCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[1])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 2 && JHW_GameManager.instance.SniperCurrentPopulation < JHW_GameManager.instance.unitMaxCount[2])
                //{
                //    JHW_GameManager.instance.SniperCurrentPopulation++;
                //}
                //else if (i == 2 && JHW_GameManager.instance.SniperCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[2])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 3 && JHW_GameManager.instance.ArtilleryCurrentPopulation < JHW_GameManager.instance.unitMaxCount[3])
                //{
                //    JHW_GameManager.instance.ArtilleryCurrentPopulation++;
                //}
                //else if (i == 3 && JHW_GameManager.instance.ArtilleryCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[3])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 4 && JHW_GameManager.instance.HeavyWeaponCurrentPopulation < JHW_GameManager.instance.unitMaxCount[4])
                //{
                //    JHW_GameManager.instance.HeavyWeaponCurrentPopulation++;
                //}
                //else if (i == 4 && JHW_GameManager.instance.HeavyWeaponCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[4])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 5 && JHW_GameManager.instance.ArmouredCurrentPopulation < JHW_GameManager.instance.unitMaxCount[5])
                //{
                //    JHW_GameManager.instance.ArmouredCurrentPopulation++;
                //}
                //else if (i == 5 && JHW_GameManager.instance.ArmouredCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[5])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 6 && JHW_GameManager.instance.TankCurrentPopulation < JHW_GameManager.instance.unitMaxCount[6])
                //{
                //    JHW_GameManager.instance.TankCurrentPopulation++;
                //}
                //else if (i == 6 && JHW_GameManager.instance.TankCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[6])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 7 && JHW_GameManager.instance.HelicopterCurrentPopulation < JHW_GameManager.instance.unitMaxCount[7])
                //{
                //    JHW_GameManager.instance.HelicopterCurrentPopulation++;
                //}
                //else if (i == 7 && JHW_GameManager.instance.HelicopterCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[7])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                //if (i == 8 && JHW_GameManager.instance.RaptorCurrentPopulation < JHW_GameManager.instance.unitMaxCount[8])
                //{
                //    JHW_GameManager.instance.RaptorCurrentPopulation++;
                //}
                //else if (i == 8 && JHW_GameManager.instance.RaptorCurrentPopulation >= JHW_GameManager.instance.unitMaxCount[8])
                //{
                //    print("���� �α����� �����մϴ�");
                //    return;
                //}
                #endregion

            }
            else if (JHW_GameManager.instance.Gold < units.price) //���� �˻� false
            {
                print("���� �����մϴ�");
            }
            else if (JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance._UnitLoad[unitIndex] > JHW_GameManager.instance.wholePopulationLimit) //�α� �� �˻� false
            {
                print("�ִ� �α����� �����մϴ�");
            }
        }
    }

    public void CreateUnit2() //���� �����ϴ� �ڵ�
    {
        int randomNum = Random.Range(0, 3); // 3���߿� �ϳ��� �����ؼ� ����
        int randomNum2 = Random.Range(0, 9); // 9�� �߿� �ϳ��� �����ؼ� ����

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9�������� �����߿� �ϳ� ����
        SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy = true; //���̴�
        SelectUnit.tag = "Enemy";
        SelectUnit.layer = LayerMask.NameToLayer("EnemyTeam");
        Collider col = SelectUnit.GetComponentInChildren<Collider>();
        col.gameObject.tag = SelectUnit.tag;
        col.gameObject.layer = SelectUnit.layer;
        Transform mcp = EnemyCreatePoint[randomNum]; // 1~3�� ��������Ʈ �߿� �ϳ� ����
        SelectUnit.transform.position = mcp.position; // ���ֵ��� ���� ����Ʈ�� ���´�

        enemyUnits.Add(SelectUnit.GetComponent<JHW_UnitManager>());
    }

    void ValueChanger(int index)
    {
        //if (unitIndex == index)
        //{
        JHW_GameManager.instance.currentPopulationArray[index] += JHW_GameManager.instance._UnitLoad[index];
        JHW_GameManager.instance.populationSum = false;
        JHW_GameManager.instance.CoolDownReady[index] = false; //������ �� ���� ��Ÿ�ӷ��� �������� ������ش�
        //}

    }

    void CoolTimeSetter(int index)
    {
        if (!JHW_GameManager.instance.isBuff_CoolDown) JHW_GameManager.instance.currentCool[index] = JHW_GameManager.instance._cooldown[index]; //���� ��� �����ش�
        else JHW_GameManager.instance.currentCool[index] = JHW_GameManager.instance._cooldown[index] * 0.75f;
        //print("��Ÿ�� ����");
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

        // print("��Ÿ�� ��");
        JHW_GameManager.instance.CoolDownReady[index] = true;
    }

    void InstantiateUnit(int unitIndex)
    {
        int randomNum = Random.Range(0, 3); // 3���߿� �ϳ��� �����ؼ� ���� (���� ����)

        JHW_GameManager.instance.Gold -= units.price; // ��ü ��忡�� ������ ���� ŭ ����

        // JHW_GameManager.instance.currentPopulation++; //�����ϸ� �α����� 1 �ø���

        GameObject SelectUnit = Instantiate(Units[unitIndex]); // 1~9�������� �����߿� �ϳ� ����
        SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy = false; //�Ʊ��̴�
        SelectUnit.tag = "Player";
        SelectUnit.layer = LayerMask.NameToLayer("PlayerTeam");
        SelectUnit.GetComponent<NavMeshAgent>().speed = SelectUnit.GetComponent<JHW_UnitInfo>().MOVE_SPEED;
        Collider col = SelectUnit.GetComponentInChildren<Collider>(); //������ ������ �θ� �������Ʈ��(�ݶ��̴� ����)
        col.gameObject.tag = SelectUnit.tag;
        col.gameObject.layer = SelectUnit.layer;

        Transform mcp = MyCreatePoint[randomNum]; // 1~3�� ��������Ʈ �߿� �ϳ� ����
        SelectUnit.transform.position = mcp.position; // ���ֵ��� ���� ����Ʈ�� ���´�

        myUnits.Add(SelectUnit.GetComponent<JHW_UnitManager>()); //�����ϸ� ����Ʈ�� �ִ´�
    }

}


