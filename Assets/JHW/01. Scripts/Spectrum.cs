using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectrum : MonoBehaviour
{
    public AudioSource Audio;   //스펙트럼 데이터로 만들 오디오 소스
    GameObject[] SpectrumCube = new GameObject[64]; //스펙트럼을 시각적으로 보여줄 큐브 배열 64개 지정
    float[] SpectrumData = new float[64]; //스펙트럼 데이터를 받을 배열 float 64개 지정
    public GameObject Temp;

    // Start is called before the first frame update
    void Start()
    {
        // 반복문을 통해서 x축으로 1간격씩 나열된 큐브 64개 생성
        for (int i = 0; i < 64; i++)
        {
            SpectrumCube[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);

            var renderer = SpectrumCube[i].transform.GetComponent<MeshRenderer>();
            renderer.material.SetColor("_Color", Color.green);
            renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            renderer.receiveShadows = false;
            SpectrumCube[i].transform.GetComponent<BoxCollider>().enabled = false;

            SpectrumCube[i].transform.SetParent(Temp.transform);
            SpectrumCube[i].transform.position = new Vector3(Temp.transform.position.x+ i, Temp.transform.position.y, Temp.transform.position.z);
            SpectrumCube[i].transform.eulerAngles = Temp.transform.eulerAngles;
        }
            Temp.transform.localScale = new Vector3(0.02303942f, 0.008018959f, 0.008018959f);
    }

    // Update is called once per frame
   // [System.Obsolete]
    void Update()
    {
        //Temp.transform.LookAt(Camera.main.transform);
        //지정된 오디오 소스에서 실시간으로 스펙트럼 데이터 받아오기
        //SpectrumData = Audio.GetSpectrumData(64, 0, FFTWindow.Rectangular);
        if (Input.GetKey(KeyCode.Space))
        {
            for (int i = 0; i < SpectrumData.Length; i++)
            {
                SpectrumData[i] = Random.Range(0f, 100f);
            }
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            for (int i = 0; i < SpectrumData.Length; i++)
            {
                SpectrumData[i] = 0;
            }
        }

        //받아온 스펙트럼 데이터를 큐브의 Scale y축 값에 대입하기
        for (int i = 0; i < 64; i++)
        {
            SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y, SpectrumData[i], 0.1f), 1);

            if (i >= 0 && i < 5)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.1f, SpectrumData[i], 0.1f), 1);
            }
            if (i >= 5 && i < 10)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.3f, SpectrumData[i], 0.1f), 1);
            }
            if (i >= 10 && i < 20)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.5f, SpectrumData[i], 0.1f), 1);
            }
            if (i >= 20 && i < 30)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.75f, SpectrumData[i], 0.1f), 1);
            }
            if (i >= 30 && i < 40)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.9f, SpectrumData[i], 0.1f), 1);
            }
            if (i >= 40 && i < 50)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.6f, SpectrumData[i], 0.1f), 1);
            }
            if (i >= 50 && i < 64)
            {
                SpectrumCube[i].transform.localScale = new Vector3(1, Mathf.Lerp(SpectrumCube[i].transform.localScale.y * 0.2f, SpectrumData[i], 0.1f), 1);
            }
        }
       
    }
}

