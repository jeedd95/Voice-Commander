using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.Core;



namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{
    public class JHW_VoiceManager : MonoBehaviour
    {
        GCSR_Example gCSR_Example;
        public Text FinalOrderText;

        // Start is called before the first frame update
        void Start()
        {
            gCSR_Example = transform.GetComponent<GCSR_Example>();
        }

        // Update is called once per frame
        void Update()
        {
            GetkeyOrder();
            FinalOrderText.text = gCSR_Example._orderText;
        }

        void GetkeyOrder()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gCSR_Example.StartRecordButtonOnClickHandler();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                gCSR_Example.StopRecordButtonOnClickHandler();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                gCSR_Example._orderText = "";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
               VoiceOrder();
            }

            //if (Input.GetKeyDown(KeyCode.X)) //��� ����
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        gCSR_Example._orderText[i].text = "";
            //        gCSR_Example.index = 0;
            //    }
            //}
            //if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Backspace)) //�ϳ� �����
            //{
            //    int index = 0;
            //    //������� ���� �迭 �߿��� �ε����� ����ū ���� ������ ����� �ʹ�
            //    for (int i = 0; i < gCSR_Example._orderText.Length; i++)
            //    {
            //        if (gCSR_Example._orderText[i].text != "")
            //        {
            //            index = i;
            //        }
            //    }

            //    gCSR_Example._orderText[index].text = "";
            //    gCSR_Example.index = index;
            //}


        }

        //void CombineOrder()
        //{
        //for (int i = 0; i < gCSR_Example._orderText.Length; i++)
        //{
        //    FinalOrderText.text += gCSR_Example._orderText[i].text;
        //}
        //+ gCSR_Example._orderText[1].text + gCSR_Example._orderText[2].text + gCSR_Example._orderText[3].text + gCSR_Example._orderText[4].text;
        //}

        void VoiceOrder()
        {
           if(FinalOrderText.text.Contains("����") && FinalOrderText.text.Contains("����"))
            {
                print("111111111111");
            }
           else
            {
                print("22222222222222222");
            }
        }
    }
}
