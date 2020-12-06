using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyModel {
    protected float hp;
    // this is damage that players gets when touching an enemy 
    protected float attackValue;
    protected bool isDead;

    private void Die () {
        isDead = true;
    }

    public bool IsDead () {
        return isDead;
    }

    public float GetDamage () {
        return attackValue;
    }

    public float GetHp () {
        return hp;
    }

     // returns 0 if enemy has died, 1 otherwise
    public int Attack (float amount) {
        if (amount >= hp) {
            hp = 0f;
            Die();
            return 0;
        }
        hp -= amount;
        return 1;
    }
}