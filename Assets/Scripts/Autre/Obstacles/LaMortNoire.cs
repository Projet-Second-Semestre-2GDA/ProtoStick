using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LaMortNoire : MonoBehaviour
{
    [Title("Parameters :")]
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private float durationCantMoveAfterRespawn = 1f;

    private void OnCollisionEnter(Collision other)
    {
        var player = other.collider;
        RespawnPlayer(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        RespawnPlayer(other);
    }

    private void OnCollisionStay(Collision other)
    {
        var player = other.collider;
        RespawnPlayer(player);
    }

    private void OnTriggerStay(Collider other)
    {
        RespawnPlayer(other);
    }

    private void RespawnPlayer(Collider playerCollider)
    {
        Debug.Log("I'm in " + playerCollider.name);
        if (playerCollider.CompareTag("Player"))
        {
            var attachedRigidbody = playerCollider.attachedRigidbody;
            var player = attachedRigidbody.gameObject;
            Debug.Log(player.name + " is in");
            var transPlayer = player.transform;
            transPlayer.position = respawnPoint.position;
            transPlayer.rotation = Quaternion.identity;
            transPlayer.rotation = respawnPoint.rotation;
            
            attachedRigidbody.velocity = new Vector3();
            attachedRigidbody.angularVelocity = new Vector3();
            player.GetComponent<Movement>().DisableMovement(durationCantMoveAfterRespawn);
        }
    }
}
