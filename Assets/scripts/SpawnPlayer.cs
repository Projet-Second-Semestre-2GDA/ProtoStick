using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : PlayerArray
{
    
    public GameObject player;
    public GameObject spawnPointPlayer;

    //timer
    public float timer;
    private float timerSave;

    private Rigidbody rb;

    private bool rbDesactivation = false;
    private bool activationScript = true;

    private void Start()
    {
        playerList = new GameObject[nombreTableau];

        timerSave = timer;

        rb = GetComponent<Rigidbody>();

        GetComponent<Movement>().enabled = true;
    }

    private void Update()
    {
        timer -= Time.deltaTime;




        if (Input.GetKeyDown(KeyCode.Tab))
        {

            for (int i = 0; i < nombreTableau; i++)
            {
                SpawnPlayerMethod(i);

            }



            /* - freeze le rigidbody en position et rotation du controler actuel
             * - faire spawn un autre avatar 
             * - transposer le contrôle du joueur sur le nouvel avatar
             */

        }

        if (rbDesactivation)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        

    }

    private void SpawnPlayerMethod(int i)
    {
        if (timer < 0)
        {
            rbDesactivation = true;

            GetComponent<Movement>().enabled = false;

            activationScript = false;

            playerList[i] = Instantiate(player, spawnPointPlayer.transform.position, Quaternion.identity);

            timer = timerSave;
        }
    }
}
