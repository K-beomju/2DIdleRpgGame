using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyRun : StateMachineBehaviour
{
    // public float speed = 2.5f;
    // public float attackRange = 3f;
    // Transform player;
    // // public Rigidbody2D rigid;

    // override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //   player = GameObject.FindGameObjectWithTag("Player").transform;
    // //   rigid = animator.GetComponent<Rigidbody2D>();
    // //   Debug.Log(rigid);
    // }



    // override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     Vector2 target = new Vector2(player.position.x, rigid.position.y);
    //     Vector2 newPos = Vector2.MoveTowards(rigid.position, target , speed * Time.deltaTime);
    //     rigid.MovePosition(newPos);

    //     if(Vector2.Distance(player.position, rigid.position) <= attackRange)
    //     {
    //         animator.SetTrigger("Attack");
    //         speed = 0;
    //     }
    // }


    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     animator.ResetTrigger("Attack");
    // }

}
