using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[RequireComponent(typeof(Image))]
public class TabButton : 
    MonoBehaviour, 
    // ISelectHandler, ISubmitHandler,IDeselectHandler,
    IPointerEnterHandler, IPointerClickHandler,IPointerExitHandler
{
    [Title("Parameters")]
    public TabGroup tabGroup;
    public Image background;
    public bool isAQuitButton = false;
    
    [Title("Event Systems")]
    public UnityEvent onTabSelected;
    public UnityEvent onTabDeselected;

    private void Awake()
    {
        if(background == null)background = GetComponent<Image>();
        if(tabGroup == null) tabGroup = GetComponentInParent<TabGroup>();
        tabGroup.Subscribe(this);
    }
    [TitleGroup("Usefull Tools"),Button(ButtonSizes.Large)]
    private void AutoSearch()
    {
        tabGroup = GetComponentInParent<TabGroup>();
        background = GetComponent<Image>();
    }

    // //Supposement à la manette
    // public void OnSelect(BaseEventData eventData)
    // {
    //     tabGroup.OnTabSelected(this);
    // }
    //
    // public void OnSubmit(BaseEventData eventData)
    // {
    //     tabGroup.OnTabSelected(this);
    // }
    //
    // public void OnDeselect(BaseEventData eventData)
    // {
    //     tabGroup.OnTabExit(this);
    // }
    
    
    //A la sourie
    public void OnPointerEnter(PointerEventData eventData)
    {
        tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
        if (isAQuitButton) Application.Quit();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }
    
    public void Select()
    {
        onTabSelected.Invoke();
    }

    public void Deselect()
    {
        onTabDeselected.Invoke();
    }
}
