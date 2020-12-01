using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;
using static EnemyMovementController;

public class EnemySecondLevelController : MonoBehaviour
{
    private EnemyModel model;
    private EnemyMovementController movementController;
    public GameObject bullet;
    public Transform bulletPosition;
    private float lastShoot;
    private float shootFrequency = 3f;
    private Actions anim;
    private bool isDying = false;
    private bool isShooting = false;

    void Start()
    {
        model = new EnemyFirstLevelModel(); 
        movementController = gameObject.GetComponent<EnemyMovementController>();
        anim = GetComponent<Actions>();
    }

    void Update()
    {
        // approach player if its far away
        if(movementController.isNearPlayer && !isDying)
        {
            if(Time.time - shootFrequency > lastShoot)
            {
                // if (anim.IsCurrentlyMoving())
                // {
                    
                // }

                if(!isShooting) startShooting();
            }
        }
        else
        {
            anim.Run();
            
        }
    }

    private IEnumerator Fire()
    {
        yield return new WaitForSeconds(.3f);
        anim.Fire();
        lastShoot = Time.time;
        CreateBullet();
        yield return new WaitForSeconds(.3f);
        CreateBullet();
        isShooting = false;
    } 

    private void CreateBullet()
    {
        Debug.Log("BULLET");
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }
    private void startShooting()
    {
        isShooting = true;
        anim.Aiming();
        StartCoroutine(Fire());
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PlayerBullet")
        {
            isDying = true;
            anim.Death();
        }
        
    }
}
