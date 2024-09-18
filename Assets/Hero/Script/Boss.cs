using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Boss : MonoBehaviour
{

    [Header("Float")]
    public float attackRadius , attackRadius2;
    float count = 0;
    public float disCov = 0;
    float romaingDis = 1f;

    [Header("Int")]
    public int health;

    [Header("Animator")]
    public Animator animator;
    public Animator mainCamera;

    [Header("Vector3")]
    public Vector3 attackOffSet , attackPos , firePos , attackOffSet2 , attackPos2;
    public Vector3 deadEffectPos1, deadEffectPos2 , rockEffectPos;

    [Header("String")]
    string[] attack = { "Attack", "Attack1" };

    [Header("Refernces")]
    public PlayerController playerController;

    [Header("GameObject")]
    public GameObject fireEffect , destroyEffect , rockEffect;
    public GameObject closeDoor2;

    [Header("LayerMask")]
    public LayerMask playerLayer;

    [Header("List")]
    public List<GameObject> healthLight;
    
  
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    
    void Update()
    {
      
        
       
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" )
        {
            if (disCov < romaingDis)
            {
              animator.SetTrigger(attack[Random.Range(0, attack.Length)]);
              disCov += Time.deltaTime;

            }
            else
            {
                disCov = 0;
            }
            

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position + attackPos , attackOffSet);
        Gizmos.DrawWireCube(transform.position + attackPos2, attackOffSet2);
    }

    public void PlayerAttacking()
    {
        Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position + attackPos, attackOffSet, attackRadius , playerLayer);

        print("Hit Animation");

        if (coll.Length > 0 )
        {
            playerController.ReduceHealths(20);
        }
    }

    public void PlayerAttacking2()
    {
        Collider2D[] coll = Physics2D.OverlapBoxAll(transform.position + attackPos2, attackOffSet2, attackRadius2, playerLayer);

        print("Hit Animation");

        if (coll.Length > 0)
        {
            playerController.ReduceHealths(10);
        }
    }

    public void FireEffect()
    {
        Audiomanager.instance.sfxmusic2("Fire");
        Instantiate(fireEffect, transform.position + firePos , Quaternion.identity);
        
    }

    public void BossReduce(int damagehealth)
    {
        
        if (count == 0)
        {
            health -= damagehealth;

            if (health <= 500)
            {
                healthLight[0].SetActive(false);
  
            }
            if (health <= 400)
            {
                healthLight[1].SetActive(false);

            }
            if (health <= 300)
            {
                healthLight[2].SetActive(false);

            }
            if (health <= 200)
            {
                healthLight[3].SetActive(false);

            }
            if (health <= 100)
            {
                healthLight[4].SetActive(false);

            }
            if (health <= 0)
            {
                healthLight[5].SetActive(false);
                animator.SetTrigger("Death");
            }

        }
   
    }
    public void DeathAni()
    {
        Destroy(gameObject);
        Destroy(closeDoor2);

    }
    public void DeathEffect()
    {

        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        Audiomanager.instance.sfxmusic2("Bomb");

    }
    public void DeathEffect1()
    {

        Instantiate(destroyEffect, transform.position + deadEffectPos1, Quaternion.identity);
        Audiomanager.instance.sfxmusic2("Bomb");

    }

    public void DeathEffect2()
    {

        Instantiate(destroyEffect, transform.position + deadEffectPos2, Quaternion.identity);
        Audiomanager.instance.sfxmusic2("Bomb");

    }

    public void CameraShake()
    {
        mainCamera.SetTrigger("Boss_Shake");
        Audiomanager.instance.sfxmusic2("Rock");
        Instantiate(rockEffect, transform.position + rockEffectPos, Quaternion.identity);

    }


}
