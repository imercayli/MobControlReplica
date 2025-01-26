using System;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData
{
    public static int GameLevel
    {
        get => PlayerPrefs.GetInt(SaveDataKeys.GameLevel, 0);
        set => PlayerPrefs.SetInt(SaveDataKeys.GameLevel, value);
    }
    
    public static int TutorialCount
    {
        get => PlayerPrefs.GetInt(SaveDataKeys.TutorialCount, -1);
        set => PlayerPrefs.SetInt(SaveDataKeys.TutorialCount, value);
    }

    public static int DayCount
    {
        get => PlayerPrefs.GetInt(SaveDataKeys.DayCount, 1);
        set => PlayerPrefs.SetInt(SaveDataKeys.DayCount, value);
    }

    public static Action<bool> OnHapticValueChanged;
    public static bool IsHapticOn
    {
        get => PlayerPrefs.GetInt(SaveDataKeys.HapticOn, 1) == 1;
        set
        {
            PlayerPrefs.SetInt(SaveDataKeys.HapticOn, value ? 1 : 0);
            OnHapticValueChanged?.Invoke(value);
        }
    }
}

public struct SaveDataKeys
{
    public const string GameLevel = "GameLevel";
    public const string DayCount = "DayCount";
    public const string HapticOn = "HapticOn";
    public const string TutorialCount = "TutorialCount";
}