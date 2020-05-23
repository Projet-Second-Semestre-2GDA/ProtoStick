using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRecoltable : MonoBehaviour
{
    // [SerializeField] private float puissance = 20;
    private bool isRecolte;

    private void Awake()
    {
        isRecolte = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player") || isRecolte) return;
        
        //feedback FMOD
        // FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Level Design/LD_Recoltables", transform.position);
        var rb = other.attachedRigidbody;
        rb.GetComponent<RecolteObject>().AddRecoltable(gameObject);
        // rb.GetComponent<RecolteObject>().Boost(puissance);
        isRecolte = true;

    }
}
