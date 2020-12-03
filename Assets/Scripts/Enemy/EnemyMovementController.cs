﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{
    private Vector3 target;
    private Rigidbody rigidbody;
    private Transform player;
    public float speed = 0.07f;
    public bool isNearPlayer = false;
    public float shootingRange = 10f;
    public float livingRange = 140f;
    

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }


    void FixedUpdate()
    {
        // destroy enemy if it is far from player
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > livingRange)
        {
            Destroy(gameObject);
        }

        transform.LookAt(player);

        target = player.position;
        // when enemy is in shooting range, stop moving and start shooting
        if ((Mathf.Abs(transform.position.x - target.x)) < shootingRange)
        {
            isNearPlayer = true;
        }
        else
        {
            isNearPlayer = false;
            transform.position = Vector3.MoveTowards(transform.position, target, speed);
        }
    }
}