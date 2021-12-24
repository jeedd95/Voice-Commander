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

            if (Input.GetKeyDown(KeyCode.X)) //초기화
            {
                gCSR_Example._orderText = "";
            }
            if(Input.GetKeyDown(KeyCode.C)) //한글자씩 지우기
            {
                gCSR_Example._orderText = gCSR_Example._orderText.Substring(0, gCSR_Example._orderText.Length - 1);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                VoiceOrder();
                gCSR_Example._orderText = "";
            }

            //if (Input.GetKeyDown(KeyCode.X)) //모두 삭제
            //{
            //    for (int i = 0; i < 5; i++)
            //    {
            //        gCSR_Example._orderText[i].text = "";
            //        gCSR_Example.index = 0;
            //    }
            //}
            //if (Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Backspace)) //하나 지우기
            //{
            //    int index = 0;
            //    //비어있지 않은 배열 중에서 인덱스가 가장큰 애의 내용을 지우고 싶다
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

            if(FinalOrderText.text.Contains("생산"))
            {
                if(FinalOrderText.text.Contains("보병"))
                {
                    JHW_UnitFactory.instance.CreateUnit(0);
                }
                if(FinalOrderText.text.Contains("정찰병"))
                {
                    JHW_UnitFactory.instance.CreateUnit(1);
                }
                if (FinalOrderText.text.Contains("저격수"))
                {
                    JHW_UnitFactory.instance.CreateUnit(2);
                }
                if (FinalOrderText.text.Contains("포병"))
                {
                    JHW_UnitFactory.instance.CreateUnit(3);
                }
                if (FinalOrderText.text.Contains("기관총"))
                {
                    JHW_UnitFactory.instance.CreateUnit(4);
                }
                if (FinalOrderText.text.Contains("장갑차"))
                {
                    JHW_UnitFactory.instance.CreateUnit(5);
                }
                if (FinalOrderText.text.Contains("탱크"))
                {
                    JHW_UnitFactory.instance.CreateUnit(6);
                }
                if (FinalOrderText.text.Contains("헬기"))
                {
                    JHW_UnitFactory.instance.CreateUnit(7);
                }
                if (FinalOrderText.text.Contains("전투기"))
                {
                    JHW_UnitFactory.instance.CreateUnit(8);
                }
            }

            if(FinalOrderText.text.Contains("점령"))
            {

            }


            else
            {
                print("알수없는 명령어 입니다");
            }
        }

    }
}
