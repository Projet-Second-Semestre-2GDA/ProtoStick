using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    private PlayerEffect lastEffect;

    private GameObject lastPlayer;
    private Movement lastPlayerMovement;
    [HideInInspector] public List<GameObject> playerList;

    [SerializeField] private GameObject prefabsPlayer;

    private Rigidbody rbPlayer;

    //private GameObject playerList[playerList.Count - 1];
    private Transform spawnPointPlayer;
    //private bool activationScript = true;

    private int added = 0;

    private void Awake()
    {
        added = 0;
    }

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
            FMODUnity.RuntimeManager.PlayOneShot("event:/DA placeholder/personnage/instanciation_bumper");
        }
    }

    private void SpawnPlayerMethod(bool aPlayerIsInScene = true)
    {
        if (aPlayerIsInScene)
        {
            rbPlayer.useGravity = false;
            rbPlayer.constraints = RigidbodyConstraints.FreezeAll;
            lastPlayerMovement.DisableScript();
            // lastEffect.canWork = true;
            var attach = lastPlayer.GetComponent<EffectAttaching>();
            attach.Attach();
            lastPlayer.GetComponent<ChangeAsset>().ChangeAssetSelected();
        }


        playerList.Add(Instantiate(prefabsPlayer, spawnPointPlayer.position, spawnPointPlayer.rotation));
        lastPlayer = playerList[playerList.Count - 1];
        lastPlayer.name += (" - " + added);
        lastPlayerMovement = lastPlayer.GetComponent<Movement>();
        rbPlayer = lastPlayerMovement.GetComponent<Rigidbody>();
        lastEffect = lastPlayer.GetComponent<PlayerEffect>();
        // lastEffect.canWork = false;
        // Debug.Log("l'effet du " + lastPlayer.name + " est " + lastEffect.canWork);
        
        added++;
    }
}