using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetButtonColor : MonoBehaviour
{
    public ButtonColor buttonColor;
    private void Start()
    {
        var buttons = GetComponentsInChildren<Button>();

        foreach (var button in buttons)
        {
            var colors = button.colors;
            colors.highlightedColor = buttonColor.highlightedColor;
            colors.normalColor = buttonColor.normalColor;
            colors.pressedColor = buttonColor.pressedColor;
            colors.selectedColor = buttonColor.selectedColor;
            colors.disabledColor = buttonColor.disableColor;
            button.colors = colors;
        }
    }
}
