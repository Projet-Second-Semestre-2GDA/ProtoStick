using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Leadboard : MonoBehaviour
{
    [TitleGroup("Timers")]
    [SerializeField] private List<TextMeshProUGUI> levelsTimerText;
    [TitleGroup("Recoltable")]
    [SerializeField] private List<TextMeshProUGUI> levelsRecoltableText;

    private void Start()
    {
        for (int i = 1; i <= levelsTimerText.Count; i++)
        {
            var key = LeadboardSetter.baseLevelKey + i;
            var keyTwo = UniversalRecoltObject.RecoltableBaseKey + i;
            var keyThree = UniversalRecoltObject.TotalRecoltableBaseKey + i;
            if (PlayerPrefs.HasKey(key))
            {
                levelsTimerText[i - 1].text = SetTime(PlayerPrefs.GetFloat(key));
            }
            else
            {
                levelsTimerText[i - 1].text = "None";
            }

            if (PlayerPrefs.HasKey(keyTwo))
            {
                levelsRecoltableText[i - 1].text = PlayerPrefs.GetInt(keyTwo) + " / " + PlayerPrefs.GetInt(keyThree);
            }
            else
            {
                levelsRecoltableText[i - 1].text = "None";
            }
        }
    }

    private string SetTime(float time)
    {

        int minute = Mathf.FloorToInt(time / 60);
        var txt = minute + " min " + (LeadboardSetter.RoundValue((time - minute * 60),10000));
        return txt;
    }

    [TitleGroup("Timers")]
    [Button(ButtonSizes.Gigantic)]
    public void ResetTimers()
    {
        for (int i = 1; i <= levelsTimerText.Count; i++)
        {
            var key = LeadboardSetter.baseLevelKey + i;
            PlayerPrefs.DeleteKey(key);
            levelsTimerText[i - 1].text = "None";
        }
    }
    
    
    [TitleGroup("Recoltable")]
    [Button(ButtonSizes.Gigantic)]
    public void ResetRecoltable()
    {
        for (int i = 1; i <= levelsTimerText.Count; i++)
        {
            var keyTwo = UniversalRecoltObject.RecoltableBaseKey + i;
            var keyThree = UniversalRecoltObject.TotalRecoltableBaseKey + i;
            PlayerPrefs.DeleteKey(keyTwo);
            PlayerPrefs.DeleteKey(keyThree);
            levelsTimerText[i - 1].text = "None";
        }
    }
}