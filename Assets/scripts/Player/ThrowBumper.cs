using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ThrowBumper : MonoBehaviour
{
    [SerializeField] private GameObject bumper;
    private PlayerNumber playerNumber;
    private Camera cam;
    private string inputName;
    private bool alreadyDid = false;
    private Ray ray;
    private void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        inputName = "Shoot" + playerNumber.playerNumber;
        cam = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        // Debug.Log(inputName + " = " +Input.GetAxis(inputName));
        if (Input.GetAxis(inputName) >0.1f && !alreadyDid)
        {
            
            alreadyDid = true;
            var trans = cam.transform;
            var pos = trans.position;
            ray= new Ray(pos, trans.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit))
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Bumper") )
                {
                    RaycastHit[] hits = new RaycastHit[3];
                    Vector3[] points = new Vector3[3];
                    var frwd = trans.forward;
                    Ray[] rays = new Ray[3]
                    {
                        new Ray(pos + (trans.up * 0.1f), frwd),
                        new Ray(pos + ((-trans.up + trans.right).normalized * 0.1f), frwd),
                        new Ray(pos + ((-trans.up - trans.right).normalized * 0.1f), frwd)
                    };
                    
                    for (int i = 0; i < points.Length; i++)
                    {
                        Physics.Raycast(rays[i], out hits[i]);
                        points[i] = hits[i].point;
                        Debug.Log(points[i]);
                    }

                    var bumperPosition = hit.point;
                    var bumperTemp = Instantiate(bumper, bumperPosition, Quaternion.identity);
                    bumperTemp.GetComponent<RotateBumper>().SetPoints(points);
                }
            }
        }

        if (alreadyDid && Input.GetAxis(inputName) <= 0.05)
        {
            alreadyDid = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay(ray);
        
    }
}
