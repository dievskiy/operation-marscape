using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface EnemyFactory
{
    // Instantiates an Enemy based on player position
    void StartInstantiation(GameObject player);
}
