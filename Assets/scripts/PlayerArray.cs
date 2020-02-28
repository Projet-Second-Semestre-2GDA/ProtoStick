using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArray : MonoBehaviour
{

    [Range(1, 20)]
    public int nombreTableau;

    public GameObject[] playerList;

    private void Start()
    {
        playerList = new GameObject[nombreTableau];
    }

    private void Update()
    {
        


    }



}
