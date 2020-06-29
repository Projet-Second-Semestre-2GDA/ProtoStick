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

    private void Start()
    {
        textField = GetComponent<TextMeshProUGUI>();
        textField.text = LocalisedString.value;
    }
}
