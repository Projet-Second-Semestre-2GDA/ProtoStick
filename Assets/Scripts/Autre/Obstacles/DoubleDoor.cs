using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class DoubleDoor : MonoBehaviour
{
    [Title("Door")]
    [SerializeField] private GameObject FirstDoor;
    [SerializeField] private GameObject SecondDoor;
    [SerializeField] private bool FirstDoorIsOpen = true;
    private GameObject[] doors = new GameObject[2];

    private void Awake()
    {
        SetupDoors();
        
    }

    private void Start()
    {
        foreach (var door in doors)
        {
            door.GetComponent<TouchButton>().SetGestionnaire(this);
        }
        FirstDoor.SetActive(FirstDoorIsOpen);
        SecondDoor.SetActive(!FirstDoorIsOpen);
    }

    public void CallTouch()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].SetActive(!doors[i].activeSelf);
        }
        
    }

    private void ActivateDoor(bool activator, GameObject door)
    {
        door.SetActive(activator);
    }
    
    private void SetupDoors()
    {
        doors = new[] {FirstDoor, SecondDoor};
    }
}
