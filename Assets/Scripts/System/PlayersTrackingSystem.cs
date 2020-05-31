using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersTrackingSystem : MonoBehaviour
{
    [SerializeField] private Transform waypointsContainer;
    [SerializeField] private List<GameObject> players;
    private List<PlayerPosition> scriptPlayerPositions = new List<PlayerPosition>();
    private List<List<Vector3>> playersPosition = new List<List<Vector3>>();
    private List<Vector3> waypointsPosition = new List<Vector3>();
    private float minDistanceBetweenWaypoints = Mathf.Infinity;

    [HideInInspector]public bool twoPlayeMode;

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

                    if (dst < minDistanceBetweenWaypoints / 2)
                    {
                        break;
                    }
                }

                for (int j = 1; j <= waypointNumber[i]; j++)
                {
                    totalDistance[i] += Vector3.Distance(waypointsPosition[j - 1], waypointsPosition[j]);
                }
                totalDistance[i] += (waypointsPosition[waypointNumber[i]] - playerPos).magnitude;
            }

            for (int i = 0; i < players.Count; i++)
            {
                Debug.Log("----------------Joueur" + i + "----------------");
                Debug.Log("WaypointNumber " + waypointNumber[i]);
                Debug.Log("totalDistance " + totalDistance[i]);
            }
            if (waypointNumber[0] > waypointNumber[1])
            {
                SetWinner(0);
            }
            else if (waypointNumber[0] < waypointNumber[1])
            {
                SetWinner(1);
            }
            else
            {
                if (totalDistance[0] > totalDistance[1])
                {
                    SetWinner(0);
                }
                else
                {
                    SetWinner(1);
                }
            }
        }
    }

    void SetWinner(int idWinnder)
    {
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
}
