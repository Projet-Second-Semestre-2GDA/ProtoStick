using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDoor : Interactible
{
    public override void Activate()
    {
        base.Activate();
        gameObject.SetActive(false);
    }
}
