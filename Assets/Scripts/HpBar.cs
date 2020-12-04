using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    //Variable for Slider
    private Slider slider;

    //Variable to set the speed on which the bar fills
    private float FillSpeed = 0.2f;

    // Adds slider variable to gameobject and sets the value to max
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
        slider.value = 1f;
    }

    // Adds 0.25 to the value and fills it on the fillspeed
    public void Progress()
    {
        slider.value += 0.25f;
        slider.value += FillSpeed * Time.deltaTime;
    }

    // Decreases value by 0.25 and takes it from the bar with fillspeed
    public void Regress()
    {
        slider.value -= 0.25f;
        slider.value -= FillSpeed * Time.deltaTime;
    }

}