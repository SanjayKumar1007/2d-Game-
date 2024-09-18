using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMotion : StateMachineBehaviour
{
    // Start is called before the first frame update

    [HideInInspector] public Transform Transform, playerTransform;
    [HideInInspector] public EnemyController enemyController;
    public float coverDis = 0, speed;
    
    

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
    //public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
       
    //    if (enemyController.playerSpotted  && !enemyController.IsDead())
    //    {
    //        if (enemyController.attackRange > Vector3.Distance(Transform.position, playerTransform.position)  )
    //        {
    //            animator.SetFloat("Motion", 0);
    //            animator.SetTrigger("attack3");
               

    //        }
    //        else
    //        {
               
    //                Movement(2, animator);
                
    //        }


    //    }
    //    else
    //    {
            
    //        animator.ResetTrigger("attack3");
    //    }

    //}

    // void EnemyRoaming(Animator animator)
    //{
    //    Movement(1,animator);
    //    coverDis += speed;
    //    if (coverDis > enemyController.roamingDis)
    //    {

    //        Transform.Rotate(0, 180, 0);
    //        coverDis = 0;

    //    }
    //}

    

}
