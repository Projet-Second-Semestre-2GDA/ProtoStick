using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject UI;
    [SerializeField] private Beginning debut;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject gameObjectToSelect;
    [SerializeField] private CustomButton ResumeButton;
    [SerializeField] private String urlFeedback = "https://forms.gle/pSeSzSjDvmm3j1eX8";
    private List<Vector3> velocity;
    private List<Vector3> angularVelocity;

    private GlobalTimer timer;


    private bool isPause = false;
    void Start()
    {
        timer = GetComponent<GlobalTimer>();
        velocity = new List<Vector3>();
        angularVelocity = new List<Vector3>();
        UI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !isPause)
        {
            SetPause();
        }

        if (isPause && Input.GetButtonDown("Cancel"))
        {
            ResumeButton.Press();
        }
    }

    public void SetPause()
    {
        isPause = true;
        DisablePlayer();
        UI.SetActive(true);
        debut.Pause();
        eventSystem.SetSelectedGameObject(gameObjectToSelect);
        timer.Pause();
    }

    public void Resume()
    {
        EnablePlayer();
        debut.Resume();
        UI.SetActive(false);
        isPause = false;
        eventSystem.SetSelectedGameObject(null);
        timer.Resume();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void SendFeedback()
    {
        Application.OpenURL(urlFeedback);
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
                // players[i].GetComponent<TimerShower>().StopChrono();
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
                //Debug.Log("rb : " + rb);
                //Debug.Log("i : " + i);
                rb.velocity = velocity[i];
                rb.angularVelocity = angularVelocity[i];
                rb.useGravity = true;
                players[i].GetComponent<Movement>().DisableMovement(-1);
                players[i].GetComponent<TirsClicGauche>().SetThrowBumper(true);
                players[i].GetComponent<Jump>().SetThrowJump(true);
                players[i].GetComponent<ModifiedGravity>().SetModifiedGravity(true);
                // players[i].GetComponent<TimerShower>().ResumeChrono();
                players[i].GetComponentInChildren<ThirdPersonCameraControl>().SetCamera(true);
            }
        }
        velocity.Clear();
        angularVelocity.Clear();
    }
}


