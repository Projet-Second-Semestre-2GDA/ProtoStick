using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPoint : MonoBehaviour
{

    private GameObject spawnPointPlayer;

    private void Start()
    {
        spawnPointPlayer = GameObject.FindGameObjectWithTag("SpawnPoint");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {

            spawnPointPlayer.transform.position = transform.position;
            spawnPointPlayer.transform.rotation = transform.rotation;
        }
    }

}
