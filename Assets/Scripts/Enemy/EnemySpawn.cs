﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private GameObject player;
    private EnemyFactory factory;

    public int level = 1;

    void Start()
    {
        player = GameObject.Find("Player");

        // create factory for current level
        switch(level)
        {
            case 1: 
                factory = gameObject.AddComponent<FirstLevelEnemyFactory>();
                break;
            default:
                break;
        }
        if(player != null && factory != null)
        {
            factory.StartInstantiation(player);
        }
    }

}