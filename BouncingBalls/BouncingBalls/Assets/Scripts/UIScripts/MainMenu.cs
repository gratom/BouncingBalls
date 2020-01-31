using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region scene buttons

#pragma warning disable
    [SerializeField] private Button StartButton;
    [SerializeField] private Button SettingButton;

#pragma warning restore

    #endregion scene buttons

    #region UnityFunctions

    private void Awake()
    {
        Init();
    }

    private void OnValidate()
    {
        Debug.Assert(StartButton != null, "StartButton is null!");
        Debug.Assert(SettingButton != null, "SettingButton is null!");
    }

    #endregion UnityFunctions

    #region private functions

    private void Init()
    {
        SettingForButtons();
    }

    private void SettingForButtons()
    {
        StartButton.onClick.AddListener(StartGameButtonClick);
        SettingButton.onClick.AddListener(SettingButtonClick);
    }

    #endregion private functions

    #region scene buttons functions

    private void StartGameButtonClick()
    {
        SceneLoader.LoadScene(2);
    }

    private void SettingButtonClick()
    {
        Debug.Log("Setting");
    }

    #endregion scene buttons functions
}