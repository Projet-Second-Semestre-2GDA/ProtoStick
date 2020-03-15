using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    [SerializeField] private GameObject prefabsPlayer;
    [HideInInspector]public List<GameObject> playerList;
    private Movement lastPlayerMovement;
    
    //private GameObject playerList[playerList.Count - 1];
    private Transform spawnPointPlayer;
    
    private Rigidbody rbPlayer;

    private GameObject lastPlayer;

    private PlayerEffect lastEffect; 
    //private bool activationScript = true;

    private void Start()
    {
        spawnPointPlayer = GameObject.FindGameObjectWithTag("SpawnPoint").transform;
        playerList = new List<GameObject>();
        SpawnPlayerMethod(false);

    }

    private void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnPlayerMethod();
        }
    }

    private void SpawnPlayerMethod(bool aPlayerIsInScene = true)
    {
        if (aPlayerIsInScene)
        {
            rbPlayer.useGravity = false;
            rbPlayer.constraints = RigidbodyConstraints.FreezeAll;
            lastPlayerMovement.DisableScript();
            lastEffect.enabled = true;

        }
        
        
        playerList.Add(Instantiate(prefabsPlayer, spawnPointPlayer.position, spawnPointPlayer.rotation));
        lastPlayer = playerList[playerList.Count - 1];
        lastPlayerMovement = lastPlayer.GetComponent<Movement>();
        rbPlayer = lastPlayerMovement.GetComponent<Rigidbody>();
        lastEffect = lastPlayer.GetComponent<PlayerEffect>();
        lastEffect.enabled = false;

    }
}
