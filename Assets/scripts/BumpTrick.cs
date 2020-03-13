using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpTrick : MonoBehaviour
{

    public float bumpForce;
    public float explosionRadius; 
    public GameObject player;


    private void Update()
    {
        
    }



    public void OnColliderEnter (Collision collision)
    {

        /*player.GetComponent<Rigidbody>().AddExplosionForce(bumpForce, transform.position, explosionRadius);
        Debug.Log("bump");*/

        Debug.Log(collision.collider.name + "bump");

        if (collision.gameObject == player)
        {
            player.GetComponent<Rigidbody>().AddExplosionForce(bumpForce, transform.position, explosionRadius, 1);
            Debug.Log("bump");
            
        }
    }

}
