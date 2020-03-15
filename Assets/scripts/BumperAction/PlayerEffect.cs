using System;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    protected string effect = "";

    protected virtual void Effect(GameObject playerWhoTouch)
    {
        Debug.Log(this.name + " do " + effect);
    } 
    
    protected void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;
        Effect(collision.collider.gameObject);
    }
}
