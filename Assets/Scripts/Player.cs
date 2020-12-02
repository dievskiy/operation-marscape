using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{

    public float speed;
    public float gravity = -9.81f;
    public float jumpSpeed;
    public float accRate = 2.0f;
    public float decRate = -450f;
    float currentSpeed = 0.0f;
    float minSpeed = 0.0f;
    float boostSpeed;

    public GameObject bullet;
   
    private CharacterController characterController;
    private Vector3 movement = new Vector3();

    private float shootAnimationTimerMax = 0.25f;
    private float shootAnimationTimer = 0.25f;

    public Animator animator;
    private Mineral mineral;
    private ProgressBar mineralBar;
    private HpBar hpBar;

    private float playerHp = 100;
    private float playerMaxHp = 100;
    public TextMeshProUGUI playerHpText;

    float elapsed = 0f;

    enum PlayerAnimationState
    {
        RUN,
        JUMP,
        SHOOT
    };

    private PlayerAnimationState animationState = PlayerAnimationState.RUN;

     // Start is called before the first frame update
       void Start()
    {
        characterController = GetComponent<CharacterController>();
        mineral = gameObject.AddComponent<Mineral>();
        mineralBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        hpBar = GameObject.Find("HpBar").GetComponent<HpBar>();

        playerHpText.text = "Player HP: " + playerHp.ToString() + " / 100";
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = currentSpeed * Time.deltaTime;

        if (playerHp<=0)
        {
            //
        }

        if (currentSpeed < speed)
        {
            currentSpeed = currentSpeed + (accRate * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += -transform.right * (currentSpeed / 0.8f) * Time.deltaTime;         
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            boostSpeed = currentSpeed * 1.5f;
            movement += transform.right * boostSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
        }

        if (characterController.isGrounded)
        {
            elapsed += Time.deltaTime;
            if (elapsed >= 0.25f)
            {
                elapsed = elapsed % 0.25f;
                RunSound();
            }

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("jump"))
            {
                animationState = PlayerAnimationState.RUN;
            }

            movement.y = -0.1f;

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                movement.y = jumpSpeed;

                animationState = PlayerAnimationState.JUMP;

                SoundManagerScript.PlaySound("jump");
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed = currentSpeed + (decRate * Time.deltaTime);
            }
        }

        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, speed);

        movement.y += gravity * Time.deltaTime;

        characterController.Move(movement * Time.deltaTime);

        
        // handle shooting animation
        if (shootAnimationTimer < shootAnimationTimerMax && animationState != PlayerAnimationState.SHOOT)
        {
            animationState = PlayerAnimationState.SHOOT;
        }
        else if (shootAnimationTimer >= shootAnimationTimerMax && animationState == PlayerAnimationState.SHOOT)
        {
            ResetAnimation();
        }

        shootAnimationTimer += Time.deltaTime;

        switch (animationState)
        {
            case PlayerAnimationState.RUN:
                animator.Play("run");
                break;
            case PlayerAnimationState.JUMP:
                animator.Play("jump");
                break;
            case PlayerAnimationState.SHOOT:
                animator.Play("shoot");
                break;
        }

        if (playerHp <= 0)
        {
            Die();
        }
    }

    void ResetAnimation()
    {
        if (characterController.isGrounded)
        {
            animationState = PlayerAnimationState.RUN;
        }
        else
        {
            animationState = PlayerAnimationState.JUMP;
        }
    }

    void Shoot()
    {
        shootAnimationTimer = 0;
        animator.Play("shoot", -1, 0);

        Vector3 bulletOrigin = transform.position + (transform.right * 1.25f) + (transform.up * 0.6f);

        Instantiate(bullet, bulletOrigin, transform.rotation);

        SoundManagerScript.PlaySound("lazerGun");
    }

    void RunSound()
    {
        int stepSoundRandom = Random.Range(1, 4);
        SoundManagerScript.PlaySound("maxStep" + stepSoundRandom.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Collectable")
        {
            mineral.PickUpMineral();
            mineralBar.Progress();
            SoundManagerScript.PlaySound("mineralCollect");
            Destroy(other.gameObject);

            if (playerHp < playerMaxHp)
            {
                hpBar.Progress();
                playerHp += 25;
                playerHpText.text = "Player HP: " + playerHp.ToString() + " / 100";
            }
        }

        if (other.tag == "EnemyBullet")
        {
            mineralBar.Regress();
            mineral.DropMineral();
            hpBar.Regress();
            playerHp -= 25;
            playerHpText.text = "Player HP: " + playerHp.ToString() + " / 100";
            Destroy(other.gameObject);
        }

        if (other.tag == "Enemy")
        {
            mineral.DropMineral();
            mineral.DropMineral();
            mineralBar.Regress();
            mineralBar.Regress();
            playerHp -= 50;
            playerHpText.text = "Player HP: " + playerHp.ToString() + " / 100";
            hpBar.Regress();
            hpBar.Regress();
            Destroy(other.gameObject);
        }
    }

    public void Die()
    {
        GameController.current.DeathScreen();
    }

}
