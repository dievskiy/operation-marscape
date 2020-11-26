using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    //Public variables for activating the different menus

    public GameObject pauseMenuCanvas;
    public GameObject deathMenuCanvas;

    //Bool variable for configuring wether or not the game is paused
    public static bool gamePaused=false;

    private Mineral mineral;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        mineral = gameObject.AddComponent<Mineral>();
        scoreText = GameObject.Find("DeathScoreText").GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        
        //scoreText.text = "Hello saatana";
        //Setting ESC and P-keys to trigger pause screen

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) 
        {
            if (gamePaused)
            {
                Continue();
            } else
            {
                Pause();
            }
        }

        //Setting L-key to trigger Deathscreen (placeholder)

        if (Input.GetKeyDown(KeyCode.L))
        {
            DeathScreen();

        }

    }

    //Method for pausing the game and activating the PauseMenuCanvas

    void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    //Method for deactivating the PauseMenuCanvas and continuing the game

    public void Continue()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    //Method for activating Deathscreen

    void DeathScreen()
    {
        if (!gamePaused)
        {
            deathMenuCanvas.SetActive(true);

            float score = mineral.GetInventory();
            scoreText.text = score + "t";

            Time.timeScale = 0f;
        }
    }

    //Method for going back to MainMenu

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    //Loads the level player has died on
    public void LoadThisLevel()
    {
        Continue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
