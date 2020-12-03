using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private Slider slider;

    private float FillSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Progress()
    {
        slider.value += 0.25f;
        slider.value += FillSpeed * Time.deltaTime;
    }

    public void Regress()
    {
        slider.value -= 0.25f;
        slider.value -= FillSpeed * Time.deltaTime;
    }

}