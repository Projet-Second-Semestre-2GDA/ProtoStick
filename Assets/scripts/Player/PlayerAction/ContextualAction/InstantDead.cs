using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDead : MonoBehaviour
{

    private GameObject spawnPoint;

    private void Start()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
    }

    private void Update()
    {
        

        if (Input.GetKey(KeyCode.J))
        {
            List<GameObject> playerList = GameObject.FindGameObjectWithTag("Gestionnaire").GetComponent<SpawnPlayer>().playerList;

            playerList[playerList.Count - 1].transform.position = spawnPoint.transform.position;
            playerList[playerList.Count - 1].transform.rotation = spawnPoint.transform.rotation;

        }
    }


}
