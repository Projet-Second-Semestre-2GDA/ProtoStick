using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class UPS : MonoBehaviour
{
    [SerializeField] private float refreshTime = 0.5f;
    [SerializeField] private Text[] UPSAffichagePlayer = new Text[2];
    
    private float nextTimer = -1;
    
    [SerializeField] private GameObject[] players = new GameObject[2];

    private System.Collections.Generic.List<Vector3>[] previousPositionPlayer = new System.Collections.Generic.List<Vector3>[2];
    private float[] actualsUPSPlayer = new float[2];
    
    // Start is called before the first frame update
    void Start()
    {
        // var playersFounds = GameObject.FindGameObjectsWithTag("Player");
        //
        // if (playersFounds.Length > 2)
        // {
        //     Debug.LogError("Il y a plus de 2 objet, " + playersFounds.Length + " exactement");
        // }
        // // players.Initialize();
        // players = new GameObject[playersFounds.Length];
        //
        //
        // players[playersFounds[0].GetComponent<PlayerNumber>().playerIndex] = playersFounds[0];
        // players[playersFounds[1].GetComponent<PlayerNumber>().playerIndex] = playersFounds[1];
        
        for (int i = 0; i < 2; i++)
        {
            previousPositionPlayer[i] = new List<Vector3>();
        }

        nextTimer = Time.time + refreshTime;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            previousPositionPlayer[i].Add(players[i].transform.position);
        }
        if (Time.time >= nextTimer)
        {
            for (int i = 0; i < 2; i++)
            {
                actualsUPSPlayer[i] = 0;
                var distance = new List<float>();
                if (previousPositionPlayer[i].Count > 0)
                {
                    for (int j = 1; j < previousPositionPlayer[i].Count; j++)
                    {
                        distance.Add(Vector3.Distance(previousPositionPlayer[i][j], previousPositionPlayer[i][j - 1] ));
                    }

                    for (int j = 0; j < distance.Count; j++)
                    {
                        actualsUPSPlayer[i] += distance[j];
                    }
                    actualsUPSPlayer[i] /= refreshTime;
                }

                UPSAffichagePlayer[i].text = "La vitesse du joueur " + (i + 1) + " est de " + actualsUPSPlayer[i] + " UPS.     ";
                previousPositionPlayer[i].Clear();
                actualsUPSPlayer[i] = 0;
            }
            
            nextTimer = Time.time + refreshTime;
        }
    }
}
