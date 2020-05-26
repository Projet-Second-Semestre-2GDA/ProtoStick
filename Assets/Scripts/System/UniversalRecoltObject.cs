using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniversalRecoltObject
{
    
    public static string RecoltableBaseKey = "NombreRecoltableLevel";
    public static string TotalRecoltableBaseKey = "NombreTotalRecoltableLevel";


    public static void RaceOver(int levelNumber, int numberOfRecoltableCatch, int totalOfRecoltableInMaps)
    {
        var key = RecoltableBaseKey + levelNumber;
        var keyTwo = TotalRecoltableBaseKey + levelNumber;
        //Debug.LogWarning("Les clés sont : " + key + ", " + keyTwo);
        //Debug.LogWarning("Nombre de récoltable attrapé : " + numberOfRecoltableCatch);
        //Debug.LogWarning("Nombre de récoltable total dans la map : " + totalOfRecoltableInMaps);
        
        if (PlayerPrefs.HasKey(key))
        {
            if (PlayerPrefs.GetInt(key) <= numberOfRecoltableCatch)
            {
                PlayerPrefs.SetInt(key,numberOfRecoltableCatch);
            }
        }
        else
        {
            PlayerPrefs.SetInt(key,numberOfRecoltableCatch);
        }
        
        PlayerPrefs.SetInt(keyTwo,totalOfRecoltableInMaps);
    }
}
