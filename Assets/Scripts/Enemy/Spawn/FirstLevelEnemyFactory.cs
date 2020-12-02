using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyFactory;

public class FirstLevelEnemyFactory : MonoBehaviour, EnemyFactory
{
    public GameObject enemy;
    private GameObject[] spawnPoints;

    public void StartInstantiation(GameObject player)
    {
        if (!enemy) return;
        CreateEnemies(player);
    }

    private void CreateEnemies(GameObject player)
    {
        // spawn enemies on predefined spawnObjects in scene
        foreach(GameObject spawn in spawnPoints)
        {
            var pos = spawn.transform.position;
            if (pos == null) return;
            Instantiate(enemy, pos, Quaternion.identity);
        }
        
    }

    void Awake()
    {
        // find needed enemy prefab
        enemy = (GameObject)Resources.Load("prefabs/EnemyFirst", typeof(GameObject));
        // find all spawnPoints. EnemySpawn tag is same for all levels
        spawnPoints = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

}
