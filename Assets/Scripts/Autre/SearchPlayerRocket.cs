﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SearchPlayerRocket : MonoBehaviour
{
    private float speedMax;
    private Rigidbody rb;
    [FormerlySerializedAs("toAddForce")] [SerializeField] private float multiplicator = 1f;
    private void Start()
    {
        speedMax = (rb = GetComponentInParent<Rigidbody>()).velocity.magnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I found " + other.attachedRigidbody.name + " and go to him !!");
            var playerPos = other.attachedRigidbody.transform.position;
            speedMax = rb.velocity.magnitude;

            var direction = playerPos - transform.position;
            var velocity = rb.velocity;
            velocity += direction.normalized * (speedMax* multiplicator * Time.fixedDeltaTime);
            // rb.AddForce(direction.normalized*multiplicator,ForceMode.Force);
            
            rb.velocity = (velocity = velocity.normalized * speedMax);
            rb.transform.LookAt(transform.position + velocity);
        }
    }
}
