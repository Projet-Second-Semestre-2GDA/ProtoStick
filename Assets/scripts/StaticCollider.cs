using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollider : MonoBehaviour
{

    private BoxCollider boxCollider;



    private void Start()
    {
        boxCollider = boxCollider.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (GetComponent<Movement>().enabled == false)
        {
            boxCollider.size = new Vector3(1.5f, 1, 1.5f);
        }
    }


}
