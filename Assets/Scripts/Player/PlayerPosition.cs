using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    [SerializeField] private Text textPosition;
    private int lastPositionKnow = -1;
    private bool defaultActivities = false;

    private void Start()
    {
        SetScriptActive(defaultActivities);
    }

    public void SetPlayerClassement(int position)
    {
        lastPositionKnow = position;
        textPosition.text = lastPositionKnow.ToString();
    }

    public void SetScriptActive(bool activitee)
    {
        defaultActivities = activitee;
        textPosition.enabled = activitee;
        if (activitee)
        {
            textPosition.text = lastPositionKnow.ToString();
        }
    }
}
