using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UnitScale
{
    small,
    middle,
    large
}
public enum AttackType
{
    normal,
    explosive,
    concussive,
    fixedDamage
}

public class JHW_UnitInfo : MonoBehaviour
{
    public bool isBehindWall; //�� �ڿ� �ִ��� ������
    public bool UseDefensive; //����¼��� ����� �Ƚ����
    public bool UseOffensive; //���� �¼��� ����� �Ƚ����
    public bool isCaptureUnit; //������������
    public bool inSmoke;

    public bool isEnemy; //�츮������ ��������
    public string unitName; //�̸�
    public UnitScale unitScale; //ũ��(����,����,����) 
    public AttackType attackType; //���� Ÿ��(�Ϲ�,����,����,����)
    public bool canGroundAttack; //����
    public bool canSkyAttack; //���
    public bool isAirForce; //���� ��������
    public float damage; //���ݷ�
    public float health; //ü��
    public float moveSpeed; //�̵��ӵ�
    public float attackSpeed; //���ݼӵ�
    public float attackRange; //��Ÿ�
    public float accuracyRate; //���߷�
    public int coolDown; //��Ÿ�� GM
    public int price; //����
    public int populationLimit; //�� ���� �� �α� ���� GM
    public int exp; //����ġ
    public int score; //����

    // property
    public float ATTACK_RANGE //isBehindWall�� true�� ��Ÿ� 30�� ���� or �������
    {
        get
        {
            if (isBehindWall)
            {
                return attackRange * 1.3f;
            }
            return attackRange;
        }
    }

    public float MOVE_SPEED //UseOffensive�� ���� ��� �̵��ӵ� 50�� ����
    {
        get
        {
            if (UseOffensive)
            {
                return moveSpeed * 1.5f;
            }
            return moveSpeed;
        }
    }
    public float ATTACK_SPEED  //UseOffensive�� ���� ��� ���ݼӵ� 30�� ����
    {
        get
        {
            if (UseOffensive)
            {
                return attackSpeed * 1.3f;
            }
            return attackSpeed;
        }
    }

    public float ACCURACYRATE
    {
        get
        {
            if (inSmoke)
            {
                return accuracyRate * 0.5f;
            }
            return accuracyRate;
        }
    }



}

