using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpTrick : PlayerEffect
{
    public float bumpForce;

    private void Awake()
    {
        effect = "Bump";
    }

    protected override void Effect(GameObject playerWhoTouch)
    {
        var otherRb = playerWhoTouch.GetComponentInParent<Rigidbody>();
        var velocity = otherRb.velocity;
        velocity = new Vector3(velocity.x, bumpForce, velocity.y);
        otherRb.velocity = velocity;
    }
}