using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomRecoltable : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Renderer>().material.SetVector("RandomNoisePBR", new Vector4(Random.Range(-10000, 10000), Random.Range(-10000, 10000), 0, 0));
        GetComponent<Renderer>().material.SetVector("RandomSeedPBR", new Vector4(Random.Range(-10000, 10000), Random.Range(-10000, 10000), 0, 0));
    }
}