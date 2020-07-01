using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocaliserUI : MonoBehaviour
{
    private TextMeshProUGUI textField;

    public LocalisedString LocalisedString;
    public string toAddBefore;
    public string toAddAfter;
    

    private void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        LanguageUpdate();
        GameObject.FindGameObjectWithTag("Gestionnaire").GetComponent<LanguageSelection>().LanguageSubscribe(this);
    }

    public void LanguageUpdate()
    {
        textField.text = toAddBefore + LocalisedString.value + toAddAfter;
    }
}
