using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeUnPlayer : MonoBehaviour
{
    [SerializeField]private List<GameObject> gameObjectsADesactiver;
    [SerializeField] private GameObject playerOne;
    private Camera cameraAModifier;

    [SerializeField] private bool modeUnJoueur;

    private void Start()
    {
        cameraAModifier = playerOne.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (modeUnJoueur)
        {
            foreach (var obj in gameObjectsADesactiver)
            {
                obj.SetActive(false);
            }
            cameraAModifier.rect = new Rect(0,0,1,1);
            cameraAModifier.fieldOfView = 100;
        }
        else
        {
            foreach (var obj in gameObjectsADesactiver)
            {
                obj.SetActive(true);
            }
            cameraAModifier.rect = new Rect(0,0.5f,1,0.5f);
            cameraAModifier.fieldOfView = 60;

        }
    }
}
