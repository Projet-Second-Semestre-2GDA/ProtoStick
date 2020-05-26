using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

public static class VoiceLinePlaying
{


    private static FMOD.Studio.EventInstance currentSound;
    private static VoiceLinePriority soundPriority;


    public static void PlaySound(string path, VoiceLinePriority priotity = VoiceLinePriority.None)
    {
        FMOD.Studio.PLAYBACK_STATE state;
        currentSound.getPlaybackState(out state);
        if (state == FMOD.Studio.PLAYBACK_STATE.STOPPED)
        {
            currentSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            currentSound = FMODUnity.RuntimeManager.CreateInstance(path);
            currentSound.start();
            soundPriority = priotity;
        }
        else if (priotity != VoiceLinePriority.None && (int)priotity > (int)soundPriority)
        {
            currentSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            currentSound = FMODUnity.RuntimeManager.CreateInstance(path);
            currentSound.start();
        }
    }

    public static void ForceStopCurrentVoice()
    {
        currentSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

}

public enum VoiceLinePriority
{
    None = -1,
    small = 1,
    big = 2,
    gigantic = 3
}