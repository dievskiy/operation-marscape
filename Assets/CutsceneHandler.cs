using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutsceneHandler : MonoBehaviour
{
    // Start is called before the first frame update
    private VideoPlayer player;

    void Start()
    {
        player = GetComponent<VideoPlayer>();
        player.url = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.m4v");
        player.Play();
        PlayerPrefs.SetString("SavedLevel", "Level1");
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)))
        {
            SceneController.LoadScene(SceneController.SceneType.LEVEL_1);
        }


        if (player.length > 0 && player.time >= player.length - 1)
        {
            SceneController.LoadScene(SceneController.SceneType.LEVEL_1);
        }
    }
}
