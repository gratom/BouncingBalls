using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings config", order = 51)]
public class SettingsScriptable : ScriptableObject
{
    public Settings settings;
}

[System.Serializable]
public class Settings
{
    public AudioSetting audio;
    public PlayerMovementSetting playerMovement;
}

[System.Serializable]
public class AudioSetting
{
    public bool IsMusicPlay;
    public bool IsSoundPlay;
}

[System.Serializable]
public class PlayerMovementSetting
{
    public Vector2 ImpulsMutiplier;
    public float JumpForce;
}

[System.Serializable]
public class Sensitivity
{
    public float minSenc;
}