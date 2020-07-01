using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem
{
    

    public static Language language = Language.French;
    private static Dictionary<Language, Dictionary<string, string>> localisedDictionaries = new Dictionary<Language, Dictionary<string, string>>();

    public static bool isInit;
    
    public static CSVLoader csvLoader;
    
    public static void Init()
    {
        localisedDictionaries.Add(Language.English,new Dictionary<string, string>());
        localisedDictionaries.Add(Language.French,new Dictionary<string, string>());
        
        csvLoader = new CSVLoader();
        csvLoader.LoadCSV();

        UpdateDictionnaries();

        isInit = true;
    }

    public static void UpdateDictionnaries()
    {
        localisedDictionaries[Language.English] = csvLoader.GetDictionaryValues("en");
        localisedDictionaries[Language.French] = csvLoader.GetDictionaryValues("fr");
    }

    public static Dictionary<string, string> GetDictionaryForEditor()
    {
        if (!isInit) Init();
        return localisedDictionaries[Language.French];
    }
    
    public static string GetLocalisedValue(string key)
    {
        if(!isInit) Init();

        string value = key;

        localisedDictionaries[language].TryGetValue(key, out value);

        return value;
    }

    public static void Add(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        
        csvLoader.LoadCSV();
        csvLoader.Add(key,value);
        csvLoader.LoadCSV();
        
        UpdateDictionnaries();
    }
    
    public static void Replace(string key, string value)
    {
        if (value.Contains("\""))
        {
            value.Replace('"', '\"');
        }

        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        
        csvLoader.LoadCSV();
        csvLoader.Edit(key,value);
        csvLoader.LoadCSV();
        
        UpdateDictionnaries();
    }

    public static void Remove(string key)
    {
        if (csvLoader == null)
        {
            csvLoader = new CSVLoader();
        }
        
        csvLoader.LoadCSV();
        csvLoader.Remove(key);
        csvLoader.LoadCSV();
        
        UpdateDictionnaries();
    }

    public static void SetMainLanguage(Language actualLanguage)
    {
        language = actualLanguage;
    }
}

public enum Language
{
    English,
    French
}
