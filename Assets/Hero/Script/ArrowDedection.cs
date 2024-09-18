using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class ArrowDedection : MonoBehaviour
{
  
    public float arrowDis=7;
    public PlayerController playerController;
    public Vector3 distancesOffSet;
   

    void Start()
    {
        
            transform.DOMove(new Vector3(distancesOffSet.x,distancesOffSet.y,distancesOffSet.z), arrowDis).SetLoops(-1,LoopType.Restart);
            
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            print("reduce");
            playerController.ReduceHealths(5);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
