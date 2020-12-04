using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;

interface EnemyLevelController {
    bool HasDied ();
    float GetDamage();
}