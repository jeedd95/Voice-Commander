using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SSJ_TextOrderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string order = "[누구]<10,20>(으로가)";

        string who = "";
        string where = "";
        string command = "";

        // 10,20으로가
        int head = order.IndexOf("[", 0);
        int tail = order.IndexOf("]", head);
        who = order.Substring(head + 1, tail - head - 1);

        head = order.IndexOf("<", tail);
        tail = order.IndexOf(">", head);
        where = order.Substring(head + 1, tail - head - 1);

        head = order.IndexOf("(", tail);
        tail = order.IndexOf(")", head);
        command = order.Substring(head + 1, tail - head - 1);

        print(who);
        print(where);
        print(command);


        string[] pos = where.Split(',');
        float x = float.Parse(pos[0]);
        float y = float.Parse(pos[1]);
        print(x + ", "+ y);


        inputFieldOrder.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && false == string.IsNullOrEmpty(inputFieldOrder.text))
        {
            OnClickOrder();
        }
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
            print("1111111111111111111111");
        }
        order = "10,20으로가";
        if (order.Contains("으로가"))
        {
            // 10,20으로가
            int pos = order.IndexOf("으로가");
        }

        inputFieldOrder.text = "";
        inputFieldOrder.Select();
    }

}
