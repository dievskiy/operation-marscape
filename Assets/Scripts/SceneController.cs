using UnityEngine;
using UnityEngine.SceneManagement;



public class SceneController : MonoBehaviour
{
    public enum SceneType
    {
        LEVEL_1
    }

    public static void LoadScene(SceneType type){
        switch(type){
            case SceneType.LEVEL_1: SceneManager.LoadScene(0, LoadSceneMode.Single); break;
            default: break;
        }
        
    }
}

