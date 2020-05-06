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
    [Title("Sliders Reference")]
    public Slider sliderJ1;
    public Slider sliderJ2;

    private string sensibilityKeyJ1 = "Sensibilitee1";
    private string sensibilityKeyJ2 = "Sensibilitee2";

    private void Awake()
    {
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
}
