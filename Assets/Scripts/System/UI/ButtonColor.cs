using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Button Color", menuName = "Button Color", order = 2)]
public class ButtonColor : ScriptableObject
{
    public Color normalColor = Color.white;
    public Color highlightedColor= Color.white;
    public Color pressedColor= Color.white;
    public Color selectedColor= Color.white;
    public Color disableColor= Color.white;
}
