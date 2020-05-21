using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning : MonoBehaviour
{
    private bool hasBegin = false;
    [SerializeField] private GameObject globaleUI;
    [SerializeField] private List<GameObject> listApperanceOnBegining;
    [SerializeField] private List<GameObject> Over;
    [SerializeField] private float timeBetweenBegining = 1f;
    [SerializeField] private GameObject[] players;
    private bool pause = false;
    private List<float> compteur = new List<float>();
    private float timePass = 0;
    private void Start()
    {
        

        hasBegin = false;
        
        for (int i = 0; i < players.Length; i++)
        {
            // players[i].GetComponent<Movement>().DisableMovement(((listApperanceOnBegining.Count - 1)*timeBetweenBegining) + 0.5f);
            players[i].GetComponent<TimerShower>().StopChrono();
            
        
        }

        for (int i = 0; i < listApperanceOnBegining.Count; i++)
        {
            listApperanceOnBegining[i].SetActive(false);
            compteur.Add(timePass + i*timeBetweenBegining + 0.5f);
        }
        listApperanceOnBegining[0].SetActive(true);

    }

    private void Update()
    {
        if (!hasBegin && !pause)
        {
            timePass += Time.deltaTime;
            for (int i = 0; i < compteur.Count; i++)
            {
                if (timePass >compteur[i])
                {
                    listApperanceOnBegining[i].SetActive(false);
                }
                else
                {
                    listApperanceOnBegining[i].SetActive(true);
                    break;
                }
            }
    
            if (timePass > compteur[compteur.Count - 1])
            {
                foreach (var obj in Over)
                {
                    obj.SetActive(false);
                }
                
                for (int i = 0; i < players.Length; i++)
                {
                    players[i].GetComponent<TimerShower>().StartChrono();
                }

                hasBegin = true;
            }
        }

    }

    public void Pause()
    {
        pause = true;
        globaleUI.SetActive(false);
    }

    public void Resume()
    {
        pause = false;
        globaleUI.SetActive(true);
    }
}
