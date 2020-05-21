using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
// using Sirenix.Utilities.Editor.Expressions;

public class LineTracer : MonoBehaviour
{
    [Title("GameObject Needed")]
    [SerializeField] private LineRenderer lineRenderer;

    [SerializeField] private Transform pointToGetPosition;

    [Title("Parameters")] 
    [SerializeField] private int numberOfPosition = 50;
    [SerializeField] private float refreshTime = 0.5f;
    private float nexRefreshTime;
    private List<Vector3> positions = new List<Vector3>();

    private void Start()
    {
        if (lineRenderer == null) lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null) throw new NullReferenceException("Aucun LineRenderer n'est sur l'objet \"" + name + "\".");

        var pos = pointToGetPosition.position;
        
        for (int i = 0; i < numberOfPosition; i++)
        {
            positions.Add(pos);
        }
        

        

        nexRefreshTime = Time.time + refreshTime;
    }

    private void Update()
    {
        var time = Time.time;
        if (time >= nexRefreshTime)
        {
            AddPosition(pointToGetPosition.position);
            
            nexRefreshTime = time + refreshTime;
        }
    }

    private void AddPosition(Vector3 position)
    {
        positions.RemoveAt(0);
        positions.Add(position);
        lineRenderer.positionCount = numberOfPosition;
        lineRenderer.SetPositions(positions.ToArray());
    }
}
