﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecolteObject : MonoBehaviour
{
    [SerializeField] private Text RecoltableShower;
    private int objectRecolted = 0;
    private string tagRecolt = "Recoltable";
    private Rigidbody rb;

    private bool haveBeenBoost = false;
    [SerializeField] private float durationBoost = 0.25f;
    private float timerNextBoost = 0;

    private void Awake()
    {
        objectRecolted = 0;
        timerNextBoost = Time.time;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        RecoltableShower.text = objectRecolted.ToString()/*+ " " + ((objectRecolted < 2) ? "objet récolté" : "objets récoltés")*/;
        if (haveBeenBoost && Time.time >= timerNextBoost)
        {
            haveBeenBoost = false;
        }
    }

    public void AddRecoltable(GameObject obj)
    {
        ++objectRecolted;
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Level Design/LD_Super_Recoltable", transform.position);
        // UniversalRecoltObject.AddRecoltable();
        Destroy(obj);
    }

    public int GetNumberRecoltable()
    {
        return objectRecolted;
    }

    public void Boost(float puissance, GameObject obj)
    {
        if (!haveBeenBoost)
        {
            
            rb.velocity +=  transform.forward * puissance;
            haveBeenBoost = true;
            timerNextBoost = Time.time + durationBoost;
        }
        Destroy(obj);
    }
}
