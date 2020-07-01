using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;


public class CustomSlider : MonoBehaviour
{
    public Image image;
    public Slider sliderAssociate;
    [Range(0,1)]
    [ReadOnly]public float sliderAmount = 1f;

    private Vector2 minMax;

    private void Start()
    {
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Horizontal;
        minMax = new Vector2(sliderAssociate.minValue,sliderAssociate.maxValue);
    }

    void Update()
    {
        FillImage(sliderAssociate.value);
    }
    
    public void FillImage(float quantitie)
    {
        var amount = (quantitie - minMax.x) / (minMax.y - minMax.x);
        // Debug.Log("Filling of " + amount);
        image.fillAmount = amount;
        sliderAmount = amount;
    }
    
}
