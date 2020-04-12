using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Movement : MonoBehaviour
{
    private PlayerNumber playerNumber;
    Rigidbody rb;
    Jump jump;
    public float speedRotation = 100;
    public float speedDeplacement = 30;
    public float speedDeplacementMax = 50;

    [SerializeField, PropertyRange(0, 1)] private float timeForChange = 1f;
    [SerializeField, Range(0, 1)] private float reducteurTerrestre, reducteurAerien;

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
            Moove(v, h);
        }
    }

    private void Moove(float v, float h)
    {
        var velocity = rb.velocity;
        float y = velocity.y;
        float deltaTime = Time.fixedDeltaTime;
        
        
        // var velocity = velocity;
        velocity.y = 0;
        // if (velocity.magnitude > speedDeplacement)
        // {
        //     velocity -= velocity.normalized * speedDeplacement;
        // }
        
        if (Mathf.Abs(v) > 0.01f || Mathf.Abs(h) > 0.01f)
        {
            var trans = transform;
            var temp = trans.forward * (v * speedDeplacement);
            temp += trans.right * (h * speedDeplacement);
            temp = (temp.magnitude > speedDeplacement) ? temp.normalized * speedDeplacement : temp;
            
            
            velocity += temp;
            if ((velocity.magnitude > speedDeplacement))
            {
                velocity -= velocity.normalized * ((velocity.magnitude - speedDeplacement > speedDeplacement)
                    ? speedDeplacement
                    : speedDeplacement - (velocity.magnitude - speedDeplacement));
                
            }
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

}
