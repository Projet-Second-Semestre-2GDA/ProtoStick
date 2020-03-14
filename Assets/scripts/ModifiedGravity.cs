using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ModifiedGravity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField,PropertyRange(0,1)] private float addedGravity = 1f;
    [SerializeField,PropertyRange(0.9f,1)] private float frictionMultiplicator = 1f;

    Vector3 previousPosition;
    Vector3 thisPosition;
    Rigidbody rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        thisPosition = transform.position;

        if (thisPosition.y < previousPosition.y)
        {
            AccelerationVerticale();
            FrictionDeLAir();
        }
        previousPosition = thisPosition;
    }

    private void AccelerationVerticale()
    {
        
            rigidbody.velocity += new Vector3(0, -addedGravity, 0);
            var accelerationY = Mathf.Abs(rigidbody.velocity.y);

        
    }
    private void FrictionDeLAir()
    {
        var velocity = rigidbody.velocity;
        velocity.y = 0;
        rigidbody.velocity -= velocity * (1 - frictionMultiplicator);
    }
}
