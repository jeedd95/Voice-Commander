using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JHW_HpbarUI : MonoBehaviour
{
    public JHW_UnitInfo unitInfo;
    public Image Simple;
    public Image Circle;
    public Image HealthBar;
    float OriginHp;
    public Text Damage;
    // Start is called before the first frame update
    void Start()
    {
        unitInfo = GetComponent<JHW_UnitInfo>();
        OriginHp = unitInfo.health;
    }

    // Update is called once per frame
    void Update()
    {
        //FrameColorChanger();
        FloorColorChanger();
        HpBar();
        Damage.text = unitInfo.health.ToString();
    }

    void FrameColorChanger()
    {
        if(unitInfo.isEnemy)
        {
            Simple.color = Color.red;
        }
        else
        {
            Simple.color = Color.green;
        }
    }

    void FloorColorChanger()
    {
        if (unitInfo.isEnemy)
        {
            Circle.color = Color.red;
        }
        else
        {
            Circle.color = Color.green;
        }
    }

    void HpBar()
    {
        HealthBar.fillAmount = unitInfo.health / OriginHp;
    }
}
