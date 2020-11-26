using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyModel
{
    protected float hp;
    protected float attackValue;

    public abstract void AttackPlayer();

}
