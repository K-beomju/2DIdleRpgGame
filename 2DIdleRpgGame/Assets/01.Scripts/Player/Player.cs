using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   private Animator animator;

   public Transform attackPoint;
   public float attackRange  = 0.5f;
   public LayerMask enemyLayers;
   private float Times;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
   void Update()
   {
        if( Times <= Time.time)
        {
            Attack();
            Times = Time.time + 1f;
        }
        else
        {
            Run();
        }
   }

    void Attack()
    {
        animator.SetTrigger("Attack");
         Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position , attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemis)
        {
          FindObjectOfType<SpawnManager>().enemyList[0].GetComponent<Enemy>().OnDamage(1);
        }
    }
    void Run()
    {
        animator.SetTrigger("Run");
    }

    void OnDrawGizmos()
    {
        if(attackPoint == null)
        return;

        Gizmos.DrawWireSphere(attackPoint.position , attackRange);
    }

}
