using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [Header("Float")]
    public float health = 100;
    public float speed = 2f;
    public float jumpSpeed = 5f, movementLerp = 10f, attackRadius = 4;
    public float attackTime;
    public float healingTime;
    public float portalDelay;
    float dirx;
    float count = 0;
    float totalTime = 0.5f;
    float healingTotalTime = 0.5f;
    float score;
    public float disCov = 0;
    float romaingDis = 10f;


    [Header("Int")]
    private int enemynumber;
    int skullDeductor = 0;

    [Header("Vector3")]
    public Vector3 camOff;
    public Vector3 bossCamera;

    [Header("RigidBody")]
    public Rigidbody2D rigidBody2d;

    [Header("Animator")]
    public Animator ani;
    public Animator mainCamera;
    public Animator openDoor;
    public Animator closeDoor, closeDoor2;
    public Animator flag;
    

    [Header("Transform")]
    public Transform Transform, cameraTransform, AttackPoint;
    public Transform boss;
    private Transform Original_Pos;
    public Transform checkPoint;

    [Header("LayerMask")]
    public LayerMask enemyLayer;
    public LayerMask GroundLayer;
    public LayerMask bossLayer;

    [Header("SpriteRenderer")]
    SpriteRenderer sprite;

    [Header("Collider")]
    Collider2D coll;

    [Header("GameObject")]
    public GameObject powerEffect;
    public GameObject gameOver;
    public GameObject nextStage;
    public GameObject checkPoint_GO;
    public GameObject checkPoint_panel;

    [Header("References")]
    public Health healthReduce;
    public static PlayerController instances;
    public HealthIn healthIncrease;
    public Text coinPoints;
    Text cCoinPoints;
    public Joystick joystick;
    public Boss bossHealth;



    enum Movement
    {
        Idle,
        Run,
        Jump


    }
    private void Awake()
    {
        if (instances == null)
        {
            instances = this;

        }
    }
    void Start()
    {

        ani = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        Audiomanager.instance.sfxmusic("MainTheme");

    }
    void Update()
    {
        // Movements 

        dirx = Input.GetAxis("Horizontal");

        if (health >= 0)
        {
            if (count == 0)
            {
                cameraTransform.position = new Vector3(transform.position.x + camOff.x, transform.position.y + camOff.y, -50 + camOff.z);
            }


            Movements();

            if (attackTime >= totalTime && Input.GetKeyDown(KeyCode.LeftControl) )

            {
                ani.SetTrigger("Attack1");
                Audiomanager.instance.sfxmusic("Sword");
                attackTime = 0;

            }
            else
            {
                attackTime += Time.deltaTime;
            }
            if (Input.GetKeyDown(KeyCode.Space) && Isgrounded())
            {
                Debug.Log("Space");
                rigidBody2d.velocity = new Vector3(rigidBody2d.velocity.x, jumpSpeed, 0f);


            }

            // Power Increase

            if (healingTime >= healingTotalTime && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");

                Power();

            }
            else
            {
                healingTime += Time.deltaTime;
            }

        }


        if (skullDeductor == 4)
        {
            openDoor.SetTrigger("Open");
            print("OpenDoor");
        }

        



    }

    public void Movements()
    {
        rigidBody2d.velocity = new Vector3(dirx * speed, rigidBody2d.velocity.y, 0f);
        Movement state;
        if (dirx > 0 || joystick.Horizontal > 0)
        {

            transform.rotation = Quaternion.Euler(0, 0, 0);
            state = Movement.Run;

            // Mobile
            if (joystick.Horizontal > 0)
            {
                rigidBody2d.velocity = new Vector3(joystick.Horizontal * speed, rigidBody2d.velocity.y, 0f);
            }

        }
        else if (dirx < 0 || joystick.Horizontal < 0)
        {

            state = Movement.Run;
            transform.rotation = Quaternion.Euler(0, 180, 0);

            // Mobile
            if (joystick.Horizontal < 0)
            {
                rigidBody2d.velocity = new Vector3(joystick.Horizontal * speed, rigidBody2d.velocity.y, 0f);
            }

        }
        else
        {
            state = Movement.Idle;
        }

        if (rigidBody2d.velocity.y > 1f )
        {
            print("Jump_Button");
            Audiomanager.instance.sfxmusic("Player_Jump");
            state = Movement.Jump;

            // Mobile
            if (Isgrounded())
            {
                rigidBody2d.velocity = new Vector3(joystick.Horizontal * speed, jumpSpeed, 0f);
            }

        }


        ani.SetInteger("Move", (int)state);

    }
    bool Isgrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0, Vector2.down, 0.1f, GroundLayer);
    }

    // DrawSphere

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(cameraTransform.position + camOff, 0.5f);
        Gizmos.DrawWireSphere(AttackPoint.position, attackRadius);

    }

    // Player Health Reduce
    public void ReduceHealths(int damageAmount)
    {

        ani.SetTrigger("Hit");
        Audiomanager.instance.sfxmusic("Player_Hurt");
        mainCamera.SetTrigger("Main");
        health -= damageAmount;
        healthReduce.HealthSlider(damageAmount);

        if (health <= 0 && !checkPoint_GO.activeInHierarchy) 
        {
            ani.SetBool("Death", true);
            Audiomanager.instance.playmusic("Fail");
            gameOver.SetActive(true);

        }
        // Checkpoint Gameobject == true

        if (checkPoint_GO.activeInHierarchy && health <= 0)
        {
            
            Audiomanager.instance.playmusic("Fail");
            checkPoint_panel.SetActive(true);
        }

    }

    // Death Animation Fuction
    public void DeathAni()
    {
        ani.SetBool("Death", false);

    }



    // Enemy Attacking
    public void EnemyAttacking()
    {
    
        Collider2D[] colls = Physics2D.OverlapCircleAll(AttackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D coll in colls)
        {

            PlayerHealth enemyhealth = coll.GetComponent<PlayerHealth>();
            if (coll != null)
            {

                enemyhealth.ReduceHealth(35);
                healthIncrease.PowerSlider(10);


            }

        }


    }

    // Boss Attacking
    public void BossAttacking()
    {
      
        Collider2D[] colls = Physics2D.OverlapCircleAll(AttackPoint.position, attackRadius, bossLayer);

            foreach (Collider2D coll in colls)
            {
               if (bossHealth.health > 0)
               {

                     Boss bosses = coll.GetComponent<Boss>();
                     if (coll != null)
                     {

                        healthIncrease.PowerSlider(15);
                        bosses.BossReduce(20);

                     }

               }
            }
      


    }


    // Moving Platform , Portal 
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Moving_Platform

        if (collision.gameObject.tag == "MovingObj")
        {
            Original_Pos = Transform.parent;
            Transform.SetParent(collision.transform);

        }

        //Obstacles

        if (collision.gameObject.tag == "ChainSpike")
        {
            ReduceHealths(20);

        }
        if (collision.gameObject.tag == "Hammer")
        {
            ReduceHealths(5);

        }




    }
  
    private void OnCollisionStay2D(Collision2D collision)
    {

        //Obstacles

        if (health > 10)
        {
            if (disCov < romaingDis)
            {
                if (collision.gameObject.tag == "Blade")
                {

                    ReduceHealths(5);
                    disCov += Time.deltaTime;
                }

            }
            else
            {
                disCov = 0;
            }
        }


    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingObj")
        {

            Transform.SetParent(Original_Pos);

        }
    }

    // Camera move towards to boss position , Skull puzzle

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.gameObject.tag == "BoxCollider")
        {

            count = 1;
            if (count == 1)
            {
                cameraTransform.position = new Vector3(boss.position.x + bossCamera.x, 0 + bossCamera.y, 0);
            }

        }
        if (other.gameObject.tag == "Portal")
        {

            
            nextStage.SetActive(true);


        }

        if (other.gameObject.tag == "SkullY")
        {
            skullDeductor++;
        }

        if (other.gameObject.tag == "SkullR")
        {
            skullDeductor++;
        }

        if (other.gameObject.tag == "SkullG")
        {
            skullDeductor++;
        }

        if (other.gameObject.tag == "SkullB")
        {
            skullDeductor++;
            print(skullDeductor);
        }

        // Coins

        if (other.gameObject.tag == "Coins")
        {
            
            Audiomanager.instance.sfxmusic2("Coins");  
            score += 1;
            coinPoints.text = score.ToString();        

        }

        // Close Door

        if (other.gameObject.tag == "Box_Deductor")
        {
            closeDoor.SetTrigger("Open");
            closeDoor2.SetTrigger("Open");
        }

        // Under_Ground

        if (other.gameObject.tag == "UnderGround" && !checkPoint_GO.activeInHierarchy)
        {
            gameOver.SetActive(true);
        }

        if (other.gameObject.tag == "UnderGround" && checkPoint_GO.activeInHierarchy)
        {
           checkPoint_panel.SetActive(true);
        }

        // CheckPoint

        if (other.gameObject.tag == "CheckPoint")
        {
            flag.SetTrigger("Flag");

        }


        // Treasure

        if (other.gameObject.tag == "Treasure")
        {
            nextStage.SetActive(true);
        }

        // open door ani

        if (other.gameObject.tag == "OpenDoor"){

            closeDoor.SetTrigger("Close");

        }



    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    //Boss_Attack1

    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (bossCov < bossDis)
    //        {
    //            bossAnimator.SetTrigger(attack[Random.Range(0, attack.Length)]);
    //            disCov += Time.deltaTime;

    //        }
    //        else
    //        {
    //            bossCov = 0;
    //        }


    //    }

    //}

    public void Power()
    {

        if (healthIncrease.powerSlider.value > 30)
        {
            int powerUps = 15;
            Debug.Log("PowerUps");
            health += powerUps;
            healthReduce.healthSlider.value += powerUps;
            healthIncrease.powerSlider.value -= 30;

            powerEffect.transform.position = Transform.position;
            Instantiate(powerEffect, Transform.position, Quaternion.identity);


        }
        healingTime = 0;


    }

    public void MobileAttack()
    {
        if (attackTime >= totalTime )

        {
            ani.SetTrigger("Attack1");
            Audiomanager.instance.sfxmusic("Sword");
            attackTime = 0;

        }
        else
        {
            attackTime += Time.deltaTime;
        }
    }

    public void CheckPoint_transform()
    {
        transform.position = checkPoint.position;
        health += 100;
        healthReduce.healthSlider.value += 100;
    }

    public void MobileJump()
    {
        if (Isgrounded ())
        {
            rigidBody2d.velocity = new Vector3(joystick.Horizontal * speed, jumpSpeed, 0f);
            ani.SetInteger("Move", 3);
        }    

    }

   


}
