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
                if (!hit.collider.CompareTag("Player"))
                {
                    var direction = hit.point - trans.position;
                    var point = hit.point - (direction.normalized * 0.5f);
                    Instantiate(bumper, point, Quaternion.identity);
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
