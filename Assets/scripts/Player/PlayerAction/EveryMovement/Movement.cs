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
    [SerializeField] private float speedMaxBase = 50;
    [SerializeField] private float directionChangementSpeed = 2f;

    [Title("Variable liée au mécanique de vitesse")] 
    [SerializeField, Range(0, 1)] private float reducteurTerrestre;
    [SerializeField, Range(0, 1)] private float  reducteurAerien; 
    [SerializeField, Range(0, 5)] private float timeToSpeedMax;
    [SerializeField, Range(-1, 1)] private float sameDirection = 0.85f;


    [SerializeField, MinMaxSlider(0f, 1f, true)]
    private Vector2 diviseurAcceleration;

    [Title("Variable liées aux bumper")]
    [SerializeField] private float upgradeMax = 10; 
    
    // [TitleGroup("Animation")]
    private Animator playerAnimation;
    
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

    public bool hasTheMultiplicatorReset = false;
    
    //Check if ground
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
        playerNumber = GetComponent<PlayerNumber>();
        playerAnimation = GetComponentInChildren<Animator>();
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
            hasTheMultiplicatorReset = true;
        }
    }

    public void ResetUpgrade()
    {
        //Debug.Log("J'ai reset ton multiplicateur connard !");

        globalUpgradeMultiplicator = 1;
        isUpgrade = false;
        hasTheMultiplicatorReset = false;
    }

    private void Moove(float v, float h, float dt, float oldV,float oldH)
    {
        //Animation
        playerAnimation.SetFloat("Walk",Mathf.Abs(v));
        // playerAnimation.SetBool("WalkFront",(oldV<0));
        playerAnimation.SetFloat("WalkSide",h);
        // playerAnimation.SetBool("WalkRight",(oldH<0));
        //Fin de l'animation
        
        var velocity = rb.velocity;
        float y = velocity.y;
        
        timePass += dt;
        accelerator = Mathf.Lerp(diviseurAcceleration.x, diviseurAcceleration.y, 
            Mathf.Clamp(timePass / timeToSpeedMax, 0, 1));
        
        velocity.y = 0;
        
        if (Mathf.Abs(oldV) > 0.01f || Mathf.Abs(oldH) > 0.01f)
        {
           
            //Vérifié que l'on ne dépasse pas le maximum voulu
            //Bonus : On a déjà indiquer la direction

            var direction = v * transform.forward;
            direction += h * transform.right;

            if (direction.magnitude > 1)
            {
                direction = direction.normalized;
            }
            
            bool sameDirection = (Vector3.Dot(velocity.normalized, direction.normalized) >= this.sameDirection) 
                                 || velocity.magnitude <= 1;
            bool speedIsntMax = velocity.magnitude < speedMaxBase * globalUpgradeMultiplicator;

            
            if (sameDirection)
            {
                if (speedIsntMax)
                {
                    //On indique la forcé à appliquer au joueur
                    rb.AddForce(direction * (speedDeplacement), ForceMode.Acceleration);
                }
            }
            else 
            {
                var speed = velocity.magnitude;
                velocity = velocity.normalized;
                velocity += direction * (directionChangementSpeed * dt);
                velocity = velocity.normalized * speed;
                velocity.y = y;
                rb.velocity = velocity;
            }
        }else
        {
            timePass = 0;
            //Réducteur de vitesse en fonction de si l'on est en l'air ou au sol
            
            bool overlapSomething = false;
            var test = Physics.OverlapSphere(transform.position + (Vector3.down), 0.2f);
            for (int i = 0; i < test.Length; i++)
            {
                if (!test[i].CompareTag("Player"))
                {
                    overlapSomething = true;
                    break;
                }
            }

            Debug.Log("On utilise " + ((overlapSomething) ? "un reducteur terrestre" : "un reducteur aerien"));
            velocity = Vector3.MoveTowards(velocity, Vector3.zero,
                ((overlapSomething) ? reducteurTerrestre : reducteurAerien) * (velocity.magnitude));
            
            velocity.y = y;
            rb.velocity = velocity;
        }
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
