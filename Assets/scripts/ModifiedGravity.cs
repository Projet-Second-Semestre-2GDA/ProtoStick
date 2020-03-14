using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
public class ModifiedGravity : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField,PropertyRange(0,1)] private float addedGravity = 1f;

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
            rigidbody.velocity += new Vector3(0, -addedGravity, 0);
        }
        previousPosition = thisPosition;
    }
}
