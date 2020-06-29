using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//Système réaliser grace à la vidéo suivante :
//https://youtu.be/211t6r12XPQ
public class TabGroup : MonoBehaviour
{
    [Title("Listes")]
    [ReadOnly ,ListDrawerSettings(Expanded = true)]public List<TabButton> tabButtons;
    [ReadOnly ,ListDrawerSettings(Expanded = true)]public List<GameObject> pages;
    
    [Title("Color Parameters")]
    public Color tabIdle;
    public Color tabHover;
    public Color tabActive;
    
    [Title("Parameters")] 
    [SerializeField] private Transform pagesHolder;
    [SerializeField] private bool thereIsAFirstSelected = false;
    
    
    

    [HideInInspector] public TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null) tabButtons = new List<TabButton>();
        
        if (!tabButtons.Contains(button)) tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
        {
            button.background.color = tabHover;
        }
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (selectedTab != null) selectedTab.Deselect();
        
        selectedTab = button;
        
        selectedTab.Select();
        
        ResetTabs();
        button.background.color = tabActive;

        int index = button.transform.GetSiblingIndex();
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(i == index);
        }
    }

    public void ResetTabs()
    {
        foreach (TabButton button in tabButtons)
        {
            if(selectedTab!= null && button == selectedTab) continue;
            button.background.color = tabIdle;
        }
    }

    public void Restart()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(false);
        }
        selectedTab = null;
        ResetTabs();
    }

    private void GetPages()
    {
        pages = new List<GameObject>();
        for (int i = 0; i < pagesHolder.childCount; i++)
        {
            pages.Add(pagesHolder.GetChild(i).gameObject);
        }
    }

    private void Start()
    {
        GetPages();
        ResetTabs();
        if (thereIsAFirstSelected)
        {
            OnTabSelected(tabButtons[0]);
        }
        else
        {
            for (int i = 0; i < pages.Count; i++)
            {
                pages[i].SetActive(false);
            }
        }
        
    }
}
