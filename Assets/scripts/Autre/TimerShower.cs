using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerShower : MonoBehaviour
{
    [Title("NeedElement")]
    [SerializeField] private Text timeShower;

    [TitleGroup("Parameters")] 
    private int numberAfterComma = 4;
    [HideInInspector]public float timer;
    private bool isOver = false;

    private GlobalTimer globalTimer;
    
    private void Start()
    {
        globalTimer = GameObject.FindGameObjectWithTag("Gestionnaire").GetComponent<GlobalTimer>();
    }

    private void Update()
    {

        if (!isOver)
        {
            UpdateChrono();
        }

        int minute = Mathf.FloorToInt(timer/60);
        float seconde = (timer - minute * 60);
        seconde = LeadboardSetter.RoundValue(seconde, Mathf.Pow(10, numberAfterComma));
        
        //Affichage des minutes avec un nombre fixe
        string realMin = (minute < 10) ? "0" + minute : minute.ToString();
        
        //Afficher les secondes avec un nombre fixe de nombres
        string realSec = seconde.ToString();
        Char comma = ',';
        List<Char> PostComma = new List<char>();
        List<Char> PreComma = new List<char>();
        
        bool boolComma = false;
        
        for (int i = 0; i < realSec.Length; i++) //Mise des characters dans les Listes
        {
            var activeChar = realSec[i];
            if (activeChar == comma) boolComma = true;
            else (!boolComma?PreComma:PostComma).Add(activeChar);
        }
        
        if (!boolComma) //S'il n'y a pas de chiffre après la virgule
        {
            char[] firstNumbers = (PreComma.Count < 2)? new char[]{'0', PreComma[0]}:PreComma.ToArray();//Pour les nombres avant la virgules
            char[] lastChars = new[] {',', '0', '0', '0', '0'};
            
            
            int originalLenght = firstNumbers.Length;
            Array.Resize(ref firstNumbers, originalLenght + lastChars.Length);
            Array.Copy(lastChars, 0, firstNumbers, originalLenght, lastChars.Length);
            realSec = new string(firstNumbers);
        }
        else
        {
            char[] firstNumbers = (PreComma.Count < 2)? new char[]{'0', PreComma[0]}:PreComma.ToArray();//Pour les nombres avant la virgules
            char[] lastChars = new char[5];
            
            //Pour les nombres après la virgule
            char[] realPostComma;
            if(((lastChars.Length-1) + PostComma.Count)>0)
            {
                char[] need = new char[(lastChars.Length-1) + PostComma.Count];
                for (int i = 0; i < need.Length; i++)
                {
                    need[i] = '0';
                }
                //First part (creation d'un Array qui contient ce qu'il y a après la virgule)
                realPostComma = new char[PostComma.ToArray().Length + need.Length];
                Array.Copy(PostComma.ToArray(), realPostComma, PostComma.ToArray().Length);
                Array.Copy(need, 0, realPostComma, PostComma.ToArray().Length, need.Length);
                
            }
            else
            {
                realPostComma = PostComma.ToArray();
            }
            //Second Part (remplissage de la partie v + chiffres)
            lastChars[0] = comma;
            for (int i = 1; i < lastChars.Length; i++)
            {
                lastChars[i] = realPostComma[i - 1];
            }
            //Last Part (création des secondes complète)
            int originalLenght = firstNumbers.Length;
            Array.Resize(ref firstNumbers, originalLenght + lastChars.Length);
            Array.Copy(lastChars, 0, firstNumbers, originalLenght, lastChars.Length);
            realSec = new string(firstNumbers);
            
            
        }
        
        //Fin de l'affichage
        timeShower.text = (minute < 60)
            ? realMin + " min " + realSec
            : "Trop Long";
    }

    private void UpdateChrono()
    {
        timer = globalTimer.GetTime();
    }

    public float GetTimer()
    {
        return timer;
    }

    public void raceEnd()
    {
        isOver = true;
        int levelID = SceneManager.GetActiveScene().buildIndex;
        Debug.LogWarning("L'ID est : " + levelID);
        Debug.LogWarning("Duration : " + timer);
        LeadboardSetter.LevelFinish(levelID,timer);
    }
}

