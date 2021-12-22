using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JHW_OrderManager : MonoBehaviour
{
    public static JHW_OrderManager instance; //�̱���
    
    //��Ʈ�� ���� ����
    [HideInInspector]
    public string order; //��ǲ�ʵ忡 ���� ����
    public InputField inputFieldOrder;

    public Text DestinationText;

    public GameObject DesinationAreaObj; //���������� ������Ʈ
    public List<string> Coordinates; //��ǥ �ؽ�Ʈ �迭
    string[] word = {"A","B","C","D"};
    int[] num = {1,2,3,4,5,6,7,8};

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CoordinatesSet();
    }

    // Update is called once per frame
    void Update()
    {
        //print(DesinationAreaObj);

        if (Input.GetKeyDown(KeyCode.Return) && string.IsNullOrEmpty(inputFieldOrder.text) == false) //����Ű
        {
            StringOrder();
        }

       if(DesinationAreaObj !=null) DestinationText.text = "���� ���� : " + DesinationAreaObj.name;
    }

    public void StringOrder() //�ؽ�Ʈ�� �������� ����
    {
        order = inputFieldOrder.text;

        for (int i = 0; i < num.Length * word.Length; i++)
        {
            if (order.Contains(Coordinates[i]))
            {
                DesinationAreaObj = GameObject.Find(Coordinates[i]).gameObject;
            }

        }
        
        inputFieldOrder.text = "";
    }

    void CoordinatesSet()
    {
        DesinationAreaObj = null;

        Coordinates = new List<string>();

        for (int i = 0; i < num.Length; i++)
        {
            for (int j = 0; j < word.Length; j++)
            {
                Coordinates.Add(word[j] + num[i].ToString());
            }
        }
        Coordinates.Sort();
    }
}
