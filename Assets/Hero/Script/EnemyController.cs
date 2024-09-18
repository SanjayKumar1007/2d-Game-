using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [Header("Float")]
    public float attackDis; 
    public float visionRange = 7f, roamingDis = 15f, Speed = 2, attackRadius = 5f;
    private int count = 0;
    private float speed;

    [Header("Vector3")]
    public Vector3 visionOff;

    [Header("Transform")]
    public Transform[] enePos;
    public Transform attackPoints;

    [Header("Animator")]
    public Animator animator;

    [Header("Bool")]
    public bool playerSpotted;

    [Header("LayerMask")]
    public LayerMask playerLayer;

    [Header("Slider")]
    public Slider enemySlider;
   
    [Header("References")]
    public PlayerHealth enemyHealth;
    public PlayerController playerController;


    void Update()
    {
        // PlayerSpotted , Attack Animator

        RaycastHit2D hit = Physics2D.Raycast(transform.position + visionOff, transform.right, visionRange, playerLayer);

        if (enemyHealth.health > 0 && playerController.health>=0)
        {
            if (hit)
            {
               // Audiomanager.instance.sfxmusic2("Enemy_Sword");
                animator.SetTrigger("attack3");

            }
            else
            {

                animator.ResetTrigger("attack3");
                EnemyRom();
            }
        }
       

        
    }
   
    // Draw a Sphere
    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + visionOff, transform.right * visionRange);
        Gizmos.DrawWireSphere(attackPoints.position , attackRadius);
    }

    // Player Attacking
    public void PlayerAttacking()
    {
        Collider2D[] coll = Physics2D.OverlapCircleAll(attackPoints.position , attackRadius , playerLayer);

        if (coll.Length > 0)
        {
            playerController.ReduceHealths(5);
            
           
        }
    }

    // Dead Animation
    public bool IsDead()
    {
        
        if (enemyHealth.health <= 0 )
        {
            return true;
        }
        return false;
    }


    public void DeathAni()
    {

        animator.SetBool("Death", false);
        Destroy(gameObject);


    }

    // Moving Position

    public void EnemyRom()
    {
       
            if (Vector3.Distance(transform.position, enePos[count].position)<0.3f  )
            {

            count++;
           
            transform.Rotate(0, 180, 0);
            enemySlider.transform.Rotate(0, 180, 0);

            }
       
            if (count >= enePos.Length)
            {
               
                count = 0; 
            }
            
            if (Vector2.Distance(transform.position, playerController.transform.position) > attackDis )
            {
            animator.SetFloat("Motion", 2);
            transform.position = Vector3.MoveTowards(transform.position, enePos[count].position, Speed * 2 * Time.deltaTime);
            }


    }
    
}
