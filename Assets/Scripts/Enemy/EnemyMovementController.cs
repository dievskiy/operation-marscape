using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementController : MonoBehaviour
{

    public Vector3[] navigationPositions;
    public float speed = 0.07f;
    private int currentTarget; 


    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, navigationPositions[currentTarget], speed);

        // when target position is reached, switch to another one
        if (transform.position == navigationPositions[currentTarget])
        {
            if (currentTarget < navigationPositions.Length - 1) currentTarget ++;
            else currentTarget = 0;
        }
    }
}
