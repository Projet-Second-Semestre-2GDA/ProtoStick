﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchButton : Interactible
{
    private DoubleDoor doubleDoor;

    public override void Activate()
    {
        base.Activate();
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
