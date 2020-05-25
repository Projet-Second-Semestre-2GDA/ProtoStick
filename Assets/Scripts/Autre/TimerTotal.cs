using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TimerTotal : MonoBehaviour
{

    [Title("Timer du départ")]
    [SerializeField] private float beginningTimer = 6;

    public bool canPlayRocketAnnounce = false;

    private float timerTotal = 0;

    private void Start()
    {
        timerTotal = 0;
        
    }


    private void Update()
    {
        if (Input.GetAxis("Pause") == 0) timerTotal += Time.deltaTime;

        //Debug.Log("le timer total est de : " + timerTotal);

        if (timerTotal >= beginningTimer)
        {
            canPlayRocketAnnounce = true;
            // Debug.Log("TU PEUX JOUER LA ROCKET");
        }

         
    }

}
