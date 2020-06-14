using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayersTrackingSystem : MonoBehaviour
{
    [Title("Functionnement")]
    [SerializeField] private Transform waypointsContainer;
    [SerializeField] private List<GameObject> players;
    [Title("Music Parameters")]
    [SerializeField, MinMaxSlider(0,5000,true)]private Vector2 distanceMinMaxMusic = new Vector2(100,1000);  
    private List<PlayerPosition> scriptPlayerPositions = new List<PlayerPosition>();
    private List<List<Vector3>> playersPosition = new List<List<Vector3>>();
    private List<Vector3> waypointsPosition = new List<Vector3>();
    private List<Vector3> waypointsForward = new List<Vector3>();
    private float minDistanceBetweenWaypoints = Mathf.Infinity;
    
    [HideInInspector] public float distanceBetweenPlayer = -1;

    [HideInInspector]public bool twoPlayeMode;

    private int playerNumber = -1;
    
    private void Awake()
    {
        if (waypointsContainer == null || players == null) throw new NullReferenceException();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < players.Count; i++)
        {
            scriptPlayerPositions.Add(players[i].GetComponent<PlayerPosition>());
            playersPosition.Add(new List<Vector3>());
        }
        for (int i = 0; i < waypointsContainer.childCount; i++)
        {
            var pos = waypointsContainer.GetChild(i).position;
            pos.y = 0;
            waypointsPosition.Add(pos);
            waypointsForward.Add(waypointsContainer.GetChild(i).TransformDirection(Vector3.forward));
        }

        for (int i = 1; i < waypointsPosition.Count; i++)
        {
            var distance = Vector3.Distance(waypointsPosition[i - 1], waypointsPosition[i]);
            if (distance < minDistanceBetweenWaypoints)
            {
                minDistanceBetweenWaypoints = distance;
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var player in scriptPlayerPositions)
        {
            player.SetScriptActive(twoPlayeMode);
        }
        if (twoPlayeMode)
        {
            List<int> waypointNumber = new List<int>();
            List<float> totalDistance = new List<float>();
            for (int i = 0; i < players.Count; i++)
            {
                var player = players[i];
                var playerPos = player.transform.position;
                playersPosition[i].Add(playerPos);
                playerPos.y = 0;

                waypointNumber.Add(-1);
                totalDistance.Add(0);

                float distanceFromPlayer = Mathf.Infinity;


                for (int j = 0; j < waypointsPosition.Count; j++)
                {
                    var dst = Vector3.Distance(playerPos, waypointsPosition[j]);
                    if (dst < distanceFromPlayer)
                    {
                        distanceFromPlayer = dst;
                        waypointNumber[i] = j;
                    }
                    else if (dst >= distanceFromPlayer)
                    {
                        //Vérifié que le waypoint de référence est toujours derrière le joueur
                        if (Vector3.Dot(waypointsForward[waypointNumber[i]],player.transform.TransformDirection(Vector3.forward)) < 0 && waypointNumber[i] >0)
                        {
                            waypointNumber[i] = waypointNumber[i] - 1;
                        }
                        break;
                    }
                    
                    if (dst < minDistanceBetweenWaypoints / 2)
                    {
                        break;
                    }
                }

                for (int j = 1; j <= waypointNumber[i]; j++)//Calculer la distance globale qui séparer le joueur du waypoints de départ
                {
                    totalDistance[i] += Vector3.Distance(waypointsPosition[j - 1], waypointsPosition[j]);
                }
                totalDistance[i] += Vector3.Distance(waypointsPosition[waypointNumber[i]],playerPos);
            }
            for (int i = 0; i < players.Count; i++)
            {
                Debug.Log("----------------Joueur" + i + "----------------");
                Debug.Log("WaypointNumber " + waypointNumber[i]);
                Debug.Log("totalDistance " + totalDistance[i]);
            }

            int idWinner = -1;
            if (waypointNumber[0] > waypointNumber[1])
            {
                idWinner = 0;
            }
            else if (waypointNumber[0] < waypointNumber[1])
            {
                idWinner = 1;
            }
            else
            {
                if (totalDistance[0] > totalDistance[1])
                {
                    idWinner = 0;
                }
                else
                {
                    idWinner = 1;
                }
            }
            
            var idLooser = (idWinner + 1 > 1) ? 0 : 1;
            //Grâce a une simple soustraction, calculer la distance séparant les deux joueurs.
            distanceBetweenPlayer = Mathf.Clamp(totalDistance[idWinner] - totalDistance[idLooser],distanceMinMaxMusic.x,distanceMinMaxMusic.y);
            Debug.Log("distanceBetweenPlayer : " + distanceBetweenPlayer);
            //return the distance to FMOD
            SetWinner(idWinner);
        }
        else
        {
            playerNumber = -1;
        }
    }

    void SetWinner(int idWinnder)
    {
        playerNumber = idWinnder + 1;
        if (idWinnder == 0)
        {
            scriptPlayerPositions[0].SetPlayerClassement(1);
            scriptPlayerPositions[1].SetPlayerClassement(2);
        }
        else
        {
            scriptPlayerPositions[1].SetPlayerClassement(1);
            scriptPlayerPositions[0].SetPlayerClassement(2);
        }
    }

    public int GetWinner()
    {
        if (playerNumber < 0) throw new ArgumentOutOfRangeException("playerNumber", playerNumber, "The player number is not an actual Player");
        else return playerNumber;
    }
    public int[] GetRanking()
    {
        var otherNumber = (playerNumber + 1 > players.Count) ? 1 : playerNumber + 1;
        int[] ranking = new [] { playerNumber-1, otherNumber-1 };
        return ranking;
    }

    public float GetPercent()
    {
        var t = (distanceBetweenPlayer - distanceMinMaxMusic.x) / (distanceMinMaxMusic.y - distanceMinMaxMusic.x);
        var lerp = Mathf.Lerp(0, -1,t);
        //Lerp :
        //+000 ---------> +001
        //-127 ---------> 2pi/2
        return lerp;
    }
}
