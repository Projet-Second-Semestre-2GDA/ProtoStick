using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [Range(1, 10)]
    public float jumpVelocity = 1;

    [SerializeField]public bool isGrounded;

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
        Debug.Log("I'm collising with " + collision.collider.tag);
        if (collision.collider.CompareTag("Ground")|| collision.collider.CompareTag("Player"))
        {
            isGrounded = true;
        }
    }

}
