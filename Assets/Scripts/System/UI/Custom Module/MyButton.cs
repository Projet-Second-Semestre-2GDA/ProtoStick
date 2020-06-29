using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MyButton : Button
{
    private RectTransform content;
    private bool isSelected; //Boolean qui me sert à savoir si le bouton est selectionner.

    protected override void Awake()
    {
        base.Awake();
        isSelected = false;
    }

    protected override void Start()
    {
        base.Start();
        content = GameObject.FindGameObjectWithTag("ScrollViewer").GetComponent<RectTransform>();
        //Cette ligne permet de différencier mes différents bouton à l'écran lorsque je test le scroll
        // GetComponentInChildren<TextMeshProUGUI>().text += " - " + Random.value;
    }
    
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        isSelected = true;
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        isSelected = false;
    }

    private void Update()
    {
        if (isSelected)
        {
            //Si le bouton est selectionner, je vérifier ma position
            //Si je suis en dehors du cadre, je fait un translate au parent
            //(Les valeurs ont été écrite en durrrr pour correspondre à ce cas précis.)
            var contentY = content.transform.localPosition.y;
            var y = transform.localPosition.y;
            var realY = contentY + y;
            // //Debug.Log(contentY + " + " + y + " = " + realY);
            if (realY > 0)
            {
                content.Translate(0,-455,0);
            }
            else if (realY < -1080)
            {
                content.Translate(0,455,0);
            }
        }
    }
}