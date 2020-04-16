using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
// using System.Collections;



public class ReloadScene : MonoBehaviour
{



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
             Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
        }
    }


}
