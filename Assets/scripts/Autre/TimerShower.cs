using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class TimerShower : MonoBehaviour
{
    [SerializeField] private Text timeShower;

    private float timer;
    private bool canUpdate = false;

    private void Start()
    {
        //StartChrono();
    }

    private void Update()
    {
        if (canUpdate)
        {
            UpdateChrono(Time.deltaTime);
        }

        int minute = Mathf.FloorToInt(timer/60);
        timeShower.text = "     Timer : " + minute + " min "+ (timer - minute*60);
    }

    public void StartChrono()
    {
        canUpdate = true;
        timer = 0;
    }

    public void ResumeChrono(float timeResume = -1)
    {
        canUpdate = true;
        timer = (timeResume < 0) ? timer : timeResume;
    }

    public void StopChrono()
    {
        canUpdate = false;
    }

    private void UpdateChrono(float timeAdd)
    {
        timer += timeAdd;
    }

    public float GetTimer()
    {
        return timer;
    }
}
