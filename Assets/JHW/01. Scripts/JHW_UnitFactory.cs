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
    }

    public void CreateUnit()
    {
        int randomNum = Random.Range(0, 2); // 3���߿� �ϳ��� �����ؼ� ����
        int randomNum2 = Random.Range(0, 9); // 9�� �߿� �ϳ��� �����ؼ� ����

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9�������� �����߿� �ϳ� ����
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
