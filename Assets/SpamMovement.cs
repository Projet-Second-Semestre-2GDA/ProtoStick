using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamMovement : MonoBehaviour
{
    Animator playerAnimation;
    // Start is called before the first frame update
    void Start()
    {
        playerAnimation = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimation.SetFloat("Walk", 1);
    }
}
