using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static PlayerModel;

public class Player : MonoBehaviour
{
    //Variables for configuring player movement
   
    public float gravity = -9.81f;
  
    public float jumpSpeed;
    public float accRate = 2.0f;
    public float decRate = -450f;
    public float currentSpeed = 0.0f;
    public float maxSpeed;
    public float minSpeed = 0.0f;
    private float boostSpeed;

    public GameObject bullet;
   
    private CharacterController characterController;
    private Vector3 movement = new Vector3();
    private Vector3 gravityMovement = new Vector3();

    private float shootAnimationTimerMax = 0.25f;
    private float shootAnimationTimer = 0.25f;

    public Animator animator;
    private Mineral mineral;
    private ProgressBar mineralBar;
    private HpBar hpBar;

    private PlayerModel model = new PlayerModel();
    private float playerMaxHp = 100;
    public TextMeshProUGUI playerHpText;

    private float elapsed = 0f;

    enum PlayerAnimationState
    {
        RUN,
        JUMP,
        SHOOT
    };

    private PlayerAnimationState animationState = PlayerAnimationState.RUN;

    void Start()
    {
        //Setting variables for right components 
        characterController = GetComponent<CharacterController>();
        mineral = gameObject.AddComponent<Mineral>();
        mineralBar = GameObject.Find("ProgressBar").GetComponent<ProgressBar>();
        hpBar = GameObject.Find("HpBar").GetComponent<HpBar>();

        //Configuring player hp text for hpbar text
        playerHpText.text = "Player HP: " + model.GetHp().ToString() + " / 100";
    }

    void Update()
    {
        //Sets the player to current speed
        movement.x = currentSpeed * Time.deltaTime;

        //Accelerates current speed if player speed hasn't been achieved
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += (accRate = Time.deltaTime);
        }
        
        //Moves the player slightly to left on left arrow key
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement += -transform.right * (currentSpeed / 0.8f * Time.deltaTime);         
        }

        //Boosts speed to right on right arrow key
        if (Input.GetKey(KeyCode.RightArrow))
        {
            boostSpeed = currentSpeed * 1.5f;
            movement += transform.right * ( boostSpeed * Time.deltaTime);
        }
        
        //Shoots on X
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }

        //Calls RunSound method every 0.25 seconds, if player is on the ground
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

            //Sets player to ground
            gravityMovement.y = -0.01f;

            //Jumps on space and arrow up, animates jump and plays jumpsound
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)))
            {
                gravityMovement.y = jumpSpeed;
                animationState = PlayerAnimationState.JUMP;
                SoundManagerScript.PlaySound("jump");
            }

            //Decelerates the player on key down
            if (Input.GetKey(KeyCode.DownArrow))
            {
                currentSpeed = currentSpeed + (decRate * Time.deltaTime);
            }
        }

        //Sets the max limit to speed
        currentSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);

        gravityMovement.y -= gravity * Time.deltaTime;

        characterController.Move(movement);
        characterController.Move(gravityMovement * Time.deltaTime);
        
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

    //Generates an random int between 1-3 for selecting random stepsound
    void RunSound()
    {
        int stepSoundRandom = Random.Range(1, 4);
        SoundManagerScript.PlaySound("maxStep" + stepSoundRandom.ToString());
    }

    //Handles collision with different game objects
    private void OnTriggerEnter(Collider other)
    {
        //Collision with mineral
        if (other.tag =="Collectable")
        {
            mineral.PickUpMineral();
            mineralBar.Progress();
            SoundManagerScript.PlaySound("mineralCollect");
            Destroy(other.gameObject);

            if (model.GetHp() < playerMaxHp)
            {
                hpBar.Progress();
                model.Heal(25);
            }
        }

        //Collision with enemybullet
        if (other.tag == "EnemyBullet")
        {
            mineralBar.Regress();
            mineral.DropMineral();
            hpBar.Regress();
            SoundManagerScript.PlaySound("lazerHit");
            if (model.Damage(25) == 0) Die();
            Destroy(other.gameObject);
        }

        //Collision with enemy
        if (other.tag == "Enemy")
        {
            Debug.Log("auts");
            mineral.DropMineral();
            mineral.DropMineral();
            mineralBar.Regress();
            mineralBar.Regress();
            if(model.Damage(50) == 0) Die();
            hpBar.Regress();
            hpBar.Regress();
            Destroy(other.gameObject);
        }
        
        playerHpText.text = "Player HP: " + model.GetHp().ToString() + " / 100";

    }

    //Method for player's death, activates deathscreen and plays deathsound
    public void Die()
    {
        SoundManagerScript.PlaySound("maxDeath");
        GameController.current.DeathScreen();
    }

}
