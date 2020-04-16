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
        Debug.LogWarning("Le winner est " + name);
        canvas.SetActive(true);
        text.text = "The Winner is\n" + name;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player") && !raceIsOver)
        {
            raceIsOver = true;
            winnerName = other.attachedRigidbody.gameObject.name;
            SetWin(winnerName);
        }
    }
}
