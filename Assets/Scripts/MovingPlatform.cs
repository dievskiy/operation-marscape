using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this code is partly from unity docs
public class MovingPlatform : MonoBehaviour {
    public Transform endMarker;
    private Transform player;
    public bool isMoving = false;
    float smoothTime = 3f;
    float xVelocity = 0.0f;

    void Start () {
        player = GameObject.FindWithTag ("Player").transform;
    }

    // todo: refactor to OnTriggerEnter
    void Update () {
        if (!isMoving && (transform.position.x - player.position.x < .01f) && (transform.position.y - player.position.y > 7.2f)) {
            isMoving = true;
            return;
        }

        if (!isMoving) return;

        float newPosition = Mathf.SmoothDamp (transform.position.x, endMarker.position.x, ref xVelocity, smoothTime);
        transform.position = new Vector3 (newPosition, transform.position.y, transform.position.z);

    }

}