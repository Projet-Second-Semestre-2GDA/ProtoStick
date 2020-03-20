using UnityEngine;
using System.Collections;

public class PickableObject : MonoBehaviour
{
    [HideInInspector]public Transform player;
    [HideInInspector]public Transform playerCam;
    public float throwForce = 10;





    // Détection de contact grace au collider is trigger
    void OnTriggerEnter()
    {
        if (!player)
        {
            LeaveObject();
        }
    }

    public void TakeObject(Transform playerWhoTake, Transform camera)
    {
        if(player) return;
        Debug.Log("Take object");

        player = playerWhoTake;
        playerCam = camera;
    
        GetComponent<Rigidbody>().isKinematic = true;
        transform.parent = playerCam;

        playerWhoTake.GetComponent<ControlObject>().SetBall(gameObject);

    }

    public void ThrowObject()
    {
        if (!player) return;
        Debug.Log("ThrowObject");

        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(playerCam.forward *throwForce);
        rb.velocity += player.GetComponent<Rigidbody>().velocity;
        DeletePlayer();

    }
    
    public void LeaveObject()
    {
        if(!player) return;    
        Debug.Log("Leave Object");

        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;
        DeletePlayer();
    }

    private void DeletePlayer()
    {
        Debug.Log("Delete player");
        player.GetComponent<ControlObject>().LeaveBall();
        player = null;
        playerCam = null;
    }
}