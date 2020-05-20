using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beginning : MonoBehaviour
{
    private bool hasBegin = false;
    [SerializeField] private List<GameObject> listApperanceOnBegining;
    [SerializeField] private List<GameObject> Over;
    [SerializeField] private float timeBetweenBegining = 1f;
    [SerializeField] private GameObject[] players;
    
    private List<float> compteur = new List<float>();
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
            compteur.Add(Time.time + i*timeBetweenBegining + 0.5f);
        }
        listApperanceOnBegining[0].SetActive(true);

    }

    private void Update()
    {
        if (!hasBegin)
        {
            for (int i = 0; i < compteur.Count; i++)
            {
                if (Time.time >compteur[i])
                {
                    listApperanceOnBegining[i].SetActive(false);
                }
                else
                {
                    listApperanceOnBegining[i].SetActive(true);
                    break;
                }
            }
    
            if (Time.time > compteur[compteur.Count - 1])
            {
                foreach (var obj in Over)
                {
                    obj.SetActive(false);
                }
                //
                // for (int i = 0; i < players.Length; i++)
                // {
                //     // players[i].GetComponent<Movement>().DisableMovement(4.2f);
                //     players[i].GetComponent<TimerShower>().StartChrono();
                // }

                hasBegin = true;
            }
        }

    }
}
