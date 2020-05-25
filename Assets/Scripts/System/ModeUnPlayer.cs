using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public enum PlayerMode
{
    OnePlayer,
    TwoPlayer
}
public class ModeUnPlayer : MonoBehaviour
{
    
    [Title("Player Two Component")]
    [SerializeField,FormerlySerializedAs("gameObjectsADesactiver")]private GameObject playerTwo;
    [Title("Player One Component")]
    [SerializeField] private GameObject playerOne;
    public RectTransform UpRef;

    [TitleGroup("HideInTwoPlayer")] [SerializeField]
    private List<GameObject> objectToDisable = new List<GameObject>();
    
    [Title("ChoosingMode")]
    public bool modeUnJoueur;
    
    //Private !!
    private Camera cameraAModifier;
    private void Start()
    {
        cameraAModifier = playerOne.GetComponentInChildren<Camera>();
        
        SetMode(PlayerOptionChoose.ModeDeJoueur);
    }

    private void Update()
    {
        if (modeUnJoueur)//Mode un joueur
        {
            playerTwo.SetActive(false);
            
            cameraAModifier.rect = new Rect(0,0,1,1);
            cameraAModifier.fieldOfView = 100;
            ChangeViseurPosition(1080);
            
            foreach (var obj in objectToDisable)
            {
                obj.SetActive(true);
            }
            
        }
        else//Mode Deux Joueurs
        {
            playerTwo.SetActive(true);
            cameraAModifier.rect = new Rect(0,0.5f,1,0.5f);
            cameraAModifier.fieldOfView = 60;

            foreach (var obj in objectToDisable)
            {
                obj.SetActive(false);
            }
            
            ChangeViseurPosition(540);

        }
    }

    private void ChangeViseurPosition(float Y)
    {
        var pos = UpRef.sizeDelta;
        pos.y = Y;
        UpRef.sizeDelta = pos;
    }

    public void SetMode(PlayerMode playerMode)
    {
        switch (playerMode)
        {
            case PlayerMode.OnePlayer:
                modeUnJoueur = true;
                break;
            case PlayerMode.TwoPlayer:
                modeUnJoueur = false;
                break;
            default:
                modeUnJoueur = false;
                break;
                    
        }
    }
    
}
