using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpTrick : MonoBehaviour
{

    public float bumpForce;
    public float explosionRadius;
    //public GameObject player;
    //public GameObject toit;


    private void Update()
    {
        
    }



    private void OnCollisionEnter (Collision collision)
    {

        /*player.GetComponent<Rigidbody>().AddExplosionForce(bumpForce, transform.position, explosionRadius);
        Debug.Log("bump");*/

        Debug.Log(collision.collider.name + "bump");

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.collider.GetComponentInParent<Rigidbody>().AddExplosionForce(bumpForce, transform.position, explosionRadius, 1);
            Debug.Log("bump");
            
        }
    }

}
