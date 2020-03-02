using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{

    [SerializeField] private GameObject prefabsPlayer;
    [HideInInspector]public List<GameObject> playerList;
    
    //private GameObject playerList[playerList.Count - 1];
    public Transform spawnPointPlayer;

    //timer
    public float timer;
    private float timerSave;

    private Rigidbody rbPlayer;

    private bool rbDesactivation = false;
    //private bool activationScript = true;

    private void Start()
    {
        playerList = new List<GameObject>();
        SpawnPlayerMethod(false);

    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnPlayerMethod();
        }
    }

    private void SpawnPlayerMethod(bool aPlayerIsInScene = true)
    {
        if (timer <= 0)
        {
            if (aPlayerIsInScene)
            {
                Movement movement = playerList[playerList.Count - 1].GetComponent<Movement>();
                if (movement != null)
                {
                    movement.DisableScript();
                }
                else
                {
                    Debug.LogError(playerList[playerList.Count - 1].name + " do not have a script Movement");
                }
                rbPlayer.useGravity = false;
                rbPlayer.constraints = RigidbodyConstraints.FreezeAll;
            }
            playerList.Add(Instantiate(prefabsPlayer, spawnPointPlayer.position, spawnPointPlayer.rotation));
            playerList[playerList.Count - 1].GetComponent<Movement>().enabled = true;
            playerList[playerList.Count - 1] = playerList[playerList.Count - 1];
            rbPlayer = playerList[playerList.Count - 1].GetComponent<Rigidbody>();
            timer = timerSave;
        }
    }
}
