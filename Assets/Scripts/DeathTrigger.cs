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
        if (other.gameObject.tag == PLAYER_TAG)
        {
            Debug.Log("death");
            player.GetComponent<Player>().Die();
        }
    }

}
