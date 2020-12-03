﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;

public class EnemyFirstLevelController : MonoBehaviour
{
    private EnemyModel model;
    public GameObject bullet;
    private float lastShoot;
    private float shootFrequency = 1f;
    private Animator anim;
    private bool isDying = false;

    private Vector3 target;
    private Rigidbody rigidbody;
    private Transform player;
    public float speed = 0.07f;
    public bool isNearPlayer = false;
    public float shootingRange = 10f;
    // this is minimum position diff, whn enemy "activates" (starts moving)
    public float movingRange = 20f;

    float elapsed = 0f;

    void Start()
    {
        model = new EnemyFirstLevelModel(); 
        anim = GetComponentInChildren<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }

    void FixedUpdate()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > movingRange)
        {
            return;
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

            // loop for playing alien step sounds every 0.25 seconds
            elapsed += Time.deltaTime;
            if (elapsed >= 0.25f)
            {
                elapsed = elapsed % 0.25f;
                RunSound();
            }
        }
    }

    void Update()
    {
        // approach player if its far away
        if(isNearPlayer && !isDying)
        {
            if(Time.time - shootFrequency > lastShoot)
            {
                startShooting();
            }
        }

    }

    // runsound method for playing 3 different alien step sounds
    void RunSound()
    {
        int stepSoundRandom = Random.Range(1, 4);
        SoundManagerScript.PlaySound("alienStep" + stepSoundRandom.ToString());
    }

    private void startShooting()
    {
        anim.SetBool("isShooting", true);
        lastShoot = Time.time;
        // get position of current object's gun
        var bulletPos = transform.GetChild(0).Find("Box001").transform.position;
        bulletPos.y += 1.4f;
        Instantiate(bullet, bulletPos, Quaternion.identity);
        SoundManagerScript.PlaySound("alienLazer");
        // stop animation programmatically because default animatoin consists of 3 shoots.
        StartCoroutine(StopAnim());
    }
    
    private IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(0.5f);
        if(!isDying)
        {
            anim.SetBool("isShooting", false);
        }
    }

    private IEnumerator Die()
    {
        // set all coliders to trigger after death so user can pass through it
        Collider[] colList = transform.GetComponentsInChildren<Collider>();
        foreach(Collider col in colList)
        {
            col.isTrigger = true;
        }
        // let death anim finish
        SoundManagerScript.PlaySound("alienDeath");
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        // should be tag comparison
        if(!isDying && other.gameObject.name.ToLower().Contains("bullet") && other.tag != "EnemyBullet")
        {
            isDying = true;
            anim.SetTrigger("Die");
            StartCoroutine(Die());
        }
        
    }
}
