using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public static class VoiceLinePlaying 
{


    private static FMOD.Studio.EventInstance currentSound;

    

    public static void PlaySound(string path, bool playNow = false)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        currentSound.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            currentSound = FMODUnity.RuntimeManager.CreateInstance(path);
            currentSound.start();
        }
        else if (playNow)
        {
            currentSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSound = FMODUnity.RuntimeManager.CreateInstance(path);
            currentSound.start();
        }
        
    }

    public static void ForceStopCurrentVoice()
    {
        currentSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

}
