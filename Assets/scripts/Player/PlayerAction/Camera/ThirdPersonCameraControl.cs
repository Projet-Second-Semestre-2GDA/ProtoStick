using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    [SerializeField, Range(0, 1)] private float sensitivity = 0.8f;
    [SerializeField] public Transform target, player;
    float mouseX, mouseY;
    private PlayerNumber playerNumber;
    [HideInInspector]
    [SerializeField] private Transform obstruction;
    private List<GameObject> objectInvisible = new List<GameObject>();

    [SerializeField, Range(1f, 10f)] private float rotationSpeed = 1;
    [SerializeField,Range(1f,10f)] private float zoomSpeed = 3f;
    float distanceFromTarget;

    [SerializeField] private bool showObsacleInView = true;

    [SerializeField, MinMaxSlider(-90f,90f,true)] private Vector2 angleBorn = new Vector2(-90f,60f);
    
    void Start()
    {
        
        mouseX = transform.rotation.eulerAngles.y;
        obstruction = target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        distanceFromTarget = Vector3.Distance(target.position, transform.position);
        playerNumber = player.GetComponent<PlayerNumber>();
    }

    private void LateUpdate()
    {
        CamControl();
        if (!showObsacleInView)
        {
            ViewObstructed();
        }
    }
    

    void CamControl()
    {
        mouseX += (Input.GetAxis("CameraHorizontal" + playerNumber.playerNumber)*sensitivity) * rotationSpeed;
        mouseY -= (Input.GetAxis("CameraVertical" + playerNumber.playerNumber)*sensitivity) * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, angleBorn.x, angleBorn.y);

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
            if (!hit.collider.gameObject.CompareTag("Player"))
            {
                obstruction = hit.transform;
                obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

                if (!objectInvisible.Contains(obstruction.gameObject))
                {
                    objectInvisible.Add(obstruction.gameObject);
                }

                if (Vector3.Distance(obstruction.position, transform.position) >= distanceFromTarget/2 && Vector3.Distance(transform.position, target.position) >= distanceFromTarget/4)
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