using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public SettingController settingController;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
            settingController.Init();
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadingCoroutine(2));
        }
        else
        {
            Debug.Log("WOW, another GameManager!");
        }
    }

    private IEnumerator LoadingCoroutine(int time)
    {
        yield return new WaitForSeconds(time);
        SceneLoader.LoadScene(1);
    }
}