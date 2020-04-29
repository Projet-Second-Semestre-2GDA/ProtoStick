using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : MonoBehaviour
{
    private DoubleDoor doubleDoor;

    public void Touch()
    {
        if (doubleDoor == null)
            throw new NullReferenceException(
                "Le SetGestionnaire a probablement pas fonctionné car les portes ne marchent pas");
        doubleDoor.CallTouch();
    }

    public void SetGestionnaire(DoubleDoor gestionnaire)
    {
        doubleDoor = gestionnaire;
    }
}
