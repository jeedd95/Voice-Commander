using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitFactory : MonoBehaviour
{
    public Transform[] MyCreatePoint; //�츮 ��������Ʈ 3��
    public Transform[] EnemyCreatePoint; //���� ���� ����Ʈ 3��

    public GameObject[] Units; //���� �����յ�

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

    public void CreateUnit() //�Ʊ��� �����ϴ� �ڵ�
    {
        int randomNum = Random.Range(0, 3); // 3���߿� �ϳ��� �����ؼ� ����
        int randomNum2 = Random.Range(0, 9); // 9�� �߿� �ϳ��� �����ؼ� ����

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9�������� �����߿� �ϳ� ����
        SelectUnit.GetComponent<JHW_UnitInfo>().isEnemy=false; //�Ʊ��̴�
        SelectUnit.tag = "Player";
        SelectUnit.layer = LayerMask.NameToLayer("PlayerTeam");
        Collider col = SelectUnit.GetComponentInChildren<Collider>();
        col.gameObject.tag = SelectUnit.tag;
        col.gameObject.layer = SelectUnit.layer;
        Transform mcp = MyCreatePoint[randomNum]; // 1~3�� ��������Ʈ �߿� �ϳ� ����
        SelectUnit.transform.position = mcp.position; // ���ֵ��� ���� ����Ʈ�� ���´�
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
    }
}
