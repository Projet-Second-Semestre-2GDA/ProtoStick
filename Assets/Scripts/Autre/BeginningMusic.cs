using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningMusic : MonoBehaviour
{

    private void Start()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Level Design/LD_Départ_course");

        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Musique/Msc_Bassline");

        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Musique/Msc_Pride_Ascent");

        FMODUnity.RuntimeManager.PlayOneShot("event:/DA glitch/Musique/Msc_Dexterity_Ascent");
    }

    private void Update()
    {
        /*if (Input.GetAxis("Pause") != 0)
        {
            Studio.EventInstance.setPaused(bool paused);
        }*/
    }


}
