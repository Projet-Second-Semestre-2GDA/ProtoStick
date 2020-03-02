using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSpawnPoint : MonoBehaviour
{

    public GameObject spawnPointPlayer;


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            spawnPointPlayer.transform.position = transform.position;
            spawnPointPlayer.transform.rotation = transform.rotation;
        }
    }

}
