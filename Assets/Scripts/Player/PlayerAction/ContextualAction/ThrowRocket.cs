using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;

public class ThrowRocket : MonoBehaviour
{
    [Title("Prefabs")] 
    [SerializeField] private GameObject rocket;

    [Title("Designer Parameters")] 
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float rocketSpeed;
    [SerializeField] private float timeBeforeNextShot;
    
    //Other paramters Private for me

    private string inputName = "ShootRocket";
    private bool alreadyDid = false;
    private float timer;
    private Camera cam;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        timer = 0;
        inputName += GetComponent<PlayerNumber>().playerNumber;
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(inputName + " = "+ Input.GetAxis(inputName));
        
        if (Input.GetAxis(inputName) >0.1f && !alreadyDid)
        {
            alreadyDid = true;
            if (Time.time >= timer)
            {
                Shoot();
                timer = Time.time + timeBeforeNextShot;
            }
        }
        if (alreadyDid && Input.GetAxis(inputName) <= 0.05)
        {
            alreadyDid = false;
        }
    }

    private void Shoot()
    {
        var shootedRocket = Instantiate(rocket, spawnPoint.position, spawnPoint.rotation);

        //Feedback FMOD
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Personnage longiforme/tir", transform.position);

        shootedRocket.layer = LayerMask.NameToLayer("RocketPlayer" + GetComponent<PlayerNumber>().playerNumber);
        Vector3 shootPoint;
        Vector3 direction;
        if (TryGetShootingPoint(out shootPoint))
        {
            direction = shootPoint - spawnPoint.position;
        }
        else
        {
            var camTrans = cam.transform;
            shootPoint = camTrans.position + (camTrans.forward * 10000);
            direction = shootPoint - spawnPoint.position;
        }
        // shootedRocket.transform.rotation = Quaternion.Euler(direction);
        var rbRocket = shootedRocket.GetComponent<Rigidbody>();
        rbRocket.AddForce(direction.normalized*rocketSpeed,ForceMode.VelocityChange);
    }

    private bool TryGetShootingPoint(out Vector3 shootPoint)
    {
        var camTrans = cam.transform;
        bool found = false;
        RaycastHit hit;
        Ray ray = new Ray(camTrans.position,camTrans.forward);
        if (Physics.Raycast(ray,out hit))
        {
            shootPoint = hit.point;
            found = true;
        }
        else
        {
            shootPoint = Vector3.negativeInfinity;
        }
        
        return found;
    }
}
