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
    private float shootFrequency = 1f;
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
        // get position of current object's gun
        var bulletPos = transform.GetChild(0).Find("Box001").transform.position;
        bulletPos.y += 1.4f;
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
        // set all coliders to trigger after death so user can pass through it
        Collider[] colList = transform.GetComponentsInChildren<Collider>();
        foreach(Collider col in colList)
        {
            col.isTrigger = true;
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        // should be tag comparison
        if(other.gameObject.name.ToLower().Contains("bullet") && other.tag != "EnemyBullet")
        {
            isDying = true;
            anim.SetTrigger("Die");
            StartCoroutine(Die());
        }
        
    }
}
