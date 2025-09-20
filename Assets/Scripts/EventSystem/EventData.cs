using UnityEngine;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.EventSystem
{
    [CreateAssetMenu(fileName = "NewEvent", menuName = "WeeklyEvent/EventData")]
    public class EventData : ScriptableObject
    {
        public string eventName;
        public Sprite eventIcon;
        public int goal;
        public Sprite rewardIcon;
        public List<string> eventWords;

        public DayOfWeek startDayOfWeek;
        public DayOfWeek endDayOfWeek;
         public int rewardAmount;


    }
}