using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralentisseur : MonoBehaviour
{
    private FMOD.Studio.EventInstance ralentisseurFeedback;
    
    [SerializeField] private float vitesseimposée = 15f;

    [SerializeField] private bool ralentisseurStopUpgraderSpeed = true; 

    [SerializeField] private float duree = 1f;
    // List<GameObject> playerIn = new List<GameObject>();

    private void Start()
    {
        ralentisseurFeedback = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_ralentisseur_collision");
    }

    private void Update()
    {
        ralentisseurFeedback.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Ralentissement(other);
        }

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Ralentissement(other);
        }
    }

    private void Ralentissement(Collider other)
    {
        ralentisseurFeedback.start();
        Rigidbody rb;
        (rb = other.attachedRigidbody).GetComponent<Movement>().ReductionSpeed(vitesseimposée, duree);
        if (ralentisseurStopUpgraderSpeed)
        {
            rb.GetComponent<Movement>().ResetUpgrade();
            rb.GetComponent<ModifiedGravity>().ForceGoDown();
        }

        // rb.velocity = rb.velocity.normalized;
        // playerIn.Add(rb.gameObject);
    }
}
