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


    public void CreateUnit() //�Ʊ��� �����ϴ� �ڵ�
    {
        int randomNum = Random.Range(0, 3); // 3���߿� �ϳ��� �����ؼ� ���� (���� ����)
        //int randomNum2 = Random.Range(0, 9); // 9�� �߿� �ϳ��� �����ؼ� ���� 
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

            if (JHW_GameManager.instance.Gold >= units.price && JHW_GameManager.instance.CanProduce_Whole ) //������ �ִ� ��尡 �������� ���� ���ݺ��� ����, ���갡���϶�
            {
                    if (i == 0)
                    {
                        JHW_GameManager.instance.RifleManCurrentPopulation++;
                    }


                JHW_GameManager.instance.Gold -= units.price; //��ü ��忡�� ������ ���� ŭ ����
                
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
            else if(JHW_GameManager.instance.Gold < units.price)
            {
                print("���� �����մϴ�");
            }
            else if(!JHW_GameManager.instance.CanProduce_Whole)
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

}
