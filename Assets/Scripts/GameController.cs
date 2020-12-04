using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{

    public SceneController.SceneType nextLevel;

    //Public variables for activating the different menus

    public GameObject pauseMenuCanvas;
    public GameObject deathMenuCanvas;
    public GameObject levelCompleteCanvas;

    //Bool variable for configuring wether or not the game is paused
    public static bool gamePaused=false;

    //private Mineral mineral;
    public int mineralsInLevel;
    public float mineralCount;
    public float totalCollectedMinerals = 0;
    public static GameController current;

    //Variable for TextMeshPro for setting player score to DeathScreen
    public TextMeshProUGUI deathScoreText;

    [Header("Victory screen")]
    public TextMeshProUGUI victoryScoreText;
    public TextMeshProUGUI mineralsCollectedText;

    private bool playerDead;

    void Start()
    {
        current = this;
        mineralsInLevel = GameObject.FindGameObjectsWithTag("Collectable").Length;

        // had some issues where finish level was visible when starting new game
        // this should fix it
        //
        // it disables all the pause, death, and level complete screens on scene load
        ResetLevel();
        playerDead = false;
    }

    void ResetLevel()
    {
        totalCollectedMinerals = 0;
        gamePaused = false;
        Time.timeScale = 1f;
        pauseMenuCanvas.SetActive(false);
        deathMenuCanvas.SetActive(false);
        levelCompleteCanvas.SetActive(false);
    }

    void Update()
    {

        //Setting ESC and P-keys to trigger pause screen

        if (!playerDead)
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
            {
                if (gamePaused)
                {
                    Continue();
                }
                else
                {
                    Pause();
                }
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
        SoundManagerScript.StopMusic();
    }

    //Method for deactivating the PauseMenuCanvas and continuing the game

    public void Continue()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        SoundManagerScript.ContinueMusic();
    }

    //Method for activating Deathscreen if game isn't paused and setting the Score

    public void DeathScreen()
    {
        if (!gamePaused)
        {
            deathMenuCanvas.SetActive(true);
            deathScoreText.text = "Score : " + mineralCount.ToString() + " minerals";
            playerDead = true;
            Time.timeScale = 0f;
        }
    }

    //Method for activating LevelCompleteCanvas and setting scores to it
    //Also saves the second level to PlayerPrefs if the player wants to continue playing later

    public void CompleteLevel()
    {
        levelCompleteCanvas.SetActive(true);
        gamePaused = true;
        victoryScoreText.text = "SCORE: " + mineralCount.ToString() + " MINERALS";
        mineralsCollectedText.text = "MINERALS COLLECTED: " + totalCollectedMinerals.ToString() + "/" + mineralsInLevel.ToString();
        Time.timeScale = 0f;
        PlayerPrefs.SetString("SavedLevel", "Level2");
    }

    //Method for going back to MainMenu

    public void LoadMenu()
    {
        Continue();
        SceneManager.LoadScene("MainMenu");
    }


    //Loads the level player has died on
    public void LoadThisLevel()
    {
        Continue();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    //Loads next level
    public void LoadNextLevel()
    {
        gamePaused = false;
        Time.timeScale = 1f;
        SceneController.LoadScene(nextLevel);
    }
}
