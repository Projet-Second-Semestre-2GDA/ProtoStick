using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;


public class GestionMenu : MonoBehaviour
{
    [TitleGroup("Sliders Sensibility")]
    public Slider sliderJ1;
    public Slider sliderJ2;
    [TitleGroup("Sliders Sound")]
    public Slider sliderVoice;
    public Slider sliderEffect;
    public Slider sliderAmbiant;
    public Slider sliderMusique;

    //Sensibility Key
    private string sensibilityKeyJ1 = "Sensibilitee1";
    private string sensibilityKeyJ2 = "Sensibilitee2";
    //Volument Key
    private string globalVolumeVoiceKey = "GlobalVolumeVoice";
    private string globalVolumeEffectKey = "GlobalVolumeEffect";
    private string globalVolumeAmbiantKey = "GlobalVolumeAmbiant";
    private string globalVolumeMusiqueKey = "GlobalVolumeMusique";

    
    private void Awake()
    {
        //-------------------Sensibility-------------------
        //Joueur 1
        if (sliderJ1 == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(sensibilityKeyJ1)) PlayerPrefs.SetFloat(sensibilityKeyJ1,0.8f);
        
        sliderJ1.value = PlayerPrefs.GetFloat(sensibilityKeyJ1);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKeyJ1));
        
        //Joueur 2
        if (sliderJ2 == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(sensibilityKeyJ2)) PlayerPrefs.SetFloat(sensibilityKeyJ2,0.8f);
        
        sliderJ2.value = PlayerPrefs.GetFloat(sensibilityKeyJ2);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKeyJ2));
        
        //-------------------Sound-------------------
        //Voice
        if (sliderVoice == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(globalVolumeVoiceKey)) PlayerPrefs.SetFloat(globalVolumeVoiceKey,100f);
        sliderVoice.value = PlayerPrefs.GetFloat(globalVolumeVoiceKey);
        
        //Effect
        if (sliderEffect == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(globalVolumeEffectKey)) PlayerPrefs.SetFloat(globalVolumeEffectKey,100f);
        sliderEffect.value = PlayerPrefs.GetFloat(globalVolumeEffectKey);

        //Ambiant
        if (sliderAmbiant == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(globalVolumeAmbiantKey)) PlayerPrefs.SetFloat(globalVolumeAmbiantKey,100f);
        sliderAmbiant.value = PlayerPrefs.GetFloat(globalVolumeAmbiantKey);

        //Musique
        if (sliderMusique == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(globalVolumeMusiqueKey)) PlayerPrefs.SetFloat(globalVolumeMusiqueKey,100f);
        sliderMusique.value = PlayerPrefs.GetFloat(globalVolumeMusiqueKey);

        
        HideCursor();
    }

    public void PlayOnPlayerMode()
    {
        PlayerOptionChoose.ModeDeJoueur = PlayerMode.OnePlayer;
        // EnterNextScene();
    }

    public void PlayTwoPlayerMode()
    {
        PlayerOptionChoose.ModeDeJoueur = PlayerMode.TwoPlayer;
        // EnterNextScene();
    }

    private void EnterNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void EnterScene(int buildSceneNumber)
    {
        SceneManager.LoadScene(buildSceneNumber);

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateSensitivityPlayerPrefJ1()
    {
        PlayerPrefs.SetFloat(sensibilityKeyJ1,sliderJ1.value);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKeyJ1));
    }
    
    public void UpdateSensitivityPlayerPrefJ2()
    {
        PlayerPrefs.SetFloat(sensibilityKeyJ2,sliderJ2.value);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKeyJ2));
    }
    [Title("Buttons Set Sliders")]
    
    [Button(ButtonSizes.Large,ButtonStyle.FoldoutButton)]
    private void SetSliderJ1To(float value = 0.8f)
    {
        PlayerPrefs.SetFloat(sensibilityKeyJ1,value);
        sliderJ1.value = PlayerPrefs.GetFloat(sensibilityKeyJ1);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKeyJ1));
    }
    
    [Button(ButtonSizes.Large,ButtonStyle.FoldoutButton)]
    private void SetSliderJ2To(float value = 0.8f)
    {
        PlayerPrefs.SetFloat(sensibilityKeyJ2,value);
        sliderJ2.value = PlayerPrefs.GetFloat(sensibilityKeyJ2);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKeyJ2));
    }

    public void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ChangeVolume(int soundType)
    {
        switch ((SoundType)soundType)
        {
            case SoundType.Voice://0
            {
                PlayerPrefs.SetFloat(globalVolumeVoiceKey,sliderVoice.value);
                sliderVoice.value = PlayerPrefs.GetFloat(globalVolumeVoiceKey);
            }
            break;
            case SoundType.Effect://1
            {
                PlayerPrefs.SetFloat(globalVolumeEffectKey,sliderEffect.value);
                sliderEffect.value = PlayerPrefs.GetFloat(globalVolumeEffectKey);
            }
            break;
            case SoundType.Ambiant://2
            {
                PlayerPrefs.SetFloat(globalVolumeAmbiantKey,sliderAmbiant.value);
                sliderAmbiant.value = PlayerPrefs.GetFloat(globalVolumeAmbiantKey);
            }
            break;
            case SoundType.Musique://3
            {
                PlayerPrefs.SetFloat(globalVolumeMusiqueKey,sliderMusique.value);
                sliderMusique.value = PlayerPrefs.GetFloat(globalVolumeMusiqueKey);
            }
            break;
        }
        

    }
}

public enum SoundType
{
    Voice,
    Effect,
    Ambiant,
    Musique
}
