using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private void Start()
    {
        if (PlayerPrefs.GetString("SavedLevel") == null)
        {
            PlayerPrefs.SetString("SavedLevel", "MainMenu");
        }

    }
    public void BtnChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void BtnContinueScene()
    {
        string savedScene = PlayerPrefs.GetString("SavedLevel");
        SceneManager.LoadScene(savedScene);
    }
}
