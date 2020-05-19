using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationFeedback : MonoBehaviour
{

    public UPS uPS;

    private FMOD.Studio.EventInstance joueurVitesse;
    private FMOD.Studio.EventDescription joueurVitesseDescription;
    private FMOD.Studio.PARAMETER_DESCRIPTION accelerationDescription;
    private FMOD.Studio.PARAMETER_ID accelerationID;

    private float speedNumber;


    private void Start()
    {
        joueurVitesse = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Personnage longiforme/joueur_vitesse");
        joueurVitesse.start();

        joueurVitesseDescription = FMODUnity.RuntimeManager.GetEventDescription("event:/DA glitch/Personnage longiforme/joueur_vitesse");
        Debug.Log(joueurVitesseDescription);

        joueurVitesseDescription.getParameterDescriptionByName("acceleration_player", out accelerationDescription);

        accelerationID = accelerationDescription.id;
        Debug.Log(accelerationID);
    }


    private void Update()
    {
        
        speedNumber = uPS.actualsUPSPlayer;
        Debug.Log("speednumber =" + speedNumber);

        joueurVitesse.setParameterByID(accelerationID, speedNumber);
    }

}
