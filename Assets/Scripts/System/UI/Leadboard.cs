﻿using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Leadboard : MonoBehaviour
{
    [SerializeField] private List<TextMeshProUGUI> levelsTimerText;

    private void Start()
    {
        for (int i = 1; i <= levelsTimerText.Count; i++)
        {
            var key = LeadboardSetter.baseLevelKey + i;
            if (PlayerPrefs.HasKey(key))
            {
                levelsTimerText[i - 1].text = SetTime(PlayerPrefs.GetFloat(key));
            }
            else
            {
                levelsTimerText[i - 1].text = "None";
            }
        }
    }

    private string SetTime(float time)
    {

        int minute = Mathf.FloorToInt(time / 60);
        var txt = minute + " min " + (LeadboardSetter.RoundValue((time - minute * 60),10000));
        return txt;
    }

    [Button(ButtonSizes.Gigantic)]
    private void ResetTimers()
    {
        for (int i = 1; i <= levelsTimerText.Count; i++)
        {
            var key = LeadboardSetter.baseLevelKey + i;
            PlayerPrefs.DeleteKey(key);
        }
    }
}