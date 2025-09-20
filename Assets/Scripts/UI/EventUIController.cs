using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.EventSystem;
using System;
using System.Linq;

namespace Assets.Scripts.UI
{
    public class EventUIController : MonoBehaviour
    {
        public Text eventTitleText;
        public Image eventIcon;
        public Slider eventProgressBar;
        public EventData starEvent;
        public EventData blazeEvent;
        public HintPanelController hintPanelController;
        public Text progressValueText; 

        void Start()
        {
            var eventManager = EventManager.Instance;
            if (eventManager == null)
            {
                Debug.LogError("EventManager instance is missing!");
                return;
            }

            EventData activeEvent = eventManager.GetActiveEvent();
            if (activeEvent == null)
            {
                Debug.LogError("No active event found!");
                return;
            }

            int savedStars = EventProgressManager.GetProgress(activeEvent);
            SetupEventUI(activeEvent, savedStars);

            if (hintPanelController != null)
            {
                var hints = eventManager.GetActiveEventWords().Take(5).ToList();
                Debug.Log($"Setting hints for active event: {string.Join(", ", hints)}");
                hintPanelController.SetHints(hints);
            }
        }

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
}