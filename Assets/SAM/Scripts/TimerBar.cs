using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider slider;

    public Image timerColor;

    public void SetTime(float time)
    {
        slider.value = time;
    }

    public void SetMaxTime(int maxTime)
    {
        slider.maxValue = maxTime;
        slider.value = maxTime;
    }

}
