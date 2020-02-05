using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    #region scene buttons

#pragma warning disable

    [SerializeField] private GameObject GamePanel;
    [SerializeField] private Button StartButton;
    [SerializeField] private Button SettingButton;

    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private Button SaveSettingButton;
    [SerializeField] private Button BackToGameMenuButton;

    [SerializeField] private Slider JumpForceSlider;
    [SerializeField] private Text JumpForceText;
    [SerializeField] private Slider XForceSlider;
    [SerializeField] private Text XForceText;
    [SerializeField] private Slider YForceSlider;
    [SerializeField] private Text YForceText;

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
        Debug.Assert(GamePanel != null, "GamePanel is null!");
        Debug.Assert(SettingPanel != null, "SettingPanel is null!");
        Debug.Assert(SaveSettingButton != null, "SaveSettingButton is null!");
        //todo переписать всю менюшку по нормальному
    }

    #endregion UnityFunctions

    #region private functions

    private void Init()
    {
        SettingForButtons();
        UpdateSettings();
    }

    private void SettingForButtons()
    {
        StartButton.onClick.AddListener(StartGameButtonClick);
        SettingButton.onClick.AddListener(SettingButtonClick);
        BackToGameMenuButton.onClick.AddListener(BackToGameMenuButtonClick);
        SaveSettingButton.onClick.AddListener(SaveSettingButtonClick);

        JumpForceSlider.onValueChanged.AddListener(JumpForceChanged);
        XForceSlider.onValueChanged.AddListener(XforceChange);
        YForceSlider.onValueChanged.AddListener(YforceChange);
    }

    private void UpdateSettings()
    {
        if (GameManager.Instance != null)
        {
            JumpForceSlider.value = GameManager.Instance.settingController.settings.playerMovement.JumpForce;
            JumpForceText.text = "Jump force : " + JumpForceSlider.value;
            XForceSlider.value = GameManager.Instance.settingController.settings.playerMovement.ImpulsMutiplier.x;
            XForceText.text = "X movement : " + XForceSlider.value;
            YForceSlider.value = GameManager.Instance.settingController.settings.playerMovement.ImpulsMutiplier.y;
            YForceText.text = "Y movement : " + YForceSlider.value;
        }
    }

    #endregion private functions

    #region scene buttons functions

    private void StartGameButtonClick()
    {
        SceneLoader.LoadScene(2);
    }

    private void SettingButtonClick()
    {
        SettingPanel.SetActive(true);
        GamePanel.SetActive(false);
    }

    private void SaveSettingButtonClick()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.settingController.Save();
        }
    }

    private void BackToGameMenuButtonClick()
    {
        GamePanel.SetActive(true);
        SettingPanel.SetActive(false);
    }

    private void JumpForceChanged(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.settingController.settings.playerMovement.JumpForce = value;
            JumpForceText.text = "Jump force : " + JumpForceSlider.value;
        }
    }

    private void XforceChange(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.settingController.settings.playerMovement.ImpulsMutiplier.x = value;
            XForceText.text = "X movement : " + XForceSlider.value;
        }
    }

    private void YforceChange(float value)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.settingController.settings.playerMovement.ImpulsMutiplier.y = value;
            YForceText.text = "Y movement : " + YForceSlider.value;
        }
    }

    #endregion scene buttons functions
}