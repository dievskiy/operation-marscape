﻿using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{
    public enum SceneType
    {
        MAIN_MENU,
        CUTSCENE,
        LEVEL_1,
        LEVEL_2,
        OUTRO,
        END_SCREEN

    }

    public static void LoadScene(SceneType type){
        switch(type){
            case SceneType.MAIN_MENU: SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); break; // no level 2 yet, use main menu for testing
            case SceneType.CUTSCENE: SceneManager.LoadScene("IntroCutscene", LoadSceneMode.Single); break;
            case SceneType.LEVEL_1: SceneManager.LoadScene("Level1", LoadSceneMode.Single); break;
            case SceneType.LEVEL_2: SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); break; // no level 2 yet, use main menu for testing
            case SceneType.OUTRO: SceneManager.LoadScene("OutroCutscene", LoadSceneMode.Single); break;
            case SceneType.END_SCREEN: SceneManager.LoadScene("Endscreen", LoadSceneMode.Single); break;
            default: break;
        }
        
    }
}

