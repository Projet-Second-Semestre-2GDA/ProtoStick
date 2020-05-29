using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SearchPlayerRocket : MonoBehaviour
{
    private float speedMax;
    private Rigidbody rb;
    [SerializeField] private float multiplicator = 1f;
    private bool canDetect = false;
    private void Start()
    {
        speedMax = (rb = GetComponentInParent<Rigidbody>()).velocity.magnitude;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("I found " + other.name);
        if (other.CompareTag("Player") && canDetect)
        {
            bool canWork = false;
            var playerPos = other.attachedRigidbody.transform.position;
            var trans = rb.transform;
            
            var heading = playerPos - trans.position;
            var dot = Vector3.Dot(heading, trans.forward);

            canWork = dot > 0;

            if (canWork)
            {
                Debug.Log("I found " + other.attachedRigidbody.name + " and go to him !!");
                speedMax = rb.velocity.magnitude;

                var direction = playerPos - transform.position;
                var velocity = rb.velocity;
                velocity += direction.normalized * (speedMax* multiplicator * Time.fixedDeltaTime);
                // rb.AddForce(direction.normalized*multiplicator,ForceMode.Force);
                velocity = velocity.normalized * speedMax;
                rb.velocity = velocity;
            }
            
        }
    }

    private void LateUpdate()
    {
        canDetect = true;
    }
}
