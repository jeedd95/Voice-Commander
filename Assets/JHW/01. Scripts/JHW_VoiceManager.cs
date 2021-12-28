using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.Core;
using System.Text.RegularExpressions;
using System;


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

            if (Input.GetKeyDown(KeyCode.X)) //�ʱ�ȭ
            {
                gCSR_Example._orderText = "";
            }
            if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Backspace)) //�ѱ��ھ� �����
            {
                gCSR_Example._orderText = gCSR_Example._orderText.Substring(0, gCSR_Example._orderText.Length - 1);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                VoiceOrder();
                gCSR_Example._orderText = "";
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

            if(FinalOrderText.text.Contains("����") && !FinalOrderText.text.Contains("����"))
            {
                if(FinalOrderText.text.Contains("����"))
                {
                    JHW_UnitFactory.instance.CreateUnit(0);
                }
                if(FinalOrderText.text.Contains("������"))
                {
                    JHW_UnitFactory.instance.CreateUnit(1);
                }
                if (FinalOrderText.text.Contains("���ݼ�"))
                {
                    JHW_UnitFactory.instance.CreateUnit(2);
                }
                if (FinalOrderText.text.Contains("����"))
                {
                    JHW_UnitFactory.instance.CreateUnit(3);
                }
                if (FinalOrderText.text.Contains("�����"))
                {
                    JHW_UnitFactory.instance.CreateUnit(4);
                }
                if (FinalOrderText.text.Contains("�尩��"))
                {
                    JHW_UnitFactory.instance.CreateUnit(5);
                }
                if (FinalOrderText.text.Contains("��ũ"))
                {
                    JHW_UnitFactory.instance.CreateUnit(6);
                }
                if (FinalOrderText.text.Contains("���"))
                {
                    JHW_UnitFactory.instance.CreateUnit(7);
                }
                if (FinalOrderText.text.Contains("������"))
                {
                    JHW_UnitFactory.instance.CreateUnit(8);
                }
            }

           

            if (FinalOrderText.text.Contains("����") && !FinalOrderText.text.Contains("����"))
            {
                JHW_GameManager.instance.isCaptureCreateMode = true;

                //���� ���� ���̳ο����ؽ�Ʈ �߿��� ������ �̸��� �ִ��� Ȯ���Ѵ�
                //�ִٸ� �� ������ �ؽ��� �����Ŵ��� �Լ��� �Ķ���ͷ� �ִ´�

                Regex regex = new Regex("A");
                Match m = regex.Match(FinalOrderText.text);
                if (m.Success)
                {
                    print(m.Index); // ���� ���ڰ� ���۵Ǵ� �ε���
                    print(m.Value); //�� ����
                    //Console.WriteLine("{0}:{1}", m.Index, m.Value);
                }

            }


            else
            {
                print("�� �� ���� ��ɾ� �Դϴ�");
            }
        }

    }
}
