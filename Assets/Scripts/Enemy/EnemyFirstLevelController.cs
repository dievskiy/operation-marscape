using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;

public class EnemyFirstLevelController : MonoBehaviour
{
    private EnemyModel model;

    // Start is called before the first frame update
    void Start()
    {
        model = new EnemyFirstLevelModel();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") 
        {
            // todo attack player. pass player model object to EnemyFirstLevelModel attackPlayer method
            // model.AttackPlayer(player)
        }
    }
}
