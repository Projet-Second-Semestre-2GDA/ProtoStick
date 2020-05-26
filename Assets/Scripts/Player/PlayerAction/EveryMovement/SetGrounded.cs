using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGrounded : MonoBehaviour
{
    public Jump jump;
    public ModifiedGravity modifiedGravity;
    public Movement movement;

    private bool feedbackHasPlayed = false;
    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("I'm collising with " + collider.tag);
        if (collider.CompareTag("Ground")|| collider.CompareTag("Player")/*|| collision.collider.CompareTag("Bumper")*/)
        {
            Debug.Log("Je suis grounded.");
            jump.PlayerIsGround();
            if (/*(!feedbackHasPlayed && modifiedGravity.totalFall >= 6) || */movement.hasTheMultiplicatorReset)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Personnage longiforme/joueur_touche_sol", transform.position);
                feedbackHasPlayed = true;
            }
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("I'm collising with " + collider.tag);
        if (collider.CompareTag("Ground") || collider.CompareTag("Player")/*|| collision.collider.CompareTag("Bumper")*/)
        {
            Debug.Log("Je suis plus grounded.");

            jump.PlayerIsNotGround();

            feedbackHasPlayed = false;

            movement.hasTheMultiplicatorReset = false;
        }
    }
    
}
