using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    public float projectileSpeed = 7f;
    public float maxDistance = 55f;
    private Transform target;
    private Vector3 direction;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        direction = (target.position - transform.position).normalized;
        
    }

    // Update is called once per frame
    void Update()
    {
        // if the distance between camera and the bullet grows too big, remove the bullet
        float distance = Vector2.Distance(transform.position, Camera.main.transform.position);
        if (distance > maxDistance)
        {
            Destroy(gameObject);
        }
        transform.position += direction * projectileSpeed * Time.deltaTime;
    }


    void OnTriggerEnter(Collider other)
    {
        // add bullet collision stuff here

        Destroy(gameObject);
    }
}
