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

    private GameObject gestionnaire;
    private GlobalTimer timer;

    private void Start()
    {
        gestionnaire = GameObject.FindGameObjectWithTag("Gestionnaire");
        timer = gestionnaire.GetComponent<GlobalTimer>();
        hasBegin = false;


        for (int i = 0; i < listApperanceOnBegining.Count; i++)
        {
            listApperanceOnBegining[i].SetActive(false);
            compteur.Add(timePass + i*timeBetweenBegining + 0.5f);
        }
        listApperanceOnBegining[0].SetActive(true);
        timer.StopAndRest();
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
                TheGameHasBegin.theGameHasBegin = true; //là ou il y a la flèche rouge
                timer.Start();
                foreach (var obj in Over)
                {
                    obj.SetActive(false);
                }
                

                hasBegin = true;
            }
            else TheGameHasBegin.theGameHasBegin = false;

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
