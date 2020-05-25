using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Ending : MonoBehaviour
{
    [TitleGroup("The big Show")] 
    [SerializeField] private List<GameObject> winners;
    
    private bool raceIsOver;

    private string winnerName;

    private GameObject canvas;
    
    // private Text text;

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
        // text = GetComponentInChildren<Text>();
        canvas.SetActive(false);
        modeUnPlayer = GameObject.FindGameObjectWithTag("Gestionnaire").GetComponent<ModeUnPlayer>();
        nombreDeJoueurAAtteindre = (modeUnPlayer.modeUnJoueur) ? 1 : 2;
        recoltableCatch = 0;
        recoltableInMap = GameObject.FindGameObjectsWithTag("SuperRecoltable").Length;
        foreach (var winner in winners)
        {
            winner.SetActive(false);
        }
    }

    private void SetWin(string name,int winnerIndex)
    {
        Debug.LogWarning("The winner est " + name);
        canvas.SetActive(true);
        // text.text = "The Winner is\n" + name;
        winners[winnerIndex].SetActive(true);
        if (name == "Player One")
        {
            VoiceLinePlaying.PlaySound("event:/DA glitch/Level Design/LD_Victoire_joueur_1", VoiceLinePriority.gigantic);
        }

        if (name == "Player Two")
        {
            VoiceLinePlaying.PlaySound("event:/DA glitch/Level Design/LD_Victoire_joueur_2", VoiceLinePriority.gigantic);
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
                SetWin(winnerName,obj.GetComponent<PlayerNumber>().playerIndex);
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
