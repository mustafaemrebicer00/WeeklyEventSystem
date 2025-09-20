using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Assets.Scripts.EventSystem
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance { get; private set; }

        public EventData starEvent;
        public EventData blazeEvent;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); 
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public EventData GetActiveEvent()
        {
            DayOfWeek today = DateTime.Now.DayOfWeek;

           
            if (today == DayOfWeek.Monday || today == DayOfWeek.Tuesday || today == DayOfWeek.Wednesday)
                return starEvent;

            
            return blazeEvent;
        }

        public List<string> GetActiveEventWords()
        {
            var activeEvent = GetActiveEvent();
            if (activeEvent == null)
            {
                Debug.LogError("No active event found! Returning empty word list.");
                return new List<string>();
            }

            
            var shuffled = new List<string>(activeEvent.eventWords);
            System.Random rnd = new System.Random();
            shuffled = shuffled.OrderBy(x => rnd.Next()).ToList();
            var selected = shuffled.Take(5).ToList();

            Debug.Log($"Active event: {activeEvent.eventName}, Words: {string.Join(", ", selected)}");
            return selected;
        }

        private bool IsDayInRange(DayOfWeek today, DayOfWeek start, DayOfWeek end)
        {
           
            if (start <= end)
                return today >= start && today <= end;

            
            return today >= start || today <= end;
        }
    }
}