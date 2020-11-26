using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float projectileSpeed = 5;
    public float maxDistance = 15;
    // Start is called before the first frame update
    void Start()
    {
        
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

        // move projectile to right
        transform.Translate(transform.right * projectileSpeed * Time.deltaTime);

    }


    void OnTriggerEnter(Collider other)
    {
        // add bullet collision stuff here

        Destroy(gameObject);
    }
}
