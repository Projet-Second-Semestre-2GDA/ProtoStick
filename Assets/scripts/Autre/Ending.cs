using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    private bool raceIsOver;

    private string winnerName;

    private GameObject canvas;
    
    private Text text;
    
    void Awake()
    {
        raceIsOver = false;
        winnerName = "";
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>().gameObject;
        text = GetComponentInChildren<Text>();
        canvas.SetActive(false);
    }

    private void SetWin(string name)
    {
        Debug.LogWarning("The winner est " + name);
        canvas.SetActive(true);
        text.text = "The Winner is\n" + name;

        if (name == "Player One")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Level Design/LD_Victoire_joueur_1");
        }

        if (name == "Player Two")
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Level Design/LD_Victoire_joueur_2");
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player") )
        {
            var obj = other.attachedRigidbody.gameObject;
            if (!raceIsOver)
            {
                raceIsOver = true;
                winnerName = obj.name;
                SetWin(winnerName);
            }
            obj.GetComponent<TimerShower>().StopChrono();
        }
    }
}
