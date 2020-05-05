using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FMOD;
using Unity.Collections;
// using UnityEditorInternal;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Jump : MonoBehaviour
{
    private PlayerNumber playerNumber;
    //[Range(1, 10)]
    [SerializeField]private float jumpVelocity = 1;

    [SerializeField] private int numberOfJump = 2;

    [SerializeField] bool firstJumpStable = true;

    private Rigidbody rb;
    private ModifiedGravity modifiedGravity;

    [SerializeField,ReadOnly] public bool isGrounded;
    private int jumpDone = 0;

    private float actualHeight = -1;
    private float futurHeight = -1;
    private bool forceJump = false;
    
    
    private float nextCheck = 0;
    private float timeBetweenCheck = 0.4f;
    private float heightPreviousCheck = 0;


    private bool active = true;
    private bool hasToActive = false;
    private float realActive;

    private void Awake()
    {
        active = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        modifiedGravity = GetComponent<ModifiedGravity>();
        playerNumber = GetComponent<PlayerNumber>();
    }

    private void Update()
    {
        if (active)
        {
            // Debug.Log(name + "a pour input" + "Jump" + playerNumber.playerNumber);
            if (Input.GetButtonDown("Jump" + playerNumber.playerNumber))
            {
                // DoJump();
                // Debug.Log("Jump" + playerNumber.playerNumber);
                if (jumpDone == 0)
                {
                    ++jumpDone;
                    isGrounded = false;
                    // SetVelocityY(jumpVelocity);
                    DoJump();
                    FMODUnity.RuntimeManager.PlayOneShot("event:/DA placeholder/personnage/premier_saut", transform.position);
                }
                else if (jumpDone < numberOfJump)
                {
                    ++jumpDone;
                    isGrounded = false;
                    DoJump();
                    // SetVelocityY(jumpVelocity);
                    FMODUnity.RuntimeManager.PlayOneShot("event:/DA placeholder/personnage/double_saut", transform.position);
                }
    
                
            }
    
            if (forceJump)
            {
                SetVelocityY(jumpVelocity);
                var actualY = transform.position.y;
                bool forceStop = false;
                if (Time.time > nextCheck)
                {
                    SetNextCheck(timeBetweenCheck);
                    forceStop = (actualY - heightPreviousCheck) < 0.2f;
                    heightPreviousCheck = actualY;
                }
                actualHeight = actualY;
                if (actualHeight >futurHeight || forceStop)
                {
                    forceJump = false;
                    SetVelocityY(jumpVelocity/2);
                    // modifiedGravity.ForceGoDown();
                }
            }
        }

        if (hasToActive)
        {
            active = true;
            hasToActive = false;
            // if (Time.time > realActive)
            // {
            //     
            // }
        }
    }

    private void DoJump(float height = 2)
    {
        actualHeight = transform.position.y;
        heightPreviousCheck = actualHeight;
        futurHeight = actualHeight + height;
        forceJump = true;
        SetNextCheck(timeBetweenCheck);
    }

    private void SetNextCheck(float time)
    {
        nextCheck = Time.time + time;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("I'm collising with " + collision.collider.tag);
        if (collision.collider.CompareTag("Bumper"))
        {
            PlayerIsGround();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        //Debug.Log("I'm collising with " + collision.collider.tag);
        if (collision.collider.CompareTag("Bumper"))
        {
            PlayerIsNotGround();
        }
    }

    private void SetVelocityY(float y)
    {
        var velocity = rb.velocity; 
        velocity.y = y;
        rb.velocity = velocity;
    }
    
    public bool IsPlayerOnJump()
    {
        return (isGrounded || ((firstJumpStable) ? jumpDone <= 1 : jumpDone <= 0));
    }

    public void SetThrowJump(bool isActive)
    {
        if (!active && isActive)
        {
            hasToActive = true;
            // realActive = Time.time + 0.2f;
        }
        else
        {
            hasToActive = false;
            active = isActive;
        }
    }

    public void PlayerIsGround()
    {
        isGrounded = true;
        jumpDone = 0;
    }

    public void PlayerIsNotGround()
    {
        isGrounded = false;
    }

}
