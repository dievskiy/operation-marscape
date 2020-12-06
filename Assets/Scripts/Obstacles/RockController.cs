using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class controls rocks falling 
public class RockController : MonoBehaviour {

    private GameObject[] rocks;
    // list of bools corresponding to each rock in the same order
    // if rock has been fallen - fallen[i] will be true 
    private List<bool> fallen = new List<bool>();
    private const float fallRange = 17f;

    private GameObject player;
    void Start () {
        player = GameObject.FindWithTag ("Player");

        rocks = GameObject.FindGameObjectsWithTag("Rock");
        // no rock has been fallen at the start
        if (rocks.Length > 0) {
            for (int i = 0; i < rocks.Length; i++) {
                fallen.Add(false);
            }
        }
    }

    void Update () {
        for (int i = 0; i < rocks.Length; i++) {
            // return if rock has already been fallen
            if (fallen[i]) return;
            // otherwise check if it's near the player
            if (rocks[i].transform.position.x - player.transform.position.x < fallRange) {
                rocks[i].GetComponent<Rigidbody>().isKinematic = false;
                fallen[i] = true;
            }
        }
    }
}