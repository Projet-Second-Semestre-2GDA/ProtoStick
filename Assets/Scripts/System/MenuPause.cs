using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject gameObjectToSelect;
    private List<Vector3> velocity;
    private List<Vector3> angularVelocity;


    private bool isPause = false;
    void Start()
    {
        
        velocity = new List<Vector3>();
        angularVelocity = new List<Vector3>();
        UI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPause)
        {
            isPause = true;
            SetPause();
        }
    }

    public void SetPause()
    {
        isPause = false;
        DisablePlayer();
        UI.SetActive(true);
        eventSystem.SetSelectedGameObject(gameObjectToSelect);
    }

    public void Resume()
    {
        EnablePlayer();
        UI.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    private void DisablePlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            velocity.Add(Vector3.zero);
            angularVelocity.Add(Vector3.zero);
            if (players[i].activeInHierarchy)
            {
                var rb = players[i].GetComponent<Rigidbody>();
                velocity[i] = (rb.velocity);
                rb.velocity = Vector3.zero;
                angularVelocity[i] = (rb.angularVelocity);
                rb.angularVelocity = Vector3.zero;
                rb.useGravity = false;
                players[i].GetComponent<Movement>().DisableMovement(Mathf.Infinity);
                players[i].GetComponent<TirsClicGauche>().SetThrowBumper(false);
                players[i].GetComponent<Jump>().SetThrowJump(false);
                players[i].GetComponent<ModifiedGravity>().SetModifiedGravity(false);
                players[i].GetComponent<TimerShower>().StopChrono();
                players[i].GetComponentInChildren<ThirdPersonCameraControl>().SetCamera(false);
            }

        }
    }
    private void EnablePlayer()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].activeInHierarchy)
            {
                var rb = players[i].GetComponent<Rigidbody>();
                Debug.Log("rb : " + rb);
                Debug.Log("i : " + i);
                rb.velocity = velocity[i];
                rb.angularVelocity = angularVelocity[i];
                rb.useGravity = true;
                players[i].GetComponent<Movement>().DisableMovement(-1);
                players[i].GetComponent<TirsClicGauche>().SetThrowBumper(true);
                players[i].GetComponent<Jump>().SetThrowJump(true);
                players[i].GetComponent<ModifiedGravity>().SetModifiedGravity(true);
                players[i].GetComponent<TimerShower>().ResumeChrono();
                players[i].GetComponentInChildren<ThirdPersonCameraControl>().SetCamera(true);
            }
        }
        velocity.Clear();
        angularVelocity.Clear();
    }
}
