using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

public class Movement : MonoBehaviour
{
    private PlayerNumber playerNumber;
    Rigidbody rb;
    Jump jump;

    [Title("Variable de vitesse")] [SerializeField]
    private AnimationCurve speedBehavior;
    public float speedDeplacement = 30;

    [Title("Variable liée au mécanique de vitesse")] 
    [SerializeField, Range(0, 1)] private float reducteurTerrestre;
    [SerializeField, Range(0, 1)] private float  reducteurAerien; 
    [SerializeField, Range(0, 5)] private float timeToSpeedMax;

    [SerializeField, MinMaxSlider(0f, 1f, true)]
    private Vector2 diviseurAcceleration;

    [Title("Variable liées aux bumper")]
    [SerializeField] private float upgradeMax = 10; 
    // [SerializeField] private float durationUpgradeDefault = 2f;
    // [Title("Variable liée à l'influence sur la vitesse")] 
    // [SerializeField] private float speedReductor = 2;

    private float reduc = 1;
    private float accelerator = 0;
    private float timePass = 0;
    
    //DowngradeSpeed
    private float speedDown = 15;
    private float restart = 0;
    
    //UpgrdeSpeed
    private bool isUpgrade = false;
    private float timeStopUpgrade = 0;
    private float upgradeMultiplicator = 1;
    private float globalUpgradeMultiplicator = 1;
    private float designDivider = -1;
    
    private float canMove = 0;
    
    //Check if ground
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
        playerNumber = GetComponent<PlayerNumber>();
    }

    private void FixedUpdate()
    {
        float oldH = Input.GetAxisRaw("MovementHorizontal" + playerNumber.playerNumber);
        float oldV = Input.GetAxisRaw("MovementVertical" + playerNumber.playerNumber);

        var h = speedBehavior.Evaluate(Mathf.Abs(oldH)) * Mathf.Sign(oldH);
        var v = speedBehavior.Evaluate(Mathf.Abs(oldV)) * Mathf.Sign(oldV);
        
        if (Time.time > canMove)
        {
            reduc = -1;
            Moove(v, h,Time.fixedDeltaTime,oldV,oldH);
        }
        
        if (Time.time < restart)
        {
            rb.velocity = rb.velocity.normalized * speedDown;
        }
    }

    private void Update()
    {

        if (isUpgrade && jump.isGrounded && Time.time >= timeStopUpgrade)
        {
            ResetUpgrade();
        }
    }

    public void ResetUpgrade()
    {
        Debug.Log("J'ai reset ton multiplicateur connard !");
        globalUpgradeMultiplicator = 1;
        isUpgrade = false;
    }

    private void Moove(float v, float h, float deltaTime, float oldV,float oldH)
    {
        var velocity = rb.velocity;
        float y = velocity.y;
        
        timePass += deltaTime;
        accelerator = Mathf.Lerp(diviseurAcceleration.x, diviseurAcceleration.y, Mathf.Clamp(timePass / timeToSpeedMax, 0, 1));
        
        velocity.y = 0;
        
        if (Mathf.Abs(oldV) > 0.01f || Mathf.Abs(oldH) > 0.01f)
        {
            var trans = transform;
            
            var realSpeed = speedDeplacement * globalUpgradeMultiplicator;
            
            Debug.Log("reduc = " + reduc);
            Debug.Log("SpeedDeplacement = " + speedDeplacement);
            Debug.Log("realSpeed = " + realSpeed);
            var temp = trans.forward * (v * (realSpeed) * accelerator);
            temp += trans.right * (h * (realSpeed) * accelerator);
            temp = (temp.magnitude > (realSpeed) * accelerator)
                ? temp.normalized * realSpeed
                : temp;
            
            
            velocity += temp;
            if ((velocity.magnitude > realSpeed))
            {
                velocity -= velocity.normalized * ((velocity.magnitude - (realSpeed) > realSpeed)
                    ? realSpeed
                    : realSpeed - (velocity.magnitude - realSpeed));
            }
        }else
        {
            timePass = 0;
        }
        /*if (jump.IsPlayerOnJump() && Physics.OverlapSphere(transform.position, 0.7f).Length < 2)*/
        
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

        velocity = Vector3.MoveTowards(velocity, Vector3.zero, ((overlapSomething)?reducteurTerrestre:reducteurAerien) * speedDeplacement);

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

    public void ReductionSpeed(float vitesseImposee ,float duration)
    {
        rb.velocity = rb.velocity.normalized * vitesseImposee;
        restart = Time.time + duration;
        speedDown = vitesseImposee;
    }

    public void StopReduc()
    {
        reduc = 1;
    }

    public void UpgradeSpeed(float multiplicator)
    {
        //Set Variable
        isUpgrade = true;
        timeStopUpgrade = Time.time + 0.2f;
        globalUpgradeMultiplicator += multiplicator;
        globalUpgradeMultiplicator = Mathf.Clamp(globalUpgradeMultiplicator, 1, upgradeMax);
        // if (globalUpgradeMultiplicator > upgradeMax)
        // {
        //     if (globalUpgradeMultiplicator - multiplicator < upgradeMax)
        //     {
        //         upgradeMultiplicator = upgradeMax / globalUpgradeMultiplicator;
        //         globalUpgradeMultiplicator /= multiplicator;
        //         globalUpgradeMultiplicator *= upgradeMultiplicator;
        //     }
        //     else
        //     {
        //         upgradeMultiplicator = 1;
        //         globalUpgradeMultiplicator /= multiplicator;
        //     }
        //
        // }
        
        designDivider = -1;
    }

}
