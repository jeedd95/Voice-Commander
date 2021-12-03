using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//레이 캐스트를 사용하여 상대팀의 유닛을 감지하고 공격하게 하고싶다
//필요 요소 : 레이 캐스트, 감지시간 0.1f , 발사 속도 느리게 , 사거리, 맞은놈의 유닛인포의 체력

public class JHW_Turret : MonoBehaviour
{
    JHW_UnitManager um;

    public float turretHp;
    public GameObject firePos;
    RaycastHit hit; //맞은놈
    float MaxDistance; //최대 사거리 = 감지거리
    [SerializeField]
    GameObject nearestUnit;

    // Start is called before the first frame update
    void Start()
    {
        turretHp = 5000f;
        um = JHW_UnitFactory.instance.GetComponent<JHW_UnitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShootRay();
        }
        TurretDamaged();

    }

    void TurretDetect()
    {
        if (this.name == "TeamTurret") um.FindNearestObjectzByLayer("EnemyTeam");
        if (this.name == "EnemyTurret") um.FindNearestObjectzByLayer("PlayerTeam");
    }

    public void TurretDamaged() //벽의 hp가 0이하로 되면 벽 삭제
    {
        if (turretHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ShootRay()
    {
        Debug.DrawRay(firePos.transform.position, transform.forward * MaxDistance, Color.red, 0.5f);
         //
    }
    
}
