using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;

public class Leadboard : MonoBehaviour
{
    [TitleGroup("Timers")]
    [SerializeField,ListDrawerSettings(ShowIndexLabels = true,ShowItemCount = true)] private List<TextMeshProUGUI> levelsTimerText;
    [TitleGroup("Recoltable")]
    [SerializeField,ListDrawerSettings(ShowIndexLabels = true,ShowItemCount = true)] private List<TextMeshProUGUI> levelsRecoltableText;
    [TitleGroup("Every Recoltable Font Settings")]
    [SerializeField] private Color couleurDeLaVictoire = Color.yellow;
    [SerializeField,ListDrawerSettings(Expanded = true)] private FontStyles[] fontStylesDeLaVictoire;
    [SerializeField] private float fontSize = 35f;
    

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
                var recoltee = PlayerPrefs.GetInt(keyTwo);
                var aRecoltee = PlayerPrefs.GetInt(keyThree);
                levelsRecoltableText[i - 1].text = recoltee + " / " + aRecoltee;
                if (recoltee >= aRecoltee) //On a donc récupéré tout les récoltables
                {
                    var txt = levelsRecoltableText[i - 1];
                    txt.color = couleurDeLaVictoire;
                    txt.fontSize = fontSize;
                    //Font Style :
                    FontStyles fs = FontStyles.Normal;
                    foreach (var fontStyle in fontStylesDeLaVictoire)
                    {
                        fs |= fontStyle;
                    }
                    txt.fontStyle = fs;
                }
                
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