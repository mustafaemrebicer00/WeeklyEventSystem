using UnityEngine;
using UnityEngine.UI;
using System;
using Assets.Scripts.EventSystem;

public class EventUIController : MonoBehaviour
{
    public Slider eventProgressBar;
    public Text eventTitleText;
    public Image eventIcon;
    public EventData starEvent;
    public EventData blazeEvent;
    public EventManager eventManager;
    public Text progressValueText; 

    public void SetupEventUI(EventData data, int currentStars)
    {
        eventTitleText.text = data.eventName;
        eventIcon.sprite = data.eventIcon;
        eventProgressBar.maxValue = data.goal;
        eventProgressBar.value = currentStars;

        
        if (progressValueText != null)
        {
            float percentage = (data.goal > 0) ? ((float)currentStars / data.goal * 100f) : 0f;
            progressValueText.text = $"{currentStars} / {data.goal} ({Mathf.RoundToInt(percentage)}%)";
        }
    }

}