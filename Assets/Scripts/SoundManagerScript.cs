using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip mainMenuTheme, levelOneTheme,
        lazerGunSound, alienGunSound, mineralPickupSound, jumpSound,
        maxStep1, maxStep2, maxStep3,
        alienStep1, alienStep2, alienStep3;

    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuTheme = Resources.Load<AudioClip>("Mainmenu_theme");
        levelOneTheme = Resources.Load<AudioClip>("theme_01");

        lazerGunSound = Resources.Load<AudioClip>("lazerGun");
        alienGunSound = Resources.Load<AudioClip>("alienLazer");

        mineralPickupSound = Resources.Load<AudioClip>("mineral_collect");
        jumpSound = Resources.Load<AudioClip>("jump");

        maxStep1 = Resources.Load<AudioClip>("maxStep1");
        maxStep2 = Resources.Load<AudioClip>("maxStep2");
        maxStep3 = Resources.Load<AudioClip>("maxStep3");

        alienStep1 = Resources.Load<AudioClip>("alienStep1");
        alienStep2 = Resources.Load<AudioClip>("alienStep2");
        alienStep3 = Resources.Load<AudioClip>("alienStep3");

        audioSrc = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (scene == "MainMenu" && !audioSrc.isPlaying)
        {
            PlaySound("mainMenu");
        }

        if (scene == "TestScene_1" && !audioSrc.isPlaying)
        {
            PlaySound("theme01");
        }
    }

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "lazerGun": 
                audioSrc.PlayOneShot(lazerGunSound, 0.6f);
                break;
            case "alienLazer":
                audioSrc.PlayOneShot(alienGunSound);
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

            case "alienStep1":
                audioSrc.PlayOneShot(alienStep1);
                break;
            case "alienStep2":
                audioSrc.PlayOneShot(alienStep2);
                break;
            case "alienStep3":
                audioSrc.PlayOneShot(alienStep3);
                break;

            case "mainMenu":
                audioSrc.PlayOneShot(mainMenuTheme);
                break;

            case "theme01":
                audioSrc.PlayOneShot(levelOneTheme, 0.9f);
                break;
        }
    }
}
