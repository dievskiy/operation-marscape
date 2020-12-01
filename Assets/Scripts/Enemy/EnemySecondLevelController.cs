using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;
using static EnemyMovementController;

public class EnemySecondLevelController : MonoBehaviour
{
    private EnemyModel model;
    public GameObject bullet;
    public Transform bulletPosition;
    private float lastShoot;
    private float shootFrequency = 3f;
    private Actions anim;
    private bool isDying = false;
    private bool isShooting = false;

    private Vector3 target;
    private Rigidbody rigidbody;
    private Transform player;
    public float speed = 0.07f;
    public bool isNearPlayer = false;
    public float shootingRange = 10f;
    public float livingRange = 140f;
    

    void FixedUpdate()
    {
        // destroy enemy if it is far from player
        if(Mathf.Abs(transform.position.x - player.transform.position.x) > livingRange)
        {
            Destroy(gameObject);
        }

        if(!isShooting && !isDying)
         {
            Vector3 targetPostition = new Vector3( player.position.x, 
                                        this.transform.position.y, 
                                        this.transform.position.z) ;
            this.transform.LookAt(targetPostition);
            transform.LookAt(player);
         }

        target = player.position;
        // when enemy is in shooting range, stop moving and start shooting
        if ((Mathf.Abs(transform.position.x - target.x)) < shootingRange)
        {
            isNearPlayer = true;
        }
        else
        {
            isNearPlayer = false;
        }
    }
    void Start()
    {
        model = new EnemyFirstLevelModel(); 
        anim = GetComponent<Actions>();
        rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        // approach player if its far away
        if(isNearPlayer && !isDying)
        {
            if(Time.time - shootFrequency > lastShoot)
            {
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
        // should be tag comparison
        if(other.gameObject.name.ToLower().Contains("bullet") && other.tag != "EnemyBullet")
        {
            Collider[] colList = transform.GetComponentsInChildren<Collider>();
            foreach(Collider col in colList)
            {
                col.isTrigger = true;
            }
            rigidbody.isKinematic = true;
            isDying = true;
            anim.Death();
        }
    }
}
