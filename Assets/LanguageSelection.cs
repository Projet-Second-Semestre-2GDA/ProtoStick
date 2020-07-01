using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSelection : MonoBehaviour
{
    private List<TextLocaliserUI> textes;
    private void SetLanguage(Language language)
    {
        LocalisationSystem.SetMainLanguage(language);
        Debug.Log("Setting language to " + language.ToString());

        foreach (var texte in textes)
        {
            texte.LanguageUpdate();
        }
    }

    public void SetLanguageFrench()
    {
        
        SetLanguage(Language.French);
    }
    
    public void SetLanguageEnglish()
    {
        SetLanguage(Language.English);
    }

    public void LanguageSubscribe(TextLocaliserUI txt)
    {
        if(textes == null) textes = new List<TextLocaliserUI>();
        
        textes.Add(txt);
    }
}
