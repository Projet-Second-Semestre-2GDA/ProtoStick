using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Sirenix.OdinInspector;
using UnityEngine;

public class BumpTrick : PlayerEffect
{
    [Title("Bump Properties")]
    [SerializeField, PropertyRange(0, 100)] private  float bumpForce;
    [SerializeField, Range(1, 5)] private float multiplicationPriority = 2;

    [Title("Speed Player Modification")] 
    [SerializeField,Range(0,10)] private float speedMultiplicator = 1.5f;
    // [SerializeField,Range(0,10)] private float speedMultiplicatorDuration = 2f;
    

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
        playerWhoTouch.GetComponent<Movement>().UpgradeSpeed(speedMultiplicator);
        var force = transform.forward * bumpForce;
        //------------------Old------------------
        //Debug.Log("La force est : " + force);
        var otherValue = new float[] {force.x,force.y,force.z};
        var values = new float[] {velocity.x,velocity.y,velocity.z};
        for (int i = 0; i < values.Length; i++)
        {
            values[i] = (Math.Abs(otherValue[i]) > 1f) ? otherValue[i] : values[i];
        }
        
        velocity = new Vector3(values[0],values[1],values[2]);
        
        // velocity = (velocity.magnitude > bumpForce) ? velocity.normalized * bumpForce : velocity;
        // //Debug.Log("Ca vélocité est maintenant : " + velocity);
        velocityGiven = velocity;
        // if (transform.forward != Vector3.up && transform.forward != Vector3.down)
        // {
        //     playerWhoTouch.GetComponent<Movement>().DisableMovement(0.25f);
        // }
        otherRb.velocity = velocity;
        //------------------Old------------------
        // otherRb.AddForce(force*100,ForceMode.Impulse);
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Bumper/propulsion_bumper", transform.position);
        
        
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

