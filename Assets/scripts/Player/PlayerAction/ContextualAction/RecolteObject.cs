using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecolteObject : MonoBehaviour
{
    [SerializeField] private Text RecoltableShower;
    private int objectRecolted = 0;
    private string tagRecolt = "Recoltable";

    private void Awake()
    {
        objectRecolted = 0;
    }

    private void Update()
    {
        RecoltableShower.text ="     " + objectRecolted + " " + ((objectRecolted < 2) ? "objet récolté" : "objets récoltés");
    }

    public void AddRecoltable(GameObject obj)
    {
        ++objectRecolted;
        Destroy(obj);
    }
}
