using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginningMusic : MonoBehaviour
{
    public List <GameObject> player;

    private FMOD.Studio.EventInstance comptageDepart;

    private FMOD.Studio.EventInstance bassLine;
    private FMOD.Studio.EventInstance prideAscent;
    private FMOD.Studio.EventInstance dexterityAscent;

    public ModeUnPlayer modeUnPlayer;


    


    private void Start()
    {
       
        

        comptageDepart = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_Départ_course");

        bassLine = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Musique/Msc_Bassline");
        prideAscent = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Musique/Msc_Pride_Ascent");
        dexterityAscent = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Musique/Msc_Dexterity_Ascent");

        

        bassLine.start();
        prideAscent.start();
        dexterityAscent.start();

        comptageDepart.start();

    }

    private void Update()
    {
        if (modeUnPlayer.modeUnJoueur)
        {
            dexterityAscent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        


        if (Input.GetAxis("Pause") != 0)
        {
            comptageDepart.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            bassLine.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            prideAscent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            dexterityAscent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }


}
