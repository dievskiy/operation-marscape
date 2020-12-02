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
    }

    // Update is called once per frame
    void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape)) || (player.time >= player.length))
        {
            SceneController.LoadScene(SceneController.SceneType.LEVEL_1);
        }
    }
}
