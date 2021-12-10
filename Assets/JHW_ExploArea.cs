using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JHW_ExploArea : MonoBehaviour
{
    float defensiveDamage;
    JHW_BulletMove bm;
    JHW_BulletMove bulletinfo;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Player"))
        {
            JHW_UnitInfo hitObj = other.gameObject.transform.parent.GetComponent<JHW_UnitInfo>();

            if (hitObj.isEnemy == false && hitObj.UseDefensive == true)
            { //공격한애가 적이고, 맞는사람이 내 유닛이고 방어태세를 켜고있을경우 데미지감소
                print("데미지 경감");
                defensiveDamage = 0.8f;
            }
            else
            {
                defensiveDamage = 1f;
            }

            if (hitObj.unitScale == UnitScale.small) hitObj.health -= damage * 0.5f;
            if (hitObj.unitScale == UnitScale.middle) hitObj.health -= damage * 0.75f;
            if (hitObj.unitScale == UnitScale.large) hitObj.health -= damage * 1;
        }
    }

    public void SetCreator(JHW_BulletMove unit)  //폭파 범위에게 총알 을 알려주는 코드
    {
        this.bm = unit;
        bulletinfo = unit.GetComponent<JHW_BulletMove>(); //쏜 애의 유닛 정보
        this.damage = bulletinfo.damage;
        //this.accuracyRate = unitInfo.ACCURACYRATE;
        // attackerName = unit.gameObject.name;
    }
}
