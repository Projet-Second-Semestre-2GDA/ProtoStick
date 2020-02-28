using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [Range(1, 10)]
    public float jumpVelocity;

    private bool isGrounded;

    private void Update()
    {

        if (isGrounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                isGrounded = false;
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
                
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {

        isGrounded = true;

    }

}
