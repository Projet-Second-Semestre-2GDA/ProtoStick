using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

public class TirsClicGauche : MonoBehaviour
{
    [Title("Bumper Component")]
    [SerializeField] private GameObject bumperPrefab;
    [SerializeField] Material[] materiauxBumper = new Material[2];
    [Title("BumperCharacteristique")]
    [SerializeField, Range(0, 5)] private float timerBeforeNewShoot = 2;
    // [SerializeField, Range(0, 5)] private float timerBeforeNewShootRemove = 2;
    [SerializeField] private float distanceMaxTir = 180f;

    [Title("Ouvrage de porte")] 
    [SerializeField, Range(0, 2)] private float durationBewteenOpeningDoors = 0.5f;

    private float nextTimeOpenDoor;
    
    private float nextTimeThrow;
    private float nextTimeRemove;

    private PlayerNumber playerNumber;
    private Camera cam;
    private string inputName;
    private bool alreadyDid = false;
    private Ray ray;

    private bool activitee = false;
    
    private void Start()
    {
        playerNumber = GetComponent<PlayerNumber>();
        inputName = "ShootBumper" + playerNumber.playerNumber;
        cam = GetComponentInChildren<Camera>();
        nextTimeThrow = Time.time;
        nextTimeRemove = Time.time;
        nextTimeOpenDoor = Time.time;
    }

    private void Update()
    {
        // c'est pas le plus visible du monde mais au moins je fais qu'un seul raycast ! (puis trois de plus pour placer le bumper mais chut ça)
        if (Input.GetAxis(inputName) >0.1f && !alreadyDid && activitee)
        {
            
            alreadyDid = true;
            var trans = cam.transform;
            var pos = trans.position;
            ray= new Ray(pos, trans.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit,distanceMaxTir))
            {
                if (!hit.collider.CompareTag("Player") && !hit.collider.CompareTag("Bumper") && !hit.collider.CompareTag("NoBumper") && !hit.collider.CompareTag("Button"))
                {
                    if (Time.time >= nextTimeThrow)
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
                        var bumperTemp = Instantiate(bumperPrefab, bumperPosition, Quaternion.identity);
                        foreach (var render in bumperTemp.GetComponentsInChildren<Renderer>())
                        {
                            render.material = materiauxBumper[playerNumber.playerIndex];
                        }
                        bumperTemp.GetComponent<RotateBumper>().SetPoints(points);
                        
                        nextTimeThrow = Time.time + timerBeforeNewShoot;
                    }
                    
                }
                else if(hit.collider.CompareTag("Button"))
                {
                    if (Time.time > nextTimeOpenDoor)
                    {
                        hit.collider.GetComponent<TouchButton>().Touch();
                        nextTimeOpenDoor = Time.time + durationBewteenOpeningDoors;
                    }
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

    public void SetThrowBumper(bool isActive)
    {
        activitee = isActive;
    }
}
