using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{
    public enum SceneType
    {
        MAIN_MENU,
        CUTSCENE,
        LEVEL_1
    }

    public static void LoadScene(SceneType type){
        switch(type){
            case SceneType.CUTSCENE: SceneManager.LoadScene("IntroCutscene", LoadSceneMode.Single); break;
            case SceneType.LEVEL_1: SceneManager.LoadScene("TestScene_1", LoadSceneMode.Single); break;
            default: break;
        }
        
    }
}

