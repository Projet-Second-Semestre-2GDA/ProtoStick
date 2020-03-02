using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float speedRotation = 2;
    public float speedDeplacement = 5;
     

    private void Update()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate(0f, 0f, v * speedDeplacement * Time.deltaTime);
        transform.Rotate(0f, h * speedRotation * Time.deltaTime, 0f);
        

    }
    public void DisableScript()
    {
        this.GetComponentInChildren<Camera>().gameObject.SetActive(false);
        this.GetComponent<Movement>().enabled = false;
    }



}
