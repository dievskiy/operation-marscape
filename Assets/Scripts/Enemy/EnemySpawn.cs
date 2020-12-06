using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class handles enemy spawning
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
            case 2:
                factory = gameObject.AddComponent<SecondLevelEnemyFactory>();
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
