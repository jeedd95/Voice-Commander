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
            unitCreat.CreateUnit();
        }
    }

}
