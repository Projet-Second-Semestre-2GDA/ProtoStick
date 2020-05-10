using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoltable : MonoBehaviour
{
    [SerializeField] private float puissance = 20;
    private void OnTriggerEnter(Collider other)
    {
        var rb = other.attachedRigidbody;
        rb.GetComponent<RecolteObject>().AddRecoltable(gameObject);
        rb.GetComponent<RecolteObject>().Boost(puissance);

    }
}
