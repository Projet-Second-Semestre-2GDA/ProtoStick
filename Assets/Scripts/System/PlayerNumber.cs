using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerNumber : MonoBehaviour
{
    [HideInInspector] public int playerNumber;
    [HideInInspector] public int playerIndex;
    
    private void Awake()
    {
        SetPlayerNumber();
    }
    

    private void SetPlayerNumber()
    {
        switch (name.Contains("One"))
        {
            case true:
                playerNumber = 1;
                break;
            case false:
                playerNumber = 2;
                break;
            default:
                break;
        }

        playerIndex = playerNumber - 1;
    }
}
