using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceLinePlaying : MonoBehaviour
{

    private FMOD.Studio.EventInstance voiceComptageDepart;
    private FMOD.Studio.EventInstance voiceRocketTouchJoueur1;
    private FMOD.Studio.EventInstance voiceRocketTouchJoueur2;
    private FMOD.Studio.EventInstance voiceVictoireJoueur1;
    private FMOD.Studio.EventInstance voiceVictoireJoueur2;

    private bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    public static bool voiceLineIsPlayingRightNow = false;

    private void Start()
    {
        voiceComptageDepart = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_Départ_course");

        voiceRocketTouchJoueur1 = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_rocket_touchée_sur_joueur_1");
        voiceRocketTouchJoueur2 = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_rocket_touchée_sur_joueur_2");

        voiceVictoireJoueur1 = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_Victoire_joueur_1");
        voiceVictoireJoueur2 = FMODUnity.RuntimeManager.CreateInstance("event:/DA glitch/Level Design/LD_Victoire_joueur_2");
    }

    private void Update()
    {
        if (IsPlaying(voiceComptageDepart) || IsPlaying(voiceRocketTouchJoueur1) || IsPlaying(voiceRocketTouchJoueur2) || IsPlaying(voiceVictoireJoueur1) || IsPlaying(voiceVictoireJoueur2))
        {
            voiceLineIsPlayingRightNow = true;
            Debug.Log("Il y a une voiceline en train de se jouer");
        }
        else voiceLineIsPlayingRightNow = false;

        Debug.Log("voice line is playing : " + voiceLineIsPlayingRightNow);
    }

}
