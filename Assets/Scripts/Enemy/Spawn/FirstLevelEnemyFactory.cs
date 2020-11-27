using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyFactory;

public class FirstLevelEnemyFactory : MonoBehaviour, EnemyFactory
{
    public GameObject enemy;

    public void StartInstantiation(GameObject player)
    {
        if (!enemy) return;
        StartCoroutine(CreateEnemy(player));
    }

    IEnumerator CreateEnemy(GameObject player)
    {
        yield return new WaitForSeconds(2f);
        var pos = player.transform.position;
        // define x position randomly
        pos.x += Random.Range(10.0f, 30.0f);
        Instantiate(enemy, pos, Quaternion.identity);
        StartCoroutine(CreateEnemy(player));
    }
    // Start is called before the first frame update
    void Awake()
    {
        // find needed enemy prefab
        enemy = (GameObject)Resources.Load("prefabs/EnemyFirst", typeof(GameObject));
    }

}
