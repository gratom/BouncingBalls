using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (Instance == null)
        {
            Instance = this;
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