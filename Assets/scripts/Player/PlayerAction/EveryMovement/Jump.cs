using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Jump : MonoBehaviour
{
    private PlayerNumber playerNumber;
    //[Range(1, 10)]
    [SerializeField]private float jumpVelocity = 1;

    [SerializeField] private int numberOfJump = 2;

    [SerializeField] bool firstJumpStable = true;

    private bool isGrounded;

    private int jumpDone = 0;

    private void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
    }

    private void Update()
    {
        Debug.Log(name + "a pour input" + "Jump" + playerNumber.playerNumber);
        if (Input.GetButtonDown("Jump" + playerNumber.playerNumber))
        {
            Debug.Log("Jump" + playerNumber.playerNumber);
            if (jumpDone == 0)
            {
                ++jumpDone;
                isGrounded = false;
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
                FMODUnity.RuntimeManager.PlayOneShot("event:/DA placeholder/personnage/premier_saut", transform.position);
            }
            else if (jumpDone < numberOfJump)
            {
                ++jumpDone;
                isGrounded = false;
                GetComponent<Rigidbody>().velocity = Vector3.up * jumpVelocity;
                FMODUnity.RuntimeManager.PlayOneShot("event:/DA placeholder/personnage/double_saut", transform.position);
            }

            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("I'm collising with " + collision.collider.tag);
        if (collision.collider.CompareTag("Ground")|| collision.collider.CompareTag("Player"))
        {
            isGrounded = true;
            jumpDone = 0;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("I'm collising with " + collision.collider.tag);
        if (collision.collider.CompareTag("Ground") || collision.collider.CompareTag("Player"))
        {
            isGrounded = false;
            
        }
    }

    public bool PlayerIsGrounded()
    {
        return (isGrounded || ((firstJumpStable) ? jumpDone <= 1 : jumpDone <= 0));
    }

}
