using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GestionMenu : MonoBehaviour
{
    public Slider slider;
    private string sensibilityKey = "Sensibilitee";
    private void Awake()
    {
        if (slider == null)throw new NullReferenceException();
        if (!PlayerPrefs.HasKey(sensibilityKey)) PlayerPrefs.SetFloat(sensibilityKey,0.8f);
        
        slider.value = PlayerPrefs.GetFloat(sensibilityKey);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKey));
    }

    public void PlayOnPlayerMode()
    {
        PlayerOptionChoose.ModeDeJoueur = PlayerMode.OnePlayer;
        EnterNextScene();
    }

    public void PlayTwoPlayerMode()
    {
        PlayerOptionChoose.ModeDeJoueur = PlayerMode.TwoPlayer;
        EnterNextScene();
    }

    private void EnterNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void UpdateSensitivityPlayerPref()
    {
        PlayerPrefs.SetFloat(sensibilityKey,slider.value);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKey));
    }

    [Button(ButtonSizes.Large,ButtonStyle.FoldoutButton)]
    private void SetSliderTo(float value = 0.8f)
    {
        PlayerPrefs.SetFloat(sensibilityKey,value);
        slider.value = PlayerPrefs.GetFloat(sensibilityKey);
        Debug.Log("La sensisibilité est de " + PlayerPrefs.GetFloat(sensibilityKey));
    }
}
