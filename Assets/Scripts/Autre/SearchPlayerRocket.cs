using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayerRocket : MonoBehaviour
{
    private float speedMax;
    private Rigidbody rb;
    private float toAddForce = 200;
    private void Start()
    {
        speedMax = (rb = GetComponentInParent<Rigidbody>()).velocity.magnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var playerPos = other.attachedRigidbody.transform.position;
            speedMax = rb.velocity.magnitude;

            var direction = playerPos - transform.position;
            
            rb.AddForce(direction.normalized*toAddForce,ForceMode.Force);
            
            var velocity = rb.velocity;
            rb.velocity = (velocity.magnitude > speedMax) ? velocity.normalized * speedMax : velocity;
        }
    }
}
