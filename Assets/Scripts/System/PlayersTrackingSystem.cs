using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

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

    private int winnerNumber = -1;
    
    private void Awake()
    {
        if (waypointsContainer == null || players == null) throw new NullReferenceException();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Je vais stocker et créer mets listes pour plus tard
        for (int i = 0; i < players.Count; i++)
        {
            scriptPlayerPositions.Add(players[i].GetComponent<PlayerPosition>());
            playersPosition.Add(new List<Vector3>());
        }
        //Ici, pour des raisons de performances, étant donné que mes waypoints
        //ne sont aucunement suseptible de bouger durant la partie,
        //je stock leurs positions et leurs orientation pour les réutiliser plus tard
        //Sans avoir d'appelle et de recherche à faire
        for (int i = 0; i < waypointsContainer.childCount; i++)
        {
            var pos = waypointsContainer.GetChild(i).position;
            pos.y = 0;
            waypointsPosition.Add(pos);
            waypointsForward.Add(waypointsContainer.GetChild(i).TransformDirection(Vector3.forward));
        }
        //Ici, pour des raisons de comparaison, je récupère la distance la plus courte
        //qu'il existeentre deux waypoints
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
        //J'active les scripts si on est en deux joueurs
        foreach (var player in scriptPlayerPositions)
        {
            player.SetScriptActive(twoPlayeMode);
        }
        if (twoPlayeMode)
        {
            //Je créer des listes pour stocker et comparer plus tard
            //Le waypoint sur lequel les joueurs sont associé
            //Et la distance total qu'ils ont parcouru.
            //Sachant que le waypoint se situera toujours derrière eux.
            List<int> waypointNumber = new List<int>();
            List<float> totalDistance = new List<float>();
            for (int i = 0; i < players.Count; i++)
            {
                //Je récupère la position et vu que je ne veux pas comparer dans la hauteur, je la mets à 0
                var player = players[i];
                var playerPos = player.transform.position;
                playersPosition[i].Add(playerPos);
                playerPos.y = 0;
                //Je set les valeurs de bases à des notions qui seront en théorie forcément changer
                waypointNumber.Add(-1);
                totalDistance.Add(0);
                
                //Ici, je créer une variable qui correspond à la distance entre le waypoint actuelles et le joueurs.
                //Elle corresponds surtout a cette distance lors de l'itération précédente
                float distanceFromPlayer = Mathf.Infinity;
                for (int j = 0; j < waypointsPosition.Count; j++)
                {
                    //dst est la distance à cette itération 
                    var dst = Vector3.Distance(playerPos, waypointsPosition[j]);
                    //Je compare les deux distance, étant donné que l'on se rapproche du joueur,
                    //la distance est censé toujours diminuer, si elle remonte, c'est que l'on s'éloigne du joueur
                    //Et que par conséquent on a déjà trouver la waypoint de référence du joueur.
                    if (dst < distanceFromPlayer)
                    {
                        distanceFromPlayer = dst;
                        waypointNumber[i] = j;
                    }
                    else if (dst >= distanceFromPlayer)
                    {
                        //Vérifié que le waypoint de référence est toujours derrière le joueurs
                        //Si le waypoints est devant, alors on prends le précédents
                        //Ainsi, le waypoint de référence est toujours le dernier.
                        if (Vector3.Dot(waypointsForward[waypointNumber[i]],player.transform.TransformDirection(Vector3.forward)) < 0 && waypointNumber[i] >0)
                        {
                            waypointNumber[i] = waypointNumber[i] - 1;
                        }
                        break;
                    }
                    
                    // if (dst < minDistanceBetweenWaypoints / 2)
                    // {
                    //     break;
                    // }
                }
                //On calcule la distance globale qui sépare le joueur du waypoints de départ
                //Pour cela on calcul d'abord la distance entre chaque waypoint jusqu'au waypoint de référence.
                for (int j = 1; j <= waypointNumber[i]; j++)
                {
                    totalDistance[i] += Vector3.Distance(waypointsPosition[j - 1], waypointsPosition[j]);
                }
                //Puis, le joueur étant forécement devant sont waypoint de référence,
                //On rajoute là distance entre le waypoint de ref et le joueur
                totalDistance[i] += Vector3.Distance(waypointsPosition[waypointNumber[i]],playerPos);
            }
            
            //Simple débug pour vérifier que tout est en ordre
            for (int i = 0; i < players.Count; i++)
            {
                Debug.Log("----------------Joueur" + i + "----------------");
                Debug.Log("WaypointNumber " + waypointNumber[i]);
                Debug.Log("totalDistance " + totalDistance[i]);
            }
            Debug.Log("----------------End----------------");
            
            //Ici, on vérifie qui gagne
            int idWinner = -1;
            //Tout d'abords, on vérifie l'ID du waypoint
            //Si les waypoints de référence sont différentes, alors nécéssairement,
            //il y en a un devant l'autre
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
                //S'il sont sur le même waypoint, 
                //On compare leurs distance totale parcouru, celui qui en a le plus gagne
                if (totalDistance[0] > totalDistance[1])
                {
                    idWinner = 0;
                }
                else if (totalDistance[0] < totalDistance[1])
                {
                    idWinner = 1;
                }
                else
                {
                    //S'ils sont à équi distance, on en choisi un au hasard
                    idWinner = Random.Range(0, 2);
                }
            }
            
            //De ce fait, étant une course à deux joueurs, le perdant est nécéssairement l'autre joueur.
            var idLooser = (idWinner + 1 > 1) ? 0 : 1;
            
            //Grâce a une simple soustraction, calculer la distance séparant les deux joueurs.
            distanceBetweenPlayer = Mathf.Clamp(totalDistance[idWinner] - totalDistance[idLooser],distanceMinMaxMusic.x,distanceMinMaxMusic.y);
            Debug.Log("distanceBetweenPlayer : " + distanceBetweenPlayer);
            //return the distance to FMOD
            SetWinner(idWinner);
        }
        else
        {
            winnerNumber = -1;
        }
    }

    void SetWinner(int idWinner)
    {
        //Ici on va donc mettre le numéro et non pas son ID dans l'integer
        //winnerNumber, par une simple addition.
        winnerNumber = idWinner + 1;
        Debug.Log("Le WinnerNumber est " + winnerNumber);
        var idLooser = (idWinner + 1 > 1) ? 0 : 1;
        //Ici on va donc grâcec à leurs "ID" mettre leurs classement à jours.
        scriptPlayerPositions[idWinner].SetPlayerClassement(1);
        scriptPlayerPositions[idLooser].SetPlayerClassement(2);
        
    }

    public int GetWinner()
    {
        if (winnerNumber < 0) throw new ArgumentOutOfRangeException("playerNumber", winnerNumber, "The player number is not an actual Player");
        else return winnerNumber;
    }
    public int[] GetRanking()
    {
        var idWinner = winnerNumber - 1;
        var looserNumber = (idWinner + 1 > 1) ? 0 : 1;
        ++looserNumber;
        Debug.Log("Ici, le WinnerNumber est " + winnerNumber);
        Debug.Log("OtherNumber : " + looserNumber);
        int[] ranking = new [] { winnerNumber-1, looserNumber-1 };
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
