using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    private void Start()
    {
        uPS = GetComponent<UPS>();

        joueurVitesse = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Personnage longiforme/joueur_vitesse");
        joueurVitesse.start();
    }


    private void Update()
    {
        speedNumber = uPS.actualsUPSPlayer;
        joueurVitesse.setParameterByName("acceleration_player", speedNumber);
    }

}
