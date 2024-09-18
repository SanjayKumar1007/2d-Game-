using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Int")]
    public int health = 100;

    [Header("Vector3")]
    public Vector3 bloodOffSet;

    [Header("Slider")]
    public Slider healthSlider;

    [Header("Animator")]
    public Animator animator;

    [Header("GameObject")]
    public GameObject bloodEffect;
    public GameObject bloodEnable;

    [Header("Button")]
    public Button bloodOn;

    public void ReduceHealth(int damageAmount )
    {

        health -= damageAmount;
        healthSlider.value = health;

        if (health > 0)
        {
            animator.SetTrigger("Hit");
            Audiomanager.instance.sfxmusic2("Enemy_Blood");

            if (bloodEnable.activeInHierarchy) 
            {
                Instantiate(bloodEffect, transform.position + bloodOffSet, Quaternion.identity);
            }
            

        }


        if (health < 0)
        {

            animator.SetBool("Death", true);

        }


    }

    public void BloodEffect()
    {

    }

}
