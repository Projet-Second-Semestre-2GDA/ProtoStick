﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class RocketBehavior : MonoBehaviour
{
    [Title("PrefabsExplosion")] 
    [SerializeField] private GameObject explosionVisual;

    [SerializeField, Range(0.1f, 1f)] private float explosionVisualLifeTime;
    
    [Title("Rocket Parameters")]
    [SerializeField] private float explosionRadius = 20f;
    [SerializeField] private float defaultTimeBeforeDestroy = 1.5f;

    [Title("Explosion Parameters")] //C'est le "10" et le "200" les variable a modifié si vous trouver ne pas avoir assez de liberté sur mes valeurs
    [SerializeField, MinMaxSlider(10, 200, true)] private Vector2 minMaxExplosionForce = new Vector2(10,50);

    
    
    // For the code
    private float timer;
    
    
    private void Awake()
    {
        if (explosionVisual == null)
        {
            throw new ArgumentNullException("explosionVisual");
        }

        timer = Time.time + defaultTimeBeforeDestroy;
    }


    private void OnCollisionEnter(Collision other)
    {
        // Debug.Log(name+ " a touché " + other.collider.name);
        ContactPoint firstPoint = other.GetContact(0);
        Vector3 explosionPoint = firstPoint.point;
        Debug.DrawRay(explosionPoint,Vector3.left,Color.green);
        ActiveVisual(explosionPoint);
        Explode(explosionPoint);
        DestroySelf();
        
        
    }

    private void Explode(Vector3 explosionPoint)
    {
        Collider[] collidersInExplosion = Physics.OverlapSphere(explosionPoint, explosionRadius);

        foreach (var col in collidersInExplosion)
        {
            if (col.CompareTag("Bumper"))
            {
                var test = col.gameObject.GetComponent<BumperDestroyBehavior>();
                if (test != null) test.DestroyBumper();
            }
            else if (col.CompareTag("Player"))
            {
                Vector3 direction = col.transform.position - explosionPoint;
                float force = Mathf.Lerp(minMaxExplosionForce.x, minMaxExplosionForce.y, direction.magnitude / explosionRadius);
                col.attachedRigidbody.AddForce(direction.normalized * force,ForceMode.VelocityChange);
            }
        }
        
    }

    private void Update()
    {
        if (Time.time >= timer)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
        
    }

    
    private void ActiveVisual(Vector3 explosionPoint)
    {
        var explode = Instantiate(explosionVisual, explosionPoint, Quaternion.identity);
        explode.transform.localScale = Vector3.one * (explosionRadius*2);
        Destroy(explode,explosionVisualLifeTime);
    }
}
