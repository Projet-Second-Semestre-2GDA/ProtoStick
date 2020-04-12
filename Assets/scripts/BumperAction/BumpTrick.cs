using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class BumpTrick : PlayerEffect
{
    [SerializeField, PropertyRange(0, 100)] private  float bumpForce;

    // [SerializeField, PropertyRange(0, 50)]
    // private float treeshold = 10;


    private Vector3 velocityGiven = Vector3.zero;
    private void Awake()
    {
        effect = "Bump";
    }

    protected override void Effect(GameObject playerWhoTouch)
    {
        base.Effect(playerWhoTouch);
        var otherRb = playerWhoTouch.GetComponentInParent<Rigidbody>();
        var velocity = otherRb.velocity;
        
        var force = transform.forward * bumpForce;
        Debug.Log("La force est : " + force);
        var otherValue = new float[] {force.x,force.y,force.z};
        var values = new float[] {velocity.x,velocity.y,velocity.z};
        for (int i = 0; i < values.Length; i++)
        {
            if ((otherValue[i] > 0 && values[i] < 0 )|| (otherValue[i] < 0 && values[i] > 0))
            {
                values[i] = -values[i];
            }
            values[i] += otherValue[i];
            
        }
        
        velocity = new Vector3(values[0],values[1],values[2]);

        velocity = (velocity.magnitude > bumpForce) ? velocity.normalized * bumpForce : velocity;
        Debug.Log("Ca vélocité est maintenant : " + velocity);
        velocityGiven = velocity;
        playerWhoTouch.GetComponent<Movement>().DisableMovement(0.25f);
        otherRb.velocity = velocity;
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA placeholder/personnage/saut_bumper", transform.position);
        
        
    }

    private void OnDrawGizmos()
    {
        if (velocityGiven != Vector3.zero)
        {
            Gizmos.color = Color.green;
            
            Gizmos.DrawLine(transform.position,transform.position + velocityGiven);
        }
    }
}