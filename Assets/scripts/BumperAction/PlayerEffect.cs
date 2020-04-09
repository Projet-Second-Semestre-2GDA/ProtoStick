using System;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    protected string effect = "";
    // [HideInInspector] public bool canWork = false;
    protected virtual void Effect(GameObject playerWhoTouch)
    {
        Debug.Log(this.name + " do " + effect);
    } 
    
    protected void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Player"))
        {
            Effect(collision.collider.attachedRigidbody.gameObject);
        }
    }


}
