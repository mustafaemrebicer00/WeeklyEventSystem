using UnityEngine;
using Assets.Scripts.EventSystem;

public static class EventProgressManager
{
    public static int GetProgress(EventData eventData)
    {
        string key = GetKey(eventData);
        return PlayerPrefs.GetInt(key, 0);
    }

    public static void AddProgress(EventData eventData, int amount)
    {
        string key = GetKey(eventData);
        int current = PlayerPrefs.GetInt(key, 0);
        PlayerPrefs.SetInt(key, current + amount);
    }

    public static void ResetProgress(EventData eventData)
    {
        string key = GetKey(eventData);
        PlayerPrefs.DeleteKey(key);
    }

    private static string GetKey(EventData eventData)
    {
        return $"Progress_{eventData.eventName}";
    }
}