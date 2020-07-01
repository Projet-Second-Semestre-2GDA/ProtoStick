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
    Selectable,
    // ISelectHandler, ISubmitHandler,IDeselectHandler,
    IPointerEnterHandler, IPointerClickHandler,IPointerExitHandler,ISubmitHandler
    
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
        Selecting();
    }

    

    public void OnPointerClick(PointerEventData eventData)
    {
        Using();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Deselecting();
    }
    
    //Call by tabGroup
    public void Select()
    {
        
        onTabSelected.Invoke();
    }

    public void Deselect()
    {
        onTabDeselected.Invoke();
    }
    
    //Selectable override function
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Selecting();
        
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Deselecting();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Using();
    }
    
    //Private function I use
    
    private void Selecting()
    {
        tabGroup.OnTabEnter(this);
        DoStateTransition(SelectionState.Selected, false);
    }

    private void Using()
    {
        // if (!IsActive() || !IsInteractable()) return;
        
        tabGroup.OnTabSelected(this);
        DoStateTransition(SelectionState.Pressed, false);
        if (isAQuitButton) Application.Quit();
    }

    private void Deselecting()
    {
        tabGroup.OnTabExit(this);
        DoStateTransition(SelectionState.Normal, false);
    }
}
