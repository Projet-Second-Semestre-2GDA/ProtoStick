using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;

public class ControlObject : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform camera;
    [SerializeField]private float treesholdAutorized = 0.5f;
    [SerializeField] private float distanceMax = 2.5f;
    private Ray rayCamera;
    private GameObject ball;
    void Start()
    {
        camera = GetComponentInChildren<ThirdPersonCameraControl>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log(ball);
            if (!ball)
            {
                rayCamera = new Ray(camera.position,camera.forward);
                RaycastHit hit;
                if (Physics.SphereCast(rayCamera,treesholdAutorized,out hit,distanceMax))
                {
                    if (hit.collider.CompareTag("Ball"))
                    {
                        // Debug.Log(hit.collider.name);

                        ActionOneOnBall(hit.collider.gameObject);
                    }
                }
            }
            else
            {
                ball.GetComponent<PickableObject>().ThrowObject();
            }
            
        }

        if (Input.GetButtonDown("Fire2") && ball)
        {
            ActionTwoBall(ball);
        }
    }

    private void ActionOneOnBall(GameObject aBall)
    {
        
        aBall.GetComponent<PickableObject>().TakeObject(transform,camera);
    }

    private void ActionTwoBall(GameObject aBall)
    {
        aBall.GetComponent<PickableObject>().LeaveObject();
    }

    public void LeaveBall()
    {
        ball = null;
    }

    public void SetBall(GameObject anObject)
    {
        ball = anObject;
    }

    private void OnDrawGizmos()
    {
        rayCamera = new Ray(camera.position,camera.forward);
        RaycastHit hit;
        

        if (Physics.SphereCast(rayCamera,treesholdAutorized,out hit,distanceMax))
        {
            var position = camera.position;
            Gizmos.DrawLine(position,hit.point);

        }
        else
        {
            var position = camera.position;
            Gizmos.DrawLine(position, position + camera.forward * distanceMax );
        }
    }
}
