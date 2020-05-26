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
    
    private Animator playerAnimation;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        timer = 0;
        inputName += GetComponent<PlayerNumber>().playerNumber;
        playerAnimation = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        // Debug.Log(inputName + " = "+ Input.GetAxis(inputName));
        var shoot = false;
        if (Input.GetAxis(inputName) >0.1f && !alreadyDid)
        {
            alreadyDid = true;
            if (Time.time >= timer)
            {
                Shoot();
                shoot = true;
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
        playerAnimation.SetTrigger("Shoot");
        var playerNumber = GetComponent<PlayerNumber>().playerNumber;
        var shootedRocket = Instantiate(rocket, spawnPoint.position, spawnPoint.rotation);
        shootedRocket.layer = LayerMask.NameToLayer("RocketPlayer" + playerNumber);
        shootedRocket.GetComponent<RocketBehavior>().playerWhoThrowTheRocket = playerNumber;
        //Feedback FMOD
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Personnage longiforme/tir", transform.position);

        
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
        Debug.DrawRay(shootPoint,direction,Color.red,2f);
        // shootedRocket.transform.rotation = Quaternion.Euler(direction);
        var rbRocket = shootedRocket.GetComponent<Rigidbody>();
        rbRocket.AddForce(direction.normalized*rocketSpeed,ForceMode.VelocityChange);
    }

    private bool TryGetShootingPoint(out Vector3 shootPoint)
    {
        var camTrans = cam.transform;
        bool found = false;
        RaycastHit hit;
        int layerMask =~ LayerMask.GetMask("Player" + GetComponent<PlayerNumber>().playerNumber);
        Ray ray = new Ray(camTrans.position,camTrans.forward);
        if (Physics.Raycast(ray,out hit,Mathf.Infinity,layerMask))
        {
            shootPoint = hit.point;
            Debug.DrawLine(spawnPoint.position,shootPoint,Color.blue,5f);
            found = true;
        }
        else
        {
            shootPoint = Vector3.negativeInfinity;
        }
        
        return found;
    }
}
