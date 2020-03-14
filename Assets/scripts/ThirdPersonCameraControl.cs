using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraControl : MonoBehaviour
{
    public Transform Target, Player;
    float mouseX, mouseY;

    [HideInInspector]
    public Transform Obstruction;

    [SerializeField, Range(1f, 10f)] private float rotationSpeed = 1;
    [SerializeField,Range(1f,10f)] private float zoomSpeed = 3f;
    float distanceFromTarget;
    
    void Start()
    {
        mouseX = transform.rotation.eulerAngles.y;
        Obstruction = Target;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        distanceFromTarget = Vector3.Distance(Target.position, transform.position);
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

        transform.LookAt(Target);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
        }
        else
        {
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Player.rotation = Quaternion.Euler(0, mouseX, 0);
        }
    }
    

    void ViewObstructed()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Target.position - transform.position, out hit, 4.5f))
        {
            if (hit.collider.gameObject.tag != "Player")
            {
                Obstruction = hit.transform;
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                
                if(Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
            else
            {
                Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (Vector3.Distance(transform.position, Target.position) < distanceFromTarget)
                    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
    }
}