using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    
    private bool raceIsOver;

    private string winnerName;

    private GameObject canvas;
    
    private Text text;

    private int numberOfPlayerArrive;

    private ModeUnPlayer modeUnPlayer;

    private int nombreDeJoueurAAtteindre;

    private int recoltableCatch;

    private int recoltableInMap;
    
    private List<string> finishNames = new List<string>();
    
    void Awake()
    {
        raceIsOver = false;
        winnerName = "";
        numberOfPlayerArrive = 0;
        recoltableCatch = 0;
    }

    private void Start()
    {
        canvas = GetComponentInChildren<Canvas>().gameObject;
        text = GetComponentInChildren<Text>();
        canvas.SetActive(false);
        modeUnPlayer = GameObject.FindGameObjectWithTag("Gestionnaire").GetComponent<ModeUnPlayer>();
        nombreDeJoueurAAtteindre = (modeUnPlayer.modeUnJoueur) ? 1 : 2;
        recoltableCatch = 0;
        recoltableInMap = GameObject.FindGameObjectsWithTag("SuperRecoltable").Length;
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
            var objName = obj.name;
            bool found = false;
            foreach (var names in finishNames)
            {
                if (names == objName)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                ++numberOfPlayerArrive;
                var objGet = obj.GetComponent<RecolteObject>().GetNumberRecoltable();
                Debug.Log(obj.name + " a récupéré " + objGet + " récoltable");
                recoltableCatch += objGet;
                Debug.Log("Ce qui fait donc un total de " + recoltableCatch);
            }
            
            if (!raceIsOver)
            {
                raceIsOver = true;
                winnerName = obj.name;
                SetWin(winnerName);
            }

            if (numberOfPlayerArrive >= nombreDeJoueurAAtteindre)
            {
                int levelID = SceneManager.GetActiveScene().buildIndex;
                UniversalRecoltObject.RaceOver(levelID,recoltableCatch,recoltableInMap);
            }
            finishNames.Add(obj.name);
            obj.GetComponent<TimerShower>().raceEnd();
        }
    }
}
