using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Sirenix.OdinInspector;
using Debug = UnityEngine.Debug;

public class Movement : MonoBehaviour
{
    private PlayerNumber playerNumber;
    Rigidbody rb;
    Jump jump;
    public float speedRotation = 100;
    public float speedDeplacement = 30;
    public float speedDeplacementMax = 50;
    [Title("Variable liée au mécanique de vitesse")]
    [SerializeField, PropertyRange(0, 1)] private float timeForChange = 1f;
    [SerializeField, Range(0, 1)] private float reducteurTerrestre, reducteurAerien; 
    [SerializeField, Range(0, 5)] private float timeToSpeedMax;

    [SerializeField, MinMaxSlider(0f, 1f, true)]
    private Vector2 diviseurAcceleration;

    // [Title("Variable liée à l'influence sur la vitesse")] 
    // [SerializeField] private float speedReductor = 2;

    private float reduc = 1;
    private float accelerator = 0;
    private float timePass = 0;
    
    
    private float canMove = 0;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
        playerNumber = GetComponent<PlayerNumber>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("MovementHorizontal" + playerNumber.playerNumber);
        float v = Input.GetAxisRaw("MovementVertical" + playerNumber.playerNumber);

        if (Time.time > canMove)
        {
            Moove(v, h,Time.fixedDeltaTime);
        }
    }

    private void Moove(float v, float h, float deltaTime)
    {
        var velocity = rb.velocity;
        float y = velocity.y;
        
        
        timePass += deltaTime;
        accelerator = Mathf.Lerp(diviseurAcceleration.x, diviseurAcceleration.y, Mathf.Clamp(timePass / timeToSpeedMax, 0, 1));
        
        // var velocity = velocity;
        velocity.y = 0;
        // if (velocity.magnitude > speedDeplacement)
        // {
        //     velocity -= velocity.normalized * speedDeplacement;
        // }
        
        if (Mathf.Abs(v) > 0.01f || Mathf.Abs(h) > 0.01f)
        {
            var trans = transform;
            var temp = trans.forward * (v * (speedDeplacement/reduc) * accelerator);
            temp += trans.right * (h * (speedDeplacement/reduc) * accelerator);
            temp = (temp.magnitude > (speedDeplacement/reduc) * accelerator) ? temp.normalized * speedDeplacement : temp;
            
            
            velocity += temp;
            if ((velocity.magnitude > speedDeplacement))
            {
                velocity -= velocity.normalized * ((velocity.magnitude - speedDeplacement > speedDeplacement)
                    ? speedDeplacement
                    : speedDeplacement - (velocity.magnitude - speedDeplacement));
                
            }
        }else
        {
            timePass = 0;
        }
        /*if (jump.PlayerIsGrounded() && Physics.OverlapSphere(transform.position, 0.7f).Length < 2)*/
        
        bool overlapSomething = false;
        var test = Physics.OverlapSphere(transform.position, 0.7f);
        for (int i = 0; i < test.Length; i++)
        {
            if (!test[i].CompareTag("Player"))
            {
                overlapSomething = true;
                break;
            }
        }

        velocity = Vector3.MoveTowards(velocity, Vector3.zero, ((/*jump.PlayerIsGrounded() &&*/ overlapSomething)?reducteurTerrestre:reducteurAerien) * speedDeplacement);
        
        //var vitesse = velocity.magnitude;
        //
        // if (vitesse > speedDeplacementMax)
        // {
        //     velocity = Vector3.MoveTowards(velocity, Vector3.zero, ((/*jump.PlayerIsGrounded() &&*/ overlapSomething)?reducteurTerrestre:reducteurAerien) * speedDeplacement);
        // }
        // else if(vitesse >speedDeplacement)
        // {
        //     velocity = Vector3.MoveTowards(velocity, Vector3.zero, ((/*jump.PlayerIsGrounded() &&*/ overlapSomething)?reducteurTerrestre:reducteurAerien) * (speedDeplacement/2));
        // }
        // else if (vitesse < 10)
        // {
        //     velocity = Vector3.MoveTowards(velocity, Vector3.zero, ((/*jump.PlayerIsGrounded() &&*/ overlapSomething)?reducteurTerrestre:reducteurAerien) * (speedDeplacement*2));
        // }
        // else
        // {
        //     velocity = Vector3.MoveTowards(velocity, Vector3.zero, ((/*jump.PlayerIsGrounded() &&*/ overlapSomething)?reducteurTerrestre:reducteurAerien) * (speedDeplacement/4));
        // }
        
        // velocity = (velocity - velocity.normalized *0.5f* speedDeplacement).normalized ;
        

        velocity.y = y;
        rb.velocity = velocity;
    }


    public void DisableScript()
    {
        this.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        this.enabled = false;
    }

    public void DisableMovement(float time)
    {
        canMove = Time.time + time;
    }

    public void ReductionSpeed(float divider)
    {
        // throw new NotImplementedException();
        reduc = divider;
    }

    public void StopReduc()
    {
        // throw new NotImplementedException();
        reduc = 1;
    }

}
