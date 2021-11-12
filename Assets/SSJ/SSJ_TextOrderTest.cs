using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SSJ_TextOrderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public JHW_UnitFactory unitCreat;
    public SSJ_PlayerMove playerMove;
    public InputField inputFieldOrder;
    // 버튼이 눌리면 불리는 함수이다.
    public void OnClickOrder()
    {
        // InputField의 내용을 가져와서 order라는 변수에 담고싶다.
        string order = inputFieldOrder.text;
        // 만약 order의 내용에 "고!"가 포함되어 있다면
        if (order.Contains("고!"))
        {
            // Player게임오브젝트의 컴포넌트인 PlayerMove를 가져와서 그녀석의 Go함수를 호출하고싶다.
            playerMove.Go();

        }
        if (order.Contains("생성"))
        {
            unitCreat.CreateUnit();
        }
    }

}
