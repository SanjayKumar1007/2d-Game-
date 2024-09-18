using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float disCov = 0;
    public float speed = 1;
    float romaingDis = 2f;
    public PlayerController controller;
   
 
    void Update()
    {
        //transform.Translate(0, 5 * Time.deltaTime, 0);


        //if (disCov < romaingDis)
        //{

        //    disCov += speed *Time.deltaTime;
        //}
        //else if(disCov >= romaingDis)
        //{
        //    transform.Rotate(180, 0, 0);
        //    disCov = 0;
        //}
        //if (Vector3.Distance(transform.position, controller.transform.position) < bladeDistance)
        //{
        //    Audiomanager.instance.sfxmusic2("Blade");
        //}


    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            if (disCov < romaingDis)
            {
            
                
                controller.ReduceHealths(5);    
                disCov += Time.deltaTime;
            }
            else
            {
                disCov = 0;
            }

        }
       

    }
}
