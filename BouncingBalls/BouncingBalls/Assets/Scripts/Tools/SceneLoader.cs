using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SceneLoader
{
    public static void LoadScene(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index);
    }
}