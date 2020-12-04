using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour
{
    //AudioClip variables for different sounds of the game
    public static AudioClip mainMenuTheme, levelOneTheme,
        lazerGunSound, alienGunSound, lazerHitSound, mineralPickupSound, jumpSound,
        maxStep1, maxStep2, maxStep3, maxDeath,
        alienStep1, alienStep2, alienStep3, alienDeath;

    //Variable for AudioSource
    static AudioSource audioSrc;

    //Bool variable for pausing the music of the game
    public static bool musicPaused=false;

    void Start()
    {

        //Setting the AudioClip variables to right soundfiles from Resources
        mainMenuTheme = Resources.Load<AudioClip>("Mainmenu_theme");
        levelOneTheme = Resources.Load<AudioClip>("theme_01");

        lazerGunSound = Resources.Load<AudioClip>("lazerGun");
        alienGunSound = Resources.Load<AudioClip>("alienLazer");
        lazerHitSound = Resources.Load<AudioClip>("lazerHitSound");

        mineralPickupSound = Resources.Load<AudioClip>("mineral_collect");

        jumpSound = Resources.Load<AudioClip>("jump");

        maxStep1 = Resources.Load<AudioClip>("maxStep1");
        maxStep2 = Resources.Load<AudioClip>("maxStep2");
        maxStep3 = Resources.Load<AudioClip>("maxStep3");
        maxDeath = Resources.Load<AudioClip>("max_death");

        alienStep1 = Resources.Load<AudioClip>("alienStep1");
        alienStep2 = Resources.Load<AudioClip>("alienStep2");
        alienStep3 = Resources.Load<AudioClip>("alienStep3");
        alienDeath = Resources.Load<AudioClip>("alien_growl");

        //Setting the AudioSource variable to AudioSource Component
        audioSrc = GetComponent<AudioSource>();

    }

    void Update()
    {
        //Using SceneManager to configure which scene is active
        string scene = SceneManager.GetActiveScene().name;


        //Setting the right music for each level
        if (scene == "MainMenu" && !audioSrc.isPlaying)
        {
            PlaySound("mainMenu");
        }

        if ((scene == "Level1" || scene == "Level_2") && !audioSrc.isPlaying && !musicPaused)
        {
            PlaySound("theme01");
        }

        //Pauses AudioSource if musicPaused is set to true
        if (musicPaused)
        {
            audioSrc.Stop();
        }
    }

    //Public method for Playing different sounds of the game
    //the string clip is given to switch-clause and volumes are condigured here
    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "lazerGun": 
                audioSrc.PlayOneShot(lazerGunSound, 0.6f);
                break;
            case "alienLazer":
                audioSrc.PlayOneShot(alienGunSound, 0.6f);
                break;
            case "lazerHit":
                audioSrc.PlayOneShot(lazerHitSound);
                break;

            case "mineralCollect":
                audioSrc.PlayOneShot(mineralPickupSound, 0.7f);
                break;
            case "jump":
                audioSrc.PlayOneShot(jumpSound);
                break;

            case "maxStep1":
                audioSrc.PlayOneShot(maxStep1, 0.4f);
                break;
            case "maxStep2":
                audioSrc.PlayOneShot(maxStep2, 0.35f);
                break;
            case "maxStep3":
                audioSrc.PlayOneShot(maxStep3, 0.42f);
                break;
            case "maxDeath":
                audioSrc.PlayOneShot(maxDeath, 0.9f);
                break;

            case "alienStep1":
                audioSrc.PlayOneShot(alienStep1);
                break;
            case "alienStep2":
                audioSrc.PlayOneShot(alienStep2);
                break;
            case "alienStep3":
                audioSrc.PlayOneShot(alienStep3);
                break;
            case "alienDeath":
                audioSrc.PlayOneShot(alienDeath, 0.7f);
                break;

            case "mainMenu":
                audioSrc.PlayOneShot(mainMenuTheme);
                break;

            case "theme01":
                audioSrc.PlayOneShot(levelOneTheme, 0.9f);
                audioSrc.loop = true;
                break;
             }
        }

    //Methods for configuring the musicPaused bool
    public static void StopMusic()
    {
        musicPaused = true;
    }

    public static void ContinueMusic()
    {
        musicPaused = false;
    }
}
