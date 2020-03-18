using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class EffectAttaching : MonoBehaviour
{
    [SerializeField, PropertyRange(0, 2)] private float sizeChecker = 1f;
    private FixedJoint joint;

    Vector3[] directionOrdre = new Vector3[6]
        {Vector3.down, Vector3.forward, Vector3.right, Vector3.back, Vector3.left, Vector3.up};
    
    private Vector3 lookAt = Vector3.down;
    private void Start()
    {
    }

    public void Attach()
    {

        RaycastHit hit;
        var trans = transform;
        var actualPosition = trans.position;
        
        trans.LookAt(actualPosition + Vector3.up);
        //Debug.Log("Je fais des trucs");
        
        for (int i = 0; i < directionOrdre.Length; i++)
        {
            var directionCheck = directionOrdre[i];
            //Debug.Log("direction check : " + directionCheck);
            if (Physics.Raycast(actualPosition,directionCheck,out hit,sizeChecker))
            {
                //Debug.Log("elle a réussi !");
                var direction = actualPosition - hit.point;
                direction = direction.normalized;
                //Debug.Log("La direction obtenu du calcul est : " + direction);
                trans.position = hit.point + direction * (0.4f/3);
                lookAt =actualPosition + direction;
                trans.LookAt(lookAt);
                
                
                //Debug.Log("La position est donc : " + trans.position);
                //Debug.Log("La rotation est donc : " + direction);
                break;
                
            }
        }



        // var rbs = listRb.allRigidbody;
        // if (rbs.Count >0)
        // {
        //     int indexLowerPoint = -1;
        //     for (int i = 0; i < rbs.Count; i++)
        //     {
        //         if (rbs.Contains(rbs[i].GetComponent<Rigidbody>()))
        //         {
        //             if (indexLowerPoint == -1)
        //             {
        //                 indexLowerPoint = i;
        //             }
        //             else
        //             {
        //                 if (rbs[indexLowerPoint].point.y > rbs[i].point.y)
        //                 {
        //                     indexLowerPoint = i;
        //                 }
        //             }
        //         }
        //         
        //     }
        //     var position = rbs[indexLowerPoint].point;
        //     var rotation = rbs[indexLowerPoint].GetComponent<Collider>().transform.rotation;
        //     
        //     var trans = transform;
        //     // position += hits[indexLowerPoint].collider.transform.up * (trans.lossyScale.y / 2);
        //     /*Je dois changer la position, il trouve pas la bonne,
        //      et trouver c'est quoi le problème avec le physics.SphereCastAll
        //      et faire une fonction qui me trouve je suis ou par rapport a l'objet (droit, haut, forward,...) 
        //         et me renvoie le vector3 associé (lié au transform de l'objet évidemment)*/
        //     trans.position = position;
        //     trans.rotation = rotation;
        //     Debug.Log("Le point touché est : " + position);
        //     joint = gameObject.AddComponent<FixedJoint>();
        //     joint.connectedBody = hits[indexLowerPoint].rigidbody;
        // }
        //
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        foreach (var direction in directionOrdre)
        {
            var position = transform.position;
            Gizmos.DrawLine(position,position + (direction*sizeChecker));
        }
        
        if (lookAt == Vector3.down) return;
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(lookAt,0.1f);

    }
}