using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    private float timer = 0;
    private bool isPause = false;
    private bool isBegin = false;

    private void Awake()
    {
        isPause = false;
        isBegin = false;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause && isBegin)
        {
            UpdateTimer(Time.deltaTime);
        }
    }

    public void Pause()
    {
        isPause = true;
    }

    public void Resume()
    {
        isPause = false;
    }

    public void Start()
    {
        isBegin = true;
    }

    public void StopAndRest()
    {
        isBegin = false;
        timer = 0;
    }

    private void UpdateTimer(float deltaTime)
    {
        timer += deltaTime;
    }

    public float GetTime()
    {
        return timer;
    }
    
}    

