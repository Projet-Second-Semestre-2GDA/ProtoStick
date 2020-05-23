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
        var customsButtons = GetComponentsInChildren<CustomButton>();
        var buttons2 = GetComponents<Button>();
        var customsButtons2 = GetComponents<CustomButton>();

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
        foreach (var button in buttons2)
        {
            var colors = button.colors;
            colors.highlightedColor = buttonColor.highlightedColor;
            colors.normalColor = buttonColor.normalColor;
            colors.pressedColor = buttonColor.pressedColor;
            colors.selectedColor = buttonColor.selectedColor;
            colors.disabledColor = buttonColor.disableColor;
            button.colors = colors;
        }
        foreach (var button in customsButtons)
        {
            var colors = button.colors;
            colors.highlightedColor = buttonColor.highlightedColor;
            colors.normalColor = buttonColor.normalColor;
            colors.pressedColor = buttonColor.pressedColor;
            colors.selectedColor = buttonColor.selectedColor;
            colors.disabledColor = buttonColor.disableColor;
            button.colors = colors;
        }
        foreach (var button in customsButtons2)
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
