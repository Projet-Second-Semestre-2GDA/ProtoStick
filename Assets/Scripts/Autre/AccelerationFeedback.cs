using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;

public class AccelerationFeedback : MonoBehaviour
{

    private UPS uPS;

    private FMOD.Studio.EventInstance joueurVitesse;
    private FMOD.Studio.EventDescription joueurVitesseDescription;
    private FMOD.Studio.PARAMETER_DESCRIPTION accelerationDescription;
    private FMOD.Studio.PARAMETER_ID accelerationID;

    private float speedNumber;

    private void Awake()
    {
        speedNumber = 250;
        Debug.Log("----------------------------------Game Begin----------------------------------");
    }

    private void Start()
    {
        uPS = GetComponent<UPS>();

        joueurVitesse = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Personnage longiforme/joueur_vitesse");
        
    }


    private void Update()
    {
        speedNumber = uPS.actualsUPSPlayer;
        var result = joueurVitesse.setParameterByName("acceleration_player_flanger", speedNumber);
        var result2 = joueurVitesse.setParameterByName("acceleration_player_volume", speedNumber);
        Debug.Log("player speed flanger : " + result);
        Debug.Log("player speed volume : " + result2);
        PLAYBACK_STATE state;
        joueurVitesse.getPlaybackState(out state);

        if (state == PLAYBACK_STATE.STOPPED)
        {
            joueurVitesse.start();
        }
    }

}
