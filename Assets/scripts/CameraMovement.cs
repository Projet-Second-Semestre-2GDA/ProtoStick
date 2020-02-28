using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject playerCameraPoint;

    private void Start()
    {
        transform.position = playerCameraPoint.transform.position;
    }

}
