using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceTriggerZone : MonoBehaviour
{
    public int zoneNumber;

    private bool isActivate = false;


    private void OnTriggerEnter(Collider other)
    {
        if (!isActivate && other.CompareTag("Player"))
        {
            isActivate = true;
            var playerNumber = other.attachedRigidbody.GetComponent<PlayerNumber>().playerNumber;
            VoiceLinePlaying.PlaySound("event:/DA glitch/Level Design/LD_entree_zone_" + zoneNumber + "_joueur_" + playerNumber, VoiceLinePriority.big);
        }

    }

}
