using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{
    public enum SceneType
    {
        LEVEL_1,
        LEVEL_2

    }

    public static void LoadScene(SceneType type){
        switch(type){
            case SceneType.LEVEL_1: SceneManager.LoadScene(0, LoadSceneMode.Single); break;
            case SceneType.LEVEL_2: SceneManager.LoadScene("MainMenu", LoadSceneMode.Single); break; // no level 2 yet, use main menu for testing
            default: break;
        }
        
    }
}

