using UnityEngine;

public static class CurrencyManager
{
    private const string CurrencyKey = "SoftCurrency";

    public static int GetCurrency()
    {
        return PlayerPrefs.GetInt(CurrencyKey, 0);
    }

    public static void AddCurrency(int amount)
    {
        int current = GetCurrency();
        PlayerPrefs.SetInt(CurrencyKey, current + amount);
    }

    public static void ResetCurrency()
    {
        PlayerPrefs.DeleteKey(CurrencyKey);
    }
}