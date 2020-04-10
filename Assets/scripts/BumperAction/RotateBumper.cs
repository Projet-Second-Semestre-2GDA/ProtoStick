using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBumper : MonoBehaviour
{
    private List<Vector3> points = new List<Vector3>();
    private Vector3 vecteurNormal;

    public void SetPoints(Vector3[] vectors)
    {
        points.Clear();
        foreach (var item in vectors)
        {
            points.Add(item);
        }
        SetRotation();
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var point in points)
        {
            Gizmos.DrawWireSphere(point,0.01f);
        }
        
        Gizmos.color = Color.green;
        var pos = transform.position;
        Gizmos.DrawLine(pos,pos + vecteurNormal.normalized);
        
    }

    private void SetRotation()
    {
        vecteurNormal = HelpOrientation.GetVecteurNormal(points[0], points[1], points[2]);
        transform.LookAt(transform.position + vecteurNormal.normalized);
    }
}
