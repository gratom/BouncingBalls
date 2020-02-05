using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingController : MonoBehaviour
{
#pragma warning disable
    [SerializeField] private SettingsScriptable DefaultSetting;
#pragma warning restore

    public Settings settings;
    private const string key = "settings";

    public void Init()
    {
        string s = PlayerPrefs.GetString(key);
        if (s != "")
        {
            settings = JsonUtility.FromJson<Settings>(s);
        }
        else
        {
            settings = DefaultSetting.settings;
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(settings));
    }
}