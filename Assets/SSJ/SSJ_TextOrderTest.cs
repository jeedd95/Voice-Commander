using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SSJ_TextOrderTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string order = "[����]<10,20>(���ΰ�)";

        string who = "";
        string where = "";
        string command = "";

        // 10,20���ΰ�
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
    // ��ư�� ������ �Ҹ��� �Լ��̴�.
    public void OnClickOrder()
    {
        // InputField�� ������ �����ͼ� order��� ������ ���ʹ�.
        string order = inputFieldOrder.text;
        // ���� order�� ���뿡 "��!"�� ���ԵǾ� �ִٸ�
        if (order.Contains("��!"))
        {
            // Player���ӿ�����Ʈ�� ������Ʈ�� PlayerMove�� �����ͼ� �׳༮�� Go�Լ��� ȣ���ϰ�ʹ�.
            playerMove.Go();

        }
        if (order.Contains("����"))
        {
            print("1111111111111111111111");
        }
        order = "10,20���ΰ�";
        if (order.Contains("���ΰ�"))
        {
            // 10,20���ΰ�
            int pos = order.IndexOf("���ΰ�");
        }

        inputFieldOrder.text = "";
        inputFieldOrder.Select();
    }

}
