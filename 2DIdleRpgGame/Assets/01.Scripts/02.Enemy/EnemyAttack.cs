using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IAttackable
{
    private Animator animator;
    public Transform attackPoint;
    public LayerMask playerLayer;




    void Awake()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        transform.Translate(Vector2.left * GameManager.instance.enemyMoveSpeed * Time.deltaTime);
        Attackstatus();
    }


    public void Attackstatus()
    {
         RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, -0.4f, playerLayer);
        if (hit)
        {
            animator.SetBool("isAttack", true);
            GameManager.instance.enemyMoveSpeed = 0;
        }
        else
        {
            animator.SetBool("isAttack", false);
            GameManager.instance.enemyMoveSpeed = 1;
        }
    }



    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, GameManager.instance.enemyAttackRange, playerLayer); // 공격 사거리 , 레이어
        foreach (Collider2D player in hitPlayer)
        {
            IDamageable target = player.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(GameManager.instance.enemyAttackDamage);
            }

        }
    }

      void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, GameManager.instance.enemyAttackRange);

    }


}
