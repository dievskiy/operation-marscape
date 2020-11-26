using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;
using static EnemyMovementController;

public class EnemyFirstLevelController : MonoBehaviour
{
    private EnemyModel model;
    private EnemyMovementController movementController;
    public GameObject bullet;
    private float lastShoot;
    private float shootFrequency = 3f;
    private Animator anim;
    private bool isDying = false;

    void Start()
    {
        model = new EnemyFirstLevelModel(); 
        movementController = gameObject.GetComponent<EnemyMovementController>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // approach player if its far away
        if(movementController.isNearPlayer && !isDying)
        {
            if(Time.time - shootFrequency > lastShoot)
            {
                startShooting();
            }
        }
    }

    private void startShooting()
    {
        anim.SetBool("isShooting", true);
        lastShoot = Time.time;
        var bulletPos = transform.position;
        // todo: create bullet spawn gameobject
        bulletPos.y += .8f;
        bulletPos.x += .8f;
        Instantiate(bullet, bulletPos, Quaternion.identity);
        // stop animation programmatically because default animatoin consists of 3 shoots.
        StartCoroutine(StopAnim());
    }
    
    IEnumerator StopAnim()
    {
        yield return new WaitForSeconds(0.5f);
        if(!isDying)
        {
            anim.SetBool("isShooting", false);
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            isDying = true;
            anim.SetTrigger("Die");
            StartCoroutine(Die());
        }
        
    }
}
