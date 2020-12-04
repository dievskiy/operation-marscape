using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Script for MainMenu for selecting new and saved levels
    private void Start()
    {
        //If player starts the game and Palyerprefs doesn't have a level saved
        //the default for Continue-option will be MainMenu 
        if (PlayerPrefs.GetString("SavedLevel") == null)
        {
            PlayerPrefs.SetString("SavedLevel", "MainMenu");
        }

    }

    //Loads the scene
    public void BtnChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Loads the saved level from PlayerPrefs
    public void BtnContinueScene()
    {
        string savedScene = PlayerPrefs.GetString("SavedLevel");
        SceneManager.LoadScene(savedScene);
    }
}
