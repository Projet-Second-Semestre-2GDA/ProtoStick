using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LeadboardSetter
{
    public static string baseLevelKey = "BestScoreLevel";
    public static void LevelFinish(int levelNumber, float duration)
    {
        var key = baseLevelKey + levelNumber;
        Debug.LogWarning("La clé est : " + key);
        Debug.LogWarning("Duration : " + duration);
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetFloat(key) < duration)
            {
                PlayerPrefs.SetFloat(key,duration);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(key,duration);
        }
    }
}
