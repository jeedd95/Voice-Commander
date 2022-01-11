using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FrostweepGames.Plugins.Core;
using System.Text.RegularExpressions;
//using System;


namespace FrostweepGames.Plugins.GoogleCloud.SpeechRecognition.Examples
{
    public class JHW_VoiceManager : MonoBehaviour
    {
        GCSR_Example gCSR_Example;
        public Text FinalOrderText;
        public Text LastOrderText;

        public Image info;
        
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
            if(Input.GetKeyDown(KeyCode.C) || Input.GetKeyDown(KeyCode.Backspace)) //한글자씩 지우기
            {
                gCSR_Example._orderText = gCSR_Example._orderText.Substring(0, gCSR_Example._orderText.Length - 1);
            }

            if (Input.GetKeyDown(KeyCode.F) && gCSR_Example._orderText != string.Empty)
            {
                VoiceOrder();
                gCSR_Example._orderText = "";
                LastOrderText.text = FinalOrderText.text;
            }

            if(Input.GetKeyDown(KeyCode.R) && LastOrderText.text != string.Empty)
            {
                FinalOrderText.text = LastOrderText.text;
                VoiceOrder();
                gCSR_Example._orderText = "";
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                info.rectTransform.anchoredPosition = new Vector2(622f, info.rectTransform.anchoredPosition.y);
                Color color = info.color;
                color.a = 1f;
                info.color = color;

            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                info.rectTransform.anchoredPosition = new Vector2(1315.839f, info.rectTransform.anchoredPosition.y);
                Color color = info.color;
                color.a = 0.7f;
                info.color = color;
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

            if (FinalOrderText.text.Contains("생산"))
            {
                ProduceUnit();
            }

            if (FinalOrderText.text.Contains("점령"))
            {
                JHW_GameManager.instance.isCaptureCreateMode = true;
                DestinationSet();
                ProduceUnit();
                //Match m = regex.Match(FinalOrderText.text);
                //if (m.Success)
                //{
                //    //print(m.Index); // 위에 문자가 시작되는 인덱스
                //    print(FinalOrderText.text.Substring(m.Index, m.Index + 1));
                //    //print(m.Value); //그 문자
                //    //Console.WriteLine("{0}:{1}", m.Index, m.Value);
                //}
            }

            if (FinalOrderText.text.Contains("폭격"))
            {
               // JHW_GameManager.instance.isPlayerSkillMode = true;
                DestinationSet();
                JHW_GameManager.instance.PlayerSkill_Bomb();
                //JHW_GameManager.instance.skill_Bomb_Cool();
            }

            if (FinalOrderText.text.Contains("연막"))
            {
                //JHW_GameManager.instance.isPlayerSkillMode = true;
                DestinationSet();
                JHW_GameManager.instance.PlayerSkill_Smoke();
            }

            if (FinalOrderText.text.Contains("방어태세"))
            {
                if(FinalOrderText.text.Contains("실행"))
                {
                    JHW_GameManager.instance.OnClickDefense();
                }
                if(FinalOrderText.text.Contains("중단"))
                {
                    JHW_GameManager.instance.NotClickDefense();
                }
            }
            if(FinalOrderText.text.Contains("공격태세"))
            {
                if (FinalOrderText.text.Contains("실행"))
                {
                    JHW_GameManager.instance.OnClickOffense();
                }
                if (FinalOrderText.text.Contains("중단"))
                {
                    JHW_GameManager.instance.NotClickOffense();
                }
            }

            if(FinalOrderText.text.Contains("인구증가"))
            {
                JHW_GameManager.instance.OnClickWholePopulationUp();
            }
            if(FinalOrderText.text.Contains("골드획득"))
            {
                JHW_GameManager.instance.OnClickGetGold();
            }
            if(FinalOrderText.text.Contains("골드업그레이드"))
            {
                JHW_GameManager.instance.OnClickGetGoldRateUP();
            }

            //else
            //{
            //    print("알 수 없는 명령어 입니다");
            //}

        }

        void DestinationSet()
        {
            string head;
            string tail;

            Regex regex = new Regex("[0-9]");
            Match m = regex.Match(FinalOrderText.text);
            //if (m.Success)
            //{
            //    print(m.Value);
            //}

            if (FinalOrderText.text.Contains("알파"))
            {
                head = "A";
                tail = m.Value.ToString();
                string order = head + tail; //A1
                JHW_OrderManager.instance.StringOrder(order);
            }

            if (FinalOrderText.text.Contains("브라보"))
            {
                head = "B";
                tail = m.Value.ToString();
                string order = head + tail; //B1
                JHW_OrderManager.instance.StringOrder(order);
            }

            if (FinalOrderText.text.Contains("찰리"))
            {
                head = "C";
                tail = m.Value.ToString();
                string order = head + tail; //C1
                JHW_OrderManager.instance.StringOrder(order);
            }
            if (FinalOrderText.text.Contains("델타"))
            {
                head = "D";
                tail = m.Value.ToString();
                string order = head + tail; //D1
                JHW_OrderManager.instance.StringOrder(order);
            }
        }

        void ProduceUnit()
        {
            if (FinalOrderText.text.Contains("보병"))
            {
                JHW_UnitFactory.instance.CreateUnit(0);
            }
            if (FinalOrderText.text.Contains("정찰병"))
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
            if (FinalOrderText.text.Contains("헬리콥터"))
            {
                JHW_UnitFactory.instance.CreateUnit(7);
            }
            if (FinalOrderText.text.Contains("제트기"))
            {
                JHW_UnitFactory.instance.CreateUnit(8);
            }
        }


    }

}
