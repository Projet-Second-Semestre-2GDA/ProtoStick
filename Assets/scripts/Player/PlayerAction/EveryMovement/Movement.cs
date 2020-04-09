using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Movement : MonoBehaviour
{
    private PlayerNumber playerNumber;
    Rigidbody rb;
    Jump jump;
    public float speedRotation = 2;
    public float speedDeplacement = 5;
    [SerializeField, PropertyRange(0, 1)] private float timeForChange = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
        playerNumber = GetComponent<PlayerNumber>();
    }

    private void FixedUpdate()
    {
        float h = Input.GetAxis("MovementHorizontal" + playerNumber.playerNumber);
        float v = Input.GetAxis("MovementVertical" + playerNumber.playerNumber);


        Moove(v, h);
    }

    private void Moove(float v, float h)
    {
        var velocity = rb.velocity;
        float deltaTime = Time.fixedDeltaTime;

        if (Mathf.Abs(v) > 0.01f || Mathf.Abs(h) > 0.01f)
        {
            var velocity2 = new Vector3();
            velocity2 += v * speedDeplacement * transform.forward;
            velocity2 += h * speedDeplacement * transform.right;
            velocity2 = velocity2.magnitude > speedDeplacement ? velocity2.normalized * speedDeplacement : velocity2;
            if (jump.PlayerIsGrounded())
            {
                velocity.x = velocity2.x;
                velocity.z = velocity2.z;
            }
            else
            {
                velocity2.x = velocity.x +
                              (velocity2.x * (deltaTime / ((timeForChange <= deltaTime) ? deltaTime : timeForChange)));
                velocity2.z = velocity.z +
                              (velocity2.z * (deltaTime / ((timeForChange <= deltaTime) ? deltaTime : timeForChange)));

                velocity2 = velocity2.normalized * speedDeplacement;


                velocity.x = velocity2.x;
                velocity.z = velocity2.z;
            }
        }
        else if (jump.PlayerIsGrounded() && Physics.OverlapSphere(transform.position, 0.7f).Length < 2)
        {
            velocity.x = 0;
            velocity.z = 0;
        }


        rb.velocity = velocity;
    }

    public void DisableScript()
    {
        this.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        this.enabled = false;
    }



}
