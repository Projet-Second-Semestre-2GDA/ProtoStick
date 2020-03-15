using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    [SerializeField] private Transform target, player;
    float mouseX, mouseY;

    [HideInInspector]
    [SerializeField] private Transform obstruction;
    private List<GameObject> objectInvisible = new List<GameObject>();

    [SerializeField, Range(1f, 10f)] private float rotationSpeed = 1;
    [SerializeField,Range(1f,10f)] private float zoomSpeed = 3f;
    float distanceFromTarget;
    
    void Start()
    {
        mouseX = transform.rotation.eulerAngles.y;
        obstruction = target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        distanceFromTarget = Vector3.Distance(target.position, transform.position);
    }

    private void LateUpdate()
    {
        CamControl();
        ViewObstructed();
    }
    

    void CamControl()
    {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }
    

    void ViewObstructed()
    {
        if (objectInvisible.Count >0)
        {
            foreach (var item in objectInvisible)
            {
                item.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
            objectInvisible.Clear();
        }

        RaycastHit hit;

        if (Physics.Raycast(transform.position, target.position - transform.position, out hit, distanceFromTarget))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (!objectInvisible.Contains(obstruction.gameObject))
                {
                    objectInvisible.Add(obstruction.gameObject);
                }

                if (Vector3.Distance(obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, target.position) >= 1.5f)
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
            else
            {
                obstruction = null;
                if (Vector3.Distance(transform.position, target.position) < distanceFromTarget)
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
    }
}