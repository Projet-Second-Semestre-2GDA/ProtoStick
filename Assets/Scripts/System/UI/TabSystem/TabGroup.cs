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
    
    [Title("Sprite Parameters")]
    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;
    
    [Title("Parameters")] 
    [SerializeField] private Transform pagesHolder;
    [SerializeField] private bool thereIsAFirstSelected = false;
    
    
    

    [HideInInspector] public TabButton activeTab;
    [HideInInspector] public TabButton selectedTab;

    public void Subscribe(TabButton button)
    {
        if (tabButtons == null) tabButtons = new List<TabButton>();
        
        if (!tabButtons.Contains(button)) tabButtons.Add(button);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
        if (activeTab == null || button != activeTab)
        {
            button.background.sprite = tabHover;
        }

        selectedTab = button;
    }

    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }

    public void OnTabSelected(TabButton button)
    {
        if (activeTab != null) activeTab.Deselect();
        
        activeTab = button;
        
        activeTab.Select();
        
        ResetTabs();
        button.background.sprite = tabActive;
        button.background.color = Color.HSVToRGB(0, 0, 3,true);

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
            if(activeTab!= null && button == activeTab) continue;
            button.background.color = Color.HSVToRGB(0, 0, 0.8f, false);
            button.background.sprite = tabIdle;
        }
    }

    public void Restart()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].SetActive(false);
        }
        activeTab = null;
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
