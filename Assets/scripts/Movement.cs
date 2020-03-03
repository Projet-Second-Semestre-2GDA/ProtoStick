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
        //var direction = new Vector3(0f, 0f, v * speedDeplacement * Time.deltaTime);
        //transform.Translate(direction);
        var velocity = rb.velocity;
        Debug.Log(v.ToString() + jump.isGrounded.ToString());
        if (Mathf.Abs(v) > 0.1f)
        {
            velocity = velocity + v * speedDeplacement * transform.forward;
        }
        else if(jump.isGrounded){
            velocity.x = 0;
            velocity.z = 0;
        }
        
        
        rb.velocity = velocity;

        transform.Rotate(0f, h * speedRotation * Time.deltaTime, 0f);
        

    }
    public void DisableScript()
    {
        this.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        this.GetComponent<Movement>().enabled = false;
    }



}
