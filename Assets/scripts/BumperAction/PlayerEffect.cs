using System;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    protected string effect = "";
    // [HideInInspector] public bool canWork = false;
    protected virtual void Effect(GameObject playerWhoTouch)
    {
        //Debug.Log(this.name + " do " + effect);
    } 
    
    protected void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Effect(other.attachedRigidbody.gameObject);
        }
    }
    protected void OnCollisionEnter(Collision col)
    {
        Collider other = col.collider;
        //Debug.Log("Collision with " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Effect(other.attachedRigidbody.gameObject);
        }
    }


}
