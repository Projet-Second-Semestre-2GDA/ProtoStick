using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperDestroyBehavior : MonoBehaviour
{
    private bool hasToDestroyLater = false;
    private float timer;
    
    public void DestroyBumper(float time = 0)
    {
        hasToDestroyLater = time > 0;
        if (!hasToDestroyLater)
        {
            DestroySelf();
        }
        else
        {
            timer = Time.time + time;
        }
    }

    private void Update()
    {
        if (hasToDestroyLater && Time.time > timer)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        Destroy(gameObject);
    }
}