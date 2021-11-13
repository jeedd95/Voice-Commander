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
    //public float accuracyRate; //���߷�
    public int coolDown; //��Ÿ��
    public int price; //����
    public int populationLimit; //�α� ����
    public int exp; //����ġ
}

