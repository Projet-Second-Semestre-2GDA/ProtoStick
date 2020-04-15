using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoltable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.attachedRigidbody.GetComponent<RecolteObject>().AddRecoltable(gameObject);
    }
}
