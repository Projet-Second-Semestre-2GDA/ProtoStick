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
            spawnPointPlayer.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        }
    }

}
