using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//레이 캐스트를 사용하여 상대팀의 유닛을 감지하고 공격하게 하고싶다
//필요 요소 : 레이 캐스트, 감지시간 0.1f , 발사 속도 느리게 , 사거리, 맞은놈의 유닛인포의 체력

public class JHW_Turret : MonoBehaviour
{
    //JHW_UnitManager um;
    public GameObject firePos; //쏘는 곳
    RaycastHit hit; //맞은놈

    public float turretHp;
    float MaxDistance = 40; //최대 사거리 = 감지거리
    bool isfire;

    Vector3 dir;
    [SerializeField]
    GameObject nearestUnit;
    [SerializeField]
    Collider[] cols;

    enum State
    {
        Detect,
        Attack
    }
    State state;


    // Start is called before the first frame update
    void Start()
    {
        turretHp = 5000f;
        //um = JHW_UnitFactory.instance.GetComponent<JHW_UnitManager>();
        state = State.Detect;
    }

    // Update is called once per frame
    void Update()
    {
        //if (this.name == "TeamTurret") print(state);
        if(hit.transform !=null) print(hit.transform);

        //if (this.name == "EnemyTurret") print("적군 터렛 상태 : " + state);

        switch (state)
        {
            case State.Detect:
                TurretDetect();
                break;
            case State.Attack:
                ShootRay();
                if(isfire ==false)
                {
                    isfire = true;
                    StartCoroutine("FireRay");
                }
                break;
        }

        TurretDamaged();
    }

    void TurretDetect()
    {
        isfire = false;
        if (this.name == "TeamTurret") nearestUnit =  FindNearestObjectzByLayer("EnemyTeam");
        if (this.name == "EnemyTurret") nearestUnit =  FindNearestObjectzByLayer("PlayerTeam");

        if (nearestUnit != null) state = State.Attack;
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

        if (nearestUnit == null || Vector3.Distance(firePos.transform.position, nearestUnit.transform.position) > MaxDistance)
        {
            state = State.Detect;
            StopCoroutine("FireRay");
        }
    }

    IEnumerator FireRay()
    {
        dir = nearestUnit.transform.position - firePos.transform.position;

        yield return new WaitForSeconds(2);

        if (nearestUnit == null) //가까이에 있는 유닛이 없다면 기다리는걸 끝냄
            yield break;

        if (Physics.Raycast(firePos.transform.position, dir, out hit, MaxDistance))
        {
            Debug.DrawRay(firePos.transform.position , dir , Color.red,0.1f);
            if(hit.transform.CompareTag("Enemy") || hit.transform.CompareTag("Player"))
            {
                hit.transform.gameObject.GetComponent<JHW_UnitInfo>().health -= 50; //무조건 50 고정데미지
            }
        }

        isfire = false;
    }


    GameObject FindNearestObjectzByLayer(string layer) //가장 가까운 오브젝트 레이어로 찾기
    {
        int layerMask = 1 << LayerMask.NameToLayer(layer);
        cols = Physics.OverlapSphere(transform.position, MaxDistance, layerMask); //공격 사거리 안에있는 적 인식

        float dist = float.MaxValue;
        int chooseIndex = -1;

        for (int i = 0; i < cols.Length; i++)
        {
            float temp = Vector3.Distance(transform.position, cols[i].transform.position);
            if (temp < dist)
            {
                dist = temp;
                chooseIndex = i;
            }
        }

        if (chooseIndex == -1) //사거리 안에 있는 적이 없음
        {
            return null;
        }
        return cols[chooseIndex].gameObject;
    }
}
