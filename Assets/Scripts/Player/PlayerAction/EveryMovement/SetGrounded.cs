using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGrounded : MonoBehaviour
{
    public Jump jump;
    
    private void OnTriggerEnter(Collider collider)
    {
        Debug.Log("I'm collising with " + collider.tag);
        if (collider.CompareTag("Ground")|| collider.CompareTag("Player")/*|| collision.collider.CompareTag("Bumper")*/)
        {
            Debug.Log("Je suis grounded.");
            jump.PlayerIsGround();
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        Debug.Log("I'm collising with " + collider.tag);
        if (collider.CompareTag("Ground") || collider.CompareTag("Player")/*|| collision.collider.CompareTag("Bumper")*/)
        {
            Debug.Log("Je suis plus grounded.");

            jump.PlayerIsNotGround();
        }
    }
    
}
