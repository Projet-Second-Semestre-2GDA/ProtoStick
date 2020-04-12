using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ThrowBumper : MonoBehaviour
{
    [SerializeField] private GameObject bumper;
    [SerializeField, Range(0, 5)] private float timerBeforeNewShoot = 2;
    [SerializeField] Material[] materiauxBumper = new Material[2];
    private float nextTime;
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
        nextTime = Time.time;
    }

    private void Update()
    {
        // Debug.Log(inputName + " = " +Input.GetAxis(inputName));
        if (Input.GetAxis(inputName) >0.1f && !alreadyDid && Time.time >= nextTime)
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
                    foreach (var render in bumperTemp.GetComponentsInChildren<Renderer>())
                    {
                        render.material = materiauxBumper[playerNumber.playerIndex];
                    }
                    bumperTemp.GetComponent<RotateBumper>().SetPoints(points);
                }
                else if(hit.collider.CompareTag("Bumper"))
                {
                    Destroy(hit.collider.gameObject);
                }

                nextTime = Time.time + timerBeforeNewShoot;
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
