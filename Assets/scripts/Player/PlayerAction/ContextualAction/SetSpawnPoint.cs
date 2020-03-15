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
            Transform trans = transform;
            spawnPointPlayer.transform.position = trans.position;
            spawnPointPlayer.transform.rotation = trans.rotation;
        }
    }

}
