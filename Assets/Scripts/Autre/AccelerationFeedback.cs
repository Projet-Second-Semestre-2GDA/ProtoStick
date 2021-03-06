﻿using System.Collections;
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
        //Debug.Log("----------------------------------Game Begin----------------------------------");
    }

    private void Start()
    {
        
        
        uPS = GetComponent<UPS>();
        // FMODUnity.RuntimeManager.
        joueurVitesse = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Personnage longiforme/joueur_acceleration");
        // joueurVitesse.setVolume(2);
        

    }


    private void Update()
    {

        //Debug.Log(FindObjectsOfType<FMODUnity.RuntimeManager>().Length);
        // joueurVitesse.setVolume(2);
        speedNumber = uPS.actualsUPSPlayer;
        var result = joueurVitesse.setParameterByName("acceleration_player_flanger", speedNumber);
        var result2 = joueurVitesse.setParameterByName("acceleration_player_volume", speedNumber);
        //Debug.Log("player speed flanger : " + result);
        //Debug.Log("player speed volume : " + result2);
        PLAYBACK_STATE state;
        joueurVitesse.getPlaybackState(out state);
        //Debug.Log("State : " + state);

        //GlobalParameterAcceleration();

        if (state == PLAYBACK_STATE.STOPPED)
        {
            joueurVitesse.start();
        }
        // else
        // {
        //     float accelerationPlayerFlanger;
        //     joueurVitesse.getParameterByName("acceleration_player_flanger", out accelerationPlayerFlanger);
        //     Debug.Log("acceleration_player_flanger : " + accelerationPlayerFlanger);
        //
        //     float accelerationPlayerVolume;
        //     joueurVitesse.getParameterByName("acceleration_player_volume", out accelerationPlayerVolume);
        //     Debug.Log("acceleration_player_volume : " + accelerationPlayerVolume);
        //
        //     float volume;
        //     float finalVolume;
        //     joueurVitesse.getVolume(out volume,out finalVolume);
        //     Debug.Log("Volume : " + volume);
        //     Debug.Log("Final volume : " + finalVolume);
        //
        //
        //     float pitch;
        //     float finalPitch;
        //     joueurVitesse.getPitch(out pitch,out finalPitch);
        //     Debug.Log("Pitch : " + pitch);
        //     Debug.Log("Final Pitch : " + finalPitch);
        // }
    }

    private void GlobalParameterAcceleration()
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("acceleration_player_flanger", speedNumber);
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("acceleration_player_volume", speedNumber);

        float speed1;
        float speed2;
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("acceleration_player_flanger", out speed1);
        FMODUnity.RuntimeManager.StudioSystem.getParameterByName("acceleration_player_volume", out speed2);

        //Debug.Log("speed 1 : " + speed1);
        //Debug.Log("speed 2 : " + speed2);
    }
}
