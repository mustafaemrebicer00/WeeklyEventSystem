using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EventSystem;
using System;

public class DebugPanel : MonoBehaviour
{
    public Slider progressSlider;
    public Text progressValueText;
    public EventUIController eventUIController;

    private EventData activeEvent;

    void Start()
    {
        var eventManager = EventManager.Instance;
        if (eventManager == null)
        {
            Debug.LogError("EventManager instance is missing!");
            return;
        }

        activeEvent = eventManager.GetActiveEvent();
        if (activeEvent == null)
        {
            Debug.LogError("No active event found!");
            return;
        }

       
        int savedStars = EventProgressManager.GetProgress(activeEvent);
        eventUIController.SetupEventUI(activeEvent, savedStars);

        
        progressSlider.minValue = 0;
        progressSlider.maxValue = activeEvent.goal;
        progressSlider.value = savedStars;

        
        UpdateProgressText(savedStars, activeEvent.goal);
    }

    public void OnProgressChanged(float value)
    {
        if (activeEvent == null)
        {
            Debug.LogError("Active event is null!");
            return;
        }

        int stars = Mathf.RoundToInt(value);
        PlayerPrefs.SetInt($"Progress_{activeEvent.eventName}", stars);
        eventUIController.SetupEventUI(activeEvent, stars);

        
        UpdateProgressText(stars, activeEvent.goal);
    }

    private void UpdateProgressText(int currentStars, int goal)
    {
        if (goal <= 0)
        {
            Debug.LogError("Goal value is invalid!");
            progressValueText.text = "0%";
            return;
        }

        float percentage = (float)currentStars / goal * 100f;
        progressValueText.text = $"{currentStars} / {goal} ({Mathf.RoundToInt(percentage)}%)";
    }
}