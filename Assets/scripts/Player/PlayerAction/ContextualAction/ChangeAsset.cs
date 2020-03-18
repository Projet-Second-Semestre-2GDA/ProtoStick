using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAsset : MonoBehaviour
{
    private bool firstIsActive = true;
    [SerializeField] private GameObject[] firstObject;
    [SerializeField] private GameObject[] secondObject;

    private void Start()
    {
        firstIsActive = true;
        foreach (var objects in firstObject)
        {
            objects.SetActive(true);
        }
        foreach (var objects in secondObject)
        {
            objects.SetActive(false);
        }
    }

    public void ChangeAssetSelected()
    {
        firstIsActive = !firstIsActive;
        foreach (var objects in firstObject)
        {
            objects.SetActive(firstIsActive);
        }
        foreach (var objects in secondObject)
        {
            objects.SetActive(!firstIsActive);
        }
    }
}
