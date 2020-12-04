using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    //Variables for Sliders
    private Slider mineralSlider;
    private Slider hpSlider;

    //Variable to set the speed on which the bar fills
    private float FillSpeed = 0.2f;

    // Adds slider variables to gameobjects and sets the value to zero and 1
    void Start()
    {
        mineralSlider = GameObject.Find("ProgressBar").GetComponent<Slider>();
        hpSlider = GameObject.Find("HpBar").GetComponent<Slider>();
        mineralSlider.value = 0.0f;
        hpSlider.value = 1.0f;
    }

    // Adds 0.25 to the mineral value and fills it on the fillspeed
    public void MineralProgress()
    {
        mineralSlider.value += 0.25f;
        mineralSlider.value += FillSpeed * Time.deltaTime;
    }

    // Decreases mineral value by 0.25 and takes it from the bar with fillspeed
    public void MineralRegress()
    {
        mineralSlider.value -= 0.25f;
        mineralSlider.value -= FillSpeed * Time.deltaTime;
    }

    public void MineralRegressAlien()
    {
        mineralSlider.value -= 0.50f;
        mineralSlider.value -= FillSpeed * Time.deltaTime;
    }

    // Adds 0.25 to the Hp value and fills it on the fillspeed
    public void HpProgress()
    {
        hpSlider.value += 0.25f;
        hpSlider.value += FillSpeed * Time.deltaTime;
    }

    // Decreases Hp value by 0.25 and takes it from the bar with fillspeed
    public void HpRegress()
    {
        hpSlider.value -= 0.25f;
        hpSlider.value -= FillSpeed * Time.deltaTime;
    }

    public void HpRegressAlien()
    {
        hpSlider.value -= 0.50f;
        hpSlider.value -= FillSpeed * Time.deltaTime;
    }

}
