using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public virtual void Activate()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Level Design/LD_Ouverture_Porte", transform.position);
        //Debug.Log(name + " has been touch");
    }
}
