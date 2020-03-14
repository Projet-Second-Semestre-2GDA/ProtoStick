using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterControl : MonoBehaviour
{
    public float Speed;

    Rigidbody rigidbody;
    Jump jump;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        jump = GetComponent<Jump>();
    }

    void LateUpdate ()
    {
        PlayerMovement();
    }

    void PlayerMovement()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        var velocity = rigidbody.velocity;

        var velocity2 = new Vector2(hor, ver) * Speed;
        
        velocity2 = velocity2.magnitude > Speed ? velocity2.normalized * Speed : velocity2;
        
        if (jump.PlayerIsGrounded())
        {

            velocity.x = velocity2.x;
            velocity.z = velocity2.y;
        }
        else
        {
            velocity.x += velocity2.x;
            velocity.z += velocity2.y;
        }




        transform.Translate(velocity, Space.Self);

        rigidbody.velocity = velocity;
    }
}