using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_UnitFactory : MonoBehaviour
{
    public Transform[] MyCreatePoint; //�츮 ��������Ʈ 3��
    public Transform[] EnemyCreatePoint; //���� ���� ����Ʈ 3��

    public GameObject[] Units; //���� �����յ�
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
        int randomNum = Random.Range(0, 3); // 3���߿� �ϳ��� �����ؼ� ����
        int randomNum2 = Random.Range(0, 9); // 9�� �߿� �ϳ��� �����ؼ� ����

        GameObject SelectUnit = Instantiate(Units[randomNum2]); // 1~9�������� �����߿� �ϳ� ����
        Transform mcp = MyCreatePoint[randomNum]; // 1~3�� ��������Ʈ �߿� �ϳ� ����
        SelectUnit.transform.position = mcp.position; // ���ֵ��� ���� ����Ʈ�� ���´�

        if (unitInfo.isEnemy == true) //���� ������ �����̶�� �۷ι� ��ǥ�� �������� ����
        {
            SelectUnit.transform.forward = Vector3.left; 
        }
        if (unitInfo.isEnemy == false) //���� ������ �츮���̶�� �۷ι� ��ǥ�� ���������� ����
        {
            SelectUnit.transform.forward = Vector3.right; 
        }
    }
}
