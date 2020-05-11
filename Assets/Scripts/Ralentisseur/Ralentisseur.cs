using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralentisseur : MonoBehaviour
{
    
    [SerializeField] private float vitesseimposée = 15f;

    [SerializeField] private float duree = 1f;
    // List<GameObject> playerIn = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.CompareTag("Player"))
        {
            Rigidbody rb;
            (rb = other.attachedRigidbody).GetComponent<Movement>().ReductionSpeed(vitesseimposée, duree);
            // rb.velocity = rb.velocity.normalized;
            // playerIn.Add(rb.gameObject);
        }

    }
}
