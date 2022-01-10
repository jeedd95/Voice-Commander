using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HPgradient : MonoBehaviour
{
    public Gradient gradient;
    Image image;
    Slider slider;
    [Range(0,1)]
    public float t;

    // Start is called before the first frame update
    void Start()
    {
        image = transform.GetComponent<Image>();
        slider = GameObject.Find("Commandhp").GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        t = slider.value;
        image.color = gradient.Evaluate(t);
    }
}
