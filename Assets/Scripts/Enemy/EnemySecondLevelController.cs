using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnemyModel;
using static EnemyLevelController;

public class EnemySecondLevelController : MonoBehaviour, EnemyLevelController {
    private EnemyModel model;
    public GameObject bullet;
    public Transform bulletPosition;
    private float lastShoot;
    private float shootFrequency = 3f;
    private Actions anim;
    private bool isShooting = false;

    private Vector3 target;
    private Rigidbody rigidbody;
    private Transform player;
    public float speed = 0.07f;
    public bool isNearPlayer = false;
    public float shootingRange = 10f;
    public float livingRange = 140f;
    private const float PLAYER_ATTACK_VALUE = 25f;

    public bool HasDied () {
        return model.IsDead ();
    }

    public float GetDamage () {
        return model.GetDamage ();
    }

    void FixedUpdate () {
        // destroy enemy if it is far from player
        if (Mathf.Abs (transform.position.x - player.transform.position.x) > livingRange) {
            Destroy (gameObject);
        }

        if (!isShooting && !model.IsDead ()) {
            Vector3 targetPostition = new Vector3 (player.position.x,
                this.transform.position.y,
                this.transform.position.z);
            this.transform.LookAt (targetPostition);
            // transform.LookAt (player);
        }

        target = player.position;
        // when enemy is in shooting range, stop moving and start shooting
        if ((Mathf.Abs (transform.position.x - target.x)) < shootingRange) {
            isNearPlayer = true;
        } else {
            isNearPlayer = false;
        }
    }
    void Start () {
        model = new EnemySecondLevelModel ();
        anim = GetComponent<Actions> ();
        rigidbody = GetComponent<Rigidbody> ();
        player = GameObject.Find ("Player").transform;
    }

    void Update () {
        // approach player if its far away
        if (isNearPlayer && !model.IsDead ()) {
            if (Time.time - shootFrequency > lastShoot) {
                if (!isShooting) startShooting ();
            }
        } else {
            anim.Run ();
        }
    }

    private IEnumerator Fire () {
        yield return new WaitForSeconds (.3f);
        anim.Fire ();
        lastShoot = Time.time;
        CreateBullet ();
        yield return new WaitForSeconds (.3f);
        CreateBullet ();
        isShooting = false;
    }

    private void CreateBullet () {
        var pos = new Vector3 (bulletPosition.position.x, bulletPosition.position.y, bulletPosition.position.z - .5f);
        Instantiate (bullet, pos, Quaternion.identity);
    }
    private void startShooting () {
        isShooting = true;
        anim.Aiming ();
        StartCoroutine (Fire ());
    }

    void OnTriggerEnter (Collider other) {
        // should be tag comparison
        if (other.gameObject.name.ToLower ().Contains ("bullet") && other.tag != "EnemyBullet") {
            if (model.IsDead ()) return;
            Collider[] colList = transform.GetComponentsInChildren<Collider> ();
            foreach (Collider col in colList) {
                col.isTrigger = true;
            }
            rigidbody.isKinematic = true;
            if (model.Attack (PLAYER_ATTACK_VALUE) == 1) return;
            anim.Death ();
        }
    }
}