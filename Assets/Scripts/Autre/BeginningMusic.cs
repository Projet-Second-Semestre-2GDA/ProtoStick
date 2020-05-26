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

    public float debutCourse;


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

    }

    private void Update()
    {


        if (modeUnPlayer.modeUnJoueur)
        {
            dexterityAscent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }

        


        if (Input.GetAxis("Pause") != 0)
        {
            
            FMODUnity.RuntimeManager.PauseAllEvents(true);
        }
    }

    public void ResumeMusic()
    {
        
        FMODUnity.RuntimeManager.PauseAllEvents(false);
    }

    public void StopMusicMenu()
    {
        
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }


}
