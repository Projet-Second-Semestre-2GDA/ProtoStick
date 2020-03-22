using System;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    protected string effect = "";
    [HideInInspector] public bool canWork = false;
    protected virtual void Effect(GameObject playerWhoTouch)
    {
        Debug.Log(this.name + " do " + effect);
    } 
    
    protected void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ball") || !collision.gameObject.CompareTag("Player") || !canWork) return;
        Effect(collision.collider.gameObject);
    }


}
