using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ralentisseur : MonoBehaviour
{
    [SerializeField] private float reductor = 4;
    List<GameObject> playerIn = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb;
            (rb = other.attachedRigidbody).GetComponent<Movement>().ReductionSpeed(reductor);
            playerIn.Add(rb.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (var player in playerIn)
        {
            if (player.name == other.attachedRigidbody.gameObject.name)
            {
                player.GetComponent<Movement>().StopReduc();
            }
        }
    }
}
