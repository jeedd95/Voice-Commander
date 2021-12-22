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
                CombineOrder();
            }

        }

        void CombineOrder()
        {
            for (int i = 0; i < gCSR_Example._orderText.Length; i++)
            {
                FinalOrderText.text += gCSR_Example._orderText[i].text;
            }
        }
    }
}
