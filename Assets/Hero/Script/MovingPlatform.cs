using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] platformPos;
    public float speed;
    int count = 0;
    void Update()
    {
        Movement();
    }

    void Movement()
    {
       
        if (Vector3.Distance(transform.position, platformPos[count].position)< 0.3f)
        {
            count ++;
            
        }
        if (count>=platformPos.Length) 
        {
          
            count = 0 ;
        }

        transform.position = Vector3.MoveTowards(transform.position, platformPos[count].position,speed * Time.deltaTime);
    }
}
