using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public enum Language
{
    Chinese,
    English,
}
public class LanguageManager : BaseModule<LanguageManager>
{
    public Language curLanguage;
    private Language preLanguage;

    public Action OnLanguageChange;

    private string rootPath = "/Languages/";
    private Dictionary<string, string> languageDic;


    private void SetupLanguage(Language language)
    {
        languageDic.Clear();
        string jsonStr = FileUtils.ReadFile($"{Application.streamingAssetsPath}{rootPath}{curLanguage}/{curLanguage}.json");

        jsonStr = "{\"TotalItems\":" + jsonStr + "}";
        LanguageList list = JsonUtility.FromJson<LanguageList>(jsonStr);

        foreach (var item in list.TotalItems)
        {
            if (item.LanguageValue.Length > 0)
            {
                languageDic.Add(item.LanguageKey, item.LanguageValue);
            }
        }
        OnLanguageChange?.Invoke();
    }

    public string GetText(string key)
    {
        if (languageDic.ContainsKey(key))
        {
            if (languageDic[key].Length > 0)
            {
                return languageDic[key];
            }
            else
            {
                return key;
            }
        }
        return key;
    }


    public override void Init()
    {
        base.Init();
        languageDic = new Dictionary<string, string>();
       
        SetupLanguage(AppConst.DefaultLanguage);
    }

    public override void Update()
    {
        base.Update();
        if (preLanguage != curLanguage)
        {
            SetupLanguage(curLanguage);
            preLanguage = curLanguage;
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        languageDic.Clear();
    }



    [Serializable]
    public class LanguageItem
    {
        public string LanguageKey;
        public string LanguageValue;
    }
    [Serializable]
    public class LanguageList
    {
        public LanguageItem[] TotalItems;
    }
}
