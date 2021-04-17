using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Run,
    Attack
};


public class Player : MonoBehaviour
{
   public State state {get; private set;}
   private Animator animator;

   public Transform attackPoint;
   public float attackRange  = 0.5f;
   public LayerMask enemyLayers;
   private float Times;
   private float attackDamage = 1f;

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
         Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position , attackRange);

        foreach(Collider2D enemy in hitEnemis)
        {
            enemy.GetComponent<EnemyHealth>().OnDamage(attackDamage);

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
