using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialsSetter : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsWithEmissiveMaterial;

    public void ChangeEmissive(Material mat)
    {
        foreach (var objectWithEmissiveMaterial in objectsWithEmissiveMaterial)
        {
            objectWithEmissiveMaterial.GetComponent<Renderer>().material = mat;
        }
    }
    
}
