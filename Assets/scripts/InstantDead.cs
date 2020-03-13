using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDead : MonoBehaviour
{

    public GameObject spawnPoint;

    private void Update()
    {
        

        if (Input.GetKey(KeyCode.J))
        {
            List<GameObject> playerList = GameObject.FindGameObjectWithTag("Gestionnaire").GetComponent<SpawnPlayer>().playerList;

            playerList[playerList.Count - 1].transform.position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, spawnPoint.transform.position.z);
        }
    }


}
