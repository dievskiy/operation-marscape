using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneHandler : MonoBehaviour
{
    
    public enum Cutscenes {
        INTRO,
        OUTRO
    }

    public Cutscenes currentCutscene;
    // Start is called before the first frame update
    private VideoPlayer player;


    void Start()
    {
        player = GetComponent<VideoPlayer>();

        if (currentCutscene == Cutscenes.INTRO)
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.m4v");

        if (currentCutscene == Cutscenes.OUTRO)
            player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "outro.m4v");

        player.Play();
        PlayerPrefs.SetString("SavedLevel", "Level1");
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            if (currentCutscene == Cutscenes.INTRO)
                SceneController.LoadScene(SceneController.SceneType.LEVEL_1);

            if (currentCutscene == Cutscenes.OUTRO)
                SceneController.LoadScene(SceneController.SceneType.END_SCREEN);
        }


        if (player.length > 0 && player.time >= player.length - 1)
        {
            if (currentCutscene == Cutscenes.INTRO)
                SceneController.LoadScene(SceneController.SceneType.LEVEL_1);

            if (currentCutscene == Cutscenes.OUTRO)
                SceneController.LoadScene(SceneController.SceneType.END_SCREEN);
        }
    }
}
