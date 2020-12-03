using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel {
    private float hp;

    public PlayerModel () {
        hp = 100f;
    }

    public float GetHp () {
        return hp;
    }

    public void Heal (float amount) {
        if (amount > 0)
            hp += amount;
        if (hp > 100) {
            hp = 100f;
        }
    }

    // returns 0 if player has died, 1 otherwise
    public int Damage (float amount) {
        if (amount >= hp) {
            hp = 0f;
            return 0;
        }
        hp -= amount;
        return 1;
    }
}