using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    Jump jump;
    public float speedRotation = 2;
    public float speedDeplacement = 5;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
    }

    private void FixedUpdate()
    {

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");


        var velocity = rb.velocity;

        if (Mathf.Abs(v) > 0.01f || Mathf.Abs(h) > 0.01f)
        {
            var velocity2 = new Vector3();
            velocity2 +=  v * speedDeplacement * transform.forward;
            velocity2 += h * speedDeplacement * transform.right;
            velocity2 = velocity2.magnitude > speedDeplacement ? velocity2.normalized * speedDeplacement : velocity2;
            if (jump.PlayerIsGrounded())
            {
                velocity.x = velocity2.x;
                velocity.z = velocity2.z;
            }
            else
            {
                velocity2.x += velocity.x;
                velocity2.z += velocity.z;

                if (velocity2.magnitude > speedDeplacement)
                {
                    velocity2 = velocity2.normalized * speedDeplacement;
                }

                velocity.x = velocity2.x;
                velocity.z = velocity2.z;
            }
            
        }
        else if(jump.PlayerIsGrounded()){
            velocity.x = 0;
            velocity.z = 0;
        }
        
        
        rb.velocity = velocity;
        

    }
    public void DisableScript()
    {
        this.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        this.GetComponent<Movement>().enabled = false;
    }



}
