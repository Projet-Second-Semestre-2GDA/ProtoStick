using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeginningMusic : MonoBehaviour
{
    public List <GameObject> player;

    private FMOD.Studio.Bus masterBus;

    private FMOD.Studio.EventInstance comptageDepart;
    private FMOD.Studio.EventDescription comptageDepartDescription;
    private FMOD.Studio.PARAMETER_DESCRIPTION beginningRaceDescription;
    private FMOD.Studio.PARAMETER_ID beginningRaceId;

    private FMOD.Studio.EventInstance bassLine;
    private FMOD.Studio.EventInstance prideAscent;
    private FMOD.Studio.EventInstance dexterityAscent;

    private FMOD.Studio.EventInstance feedbackChute;

    public ModeUnPlayer modeUnPlayer;

    public PlayersTrackingSystem playersTrackingSystem;

    public float debutCourse;
    private List<FMOD.Studio.EventInstance> listeInstance;
    private bool isPause;

    private void Start()
    {
        masterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        bassLine = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Musique/Msc_Bassline");
        prideAscent = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Musique/Msc_Pride_Ascent");
        dexterityAscent = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Musique/Msc_Dexterity_Ascent");

        bassLine.start();
        prideAscent.start();
        dexterityAscent.start();

        
        
        comptageDepartDescription = FMODUnity.RuntimeManager.GetEventDescription("event:/DA glitch/Level Design/LD_Départ_course");
        comptageDepartDescription.getParameterDescriptionByName("beginning_race", out beginningRaceDescription);

        beginningRaceId = beginningRaceDescription.id;

        comptageDepart.setParameterByID(beginningRaceId, debutCourse);

        VoiceLinePlaying.PlaySound("event:/DA glitch/Level Design/LD_Départ_course", VoiceLinePriority.gigantic);

        FMODUnity.RuntimeManager.PauseAllEvents(false);

        listeInstance.Add(prideAscent); //Correspond à "listeInstance[0]" et donc la musique du joueur 1
        listeInstance.Add(dexterityAscent); //Correspond à "listeInstance[1]" et donc la musique du joueur 2
    }

    private void Update()
    {


        if (modeUnPlayer.modeUnJoueur)
        {
            dexterityAscent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
        else
        {
            var ranking = playersTrackingSystem.GetRanking(); //Liste des IDs des joueurs en fonction de leurs position
            var value = playersTrackingSystem.GetPercent(); //Taux de retard du joueur en seconde position par rapport au premier
            
            //Supposons Joueur 2 en première position
            //Alors ranking[0] qui est le joueur en première position sera égale à 1
            //Donc, listeInstance[ ranking[0] ] correspond à listeInstance[1]
            //Et donc, à l'instance du second joueur.
            //Et inversement.

            listeInstance[ ranking[0] ].setParameterByName("retard_du_joueur", 0); //Set du volume du joueur en première position
            listeInstance[ ranking[1] ].setParameterByName("retard_du_joueur", value); //Set du volume du joueur en seconde position
        }

        if (!isPause)
        {
            FMODUnity.RuntimeManager.PauseAllEvents(false);
        }


        if (isPause)
        {
            // FMODUnity.RuntimeManager.PauseAllEvents(true);
        }
        else
        {
            // FMODUnity.RuntimeManager.PauseAllEvents(false);
        }
    }

    public void ResumeMusic()
    {
        isPause = false;
        FMODUnity.RuntimeManager.PauseAllEvents(false);
    }

    public void StopMusicMenu()
    {
        FMODUnity.RuntimeManager.PauseAllEvents(true);
        VoiceLinePlaying.ForceStopCurrentVoice();
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void PauseMusic()
    {
        isPause = true;
        FMODUnity.RuntimeManager.PauseAllEvents(true);

    }

}
