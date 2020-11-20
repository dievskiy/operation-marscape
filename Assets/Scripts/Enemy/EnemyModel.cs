using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyModel
{
    protected float hp;

    public abstract void AttackPlayer();

    public abstract void Die();

}
