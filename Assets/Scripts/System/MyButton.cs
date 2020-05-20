using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyButton : Button
{
    private RectTransform content;
    protected override void Start()
    {
        base.Start();
        content = GameObject.FindGameObjectWithTag("ScrollViewer").GetComponent<RectTransform>();
    }


    // public override void OnSelect(BaseEventData eventData)
    // {
    //     base.OnSelect(eventData);
    //     var y = transform.position.y;
    //     
    //     if (PlayerPrefs.HasKey("LastUIPositionY"))
    //     {
    //         var previousY = PlayerPrefs.GetFloat("LastUIPositionY");
    //         if (previousY > y)
    //         {
    //             content.Translate(0,455,0);
    //         }
    //         else if (previousY < y)
    //         {
    //             content.Translate(0,-455,0);
    //         }
    //     }
    //     
    //     PlayerPrefs.SetFloat("LastUIPositionY",y);
    // }

    public override void OnDeselect(BaseEventData eventData)
    {
        var v = Input.GetAxis("MovementVertical1");
        if (v >0.05f)
        {
            content.Translate(0,455 * Mathf.Sign(v),0);
        }
        
        
    }
}
