using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeathTrigger : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag(PLAYER_TAG);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        // If player enters this trigger
        if (other.gameObject.tag == PLAYER_TAG)
        {
            // Trigger die function
            // which shows the death screen
            player.GetComponent<Player>().Die();
        }
    }

}
