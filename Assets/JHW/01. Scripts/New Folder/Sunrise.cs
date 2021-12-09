using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunrise : MonoBehaviour
{
    public Transform sunrise; //������ ������ġ
    public Transform sunset; //������ ������ġ
    public float journeyTime = 1.0F; //������ġ���� ������ġ���� �����ϴ� �ð�, ���� �������� ������ ����.
    private float startTime;
    public float reduceHeight = 0.2f; //Center���� ���̱�, �ش� ���� �������� �������� ���̴� ��������.

    void Start()
    {
        sunrise = GameObject.Find("Cube").transform;
        sunset = GameObject.Find("Cube (1)").transform;

        startTime = Time.time;
    }
    void Update()
    {
        Vector3 center = (sunrise.position + sunset.position) * 0.5F; //Center ����ŭ ���� �ö󰣴�.
        center -= new Vector3(0, 1f * reduceHeight, 0); //y���� ���̸� ���̰� ��������.
        Vector3 riseRelCenter = sunrise.position - center;
        Vector3 setRelCenter = sunset.position - center;
        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;
    }
}