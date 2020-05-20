using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testeur : MonoBehaviour
{
    private void OnEnable()
    {
        var trans = transform;
        var pos = trans.position;
        pos.y = 1080;
        trans.position = pos;
    }
}
