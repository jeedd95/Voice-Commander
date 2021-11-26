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

    bool producing=false;


    void Start()
    {
    }

    void Update()
    {
        CreateUnit();

        if (Input.GetKeyDown(KeyCode.Alpha2)) //2��Ű�� ������ �� ���� ����
        {
            CreateUnit2();
        }
    }

    int i = -1; //�Ʒ� �Լ����� ���̴� ����
    public void CreateUnit() //�Ʊ��� �����ϴ� �ڵ�
    {

        int randomNum = Random.Range(0, 3); // 3���߿� �ϳ��� �����ؼ� ���� (���� ����)
        //int randomNum2 = Random.Range(0, 9); // 9�� �߿� �ϳ��� �����ؼ� ���� 

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
                default:
                    i = -1;
                    break;
            }

            if (i == -1)
            {
                return;
            }

            units = Units[i].GetComponent<JHW_UnitInfo>();
            if (JHW_GameManager.instance.Gold >= units.price && JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance._UnitLoad[i] <= JHW_GameManager.instance.wholePopulationLimit) //������ �ִ� ��尡 �������� ���� ���ݺ��� ����, ���� �α� + ���� ���ϼ��� �� �α����� ������
            {
                for (int j = 0; j < JHW_GameManager.instance._UnitLoad.Length; j++)
                {
                AA(j);
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

                JHW_GameManager.instance.Gold -= units.price; // ��ü ��忡�� ������ ���� ŭ ����

                // JHW_GameManager.instance.currentPopulation++; //�����ϸ� �α����� 1 �ø���

                GameObject SelectUnit = Instantiate(Units[i]); // 1~9�������� �����߿� �ϳ� ����
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
            else if (JHW_GameManager.instance.Gold < units.price)
            {
                print("���� �����մϴ�");
            }
            else if (JHW_GameManager.instance.currentPopulation + JHW_GameManager.instance._UnitLoad[i] > JHW_GameManager.instance.wholePopulationLimit)
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

    void AA(int index)
    {
            if (i == index && JHW_GameManager.instance.CoolDownReady[index] && JHW_GameManager.instance.CoolDownReady[index] == true)
            {
                JHW_GameManager.instance.currentPopulationArray[index] += JHW_GameManager.instance._UnitLoad[index];
                JHW_GameManager.instance.populationSum = false;
                JHW_GameManager.instance.CoolDownReady[index] = false; //������ �� ���� ��Ÿ�ӷ��� �������� ������ش�
            }
            else if (i == index && !JHW_GameManager.instance.CoolDownReady[index])
            {
                print("��Ÿ�� ���Դϴ� : " + JHW_GameManager.instance.currentCool[index] + "�� ���ҽ��ϴ�");
                return;
            }
           StartCoroutine(CoolTimeCo(index, JHW_GameManager.instance.currentCool[index]));
            
    }
    //public void CoolTimer()
    //{
    //    //������ �ϳ� ���� ������ ��Ÿ���� ������ �ʹ�
    //    //��Ÿ���� ���� ������ ���� ���Ѵ�
    //    //�ʿ� �Ӽ� : ���� ��Ÿ�� �����迭, ���� ��Ÿ�� ���� �迭(�̸� �÷ȴ� ���ȴ� ��), ��Ÿ���� �����ִ��� �ƴ��� �Ǻ��ϴ� �� �迭
    //}
    IEnumerator CoolTimeCo(int index, float coolTime)
    {
        yield return new WaitForSeconds(coolTime); //��Ÿ�� ��ŭ ��ٸ���
        JHW_GameManager.instance.CoolDownReady[index] = true; //��Ÿ���� ��ٸ�����  ���� ��ٰ� ��Ÿ�ӷ���  ������ ��ȭ�����ش�
    }

}


