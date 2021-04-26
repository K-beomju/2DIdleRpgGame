using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator; 
    public Transform attackPoint; 
    public LayerMask playerLayer;

    [SerializeField] [Range (0,2)] float attackRange = 0.5f;
    [SerializeField] float attackDamage = 1f; 
    [SerializeField] float moveSpeed = 1f;


    private bool isAttack; 


    void Awake()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
        // Debug.DrawRay(transform.position, transform.right * -0.3f, Color.blue);
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, -0.4f, playerLayer);
        if (hit)
        {
            animator.SetBool("isAttack", true);
            moveSpeed = 0;
        }
        else
        {
            animator.SetBool("isAttack", false);
            moveSpeed = 1;
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer); // 공격 사거리 , 레이어
        foreach (Collider2D player in hitPlayer)
        {
            IDamageable target = player.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(attackDamage);
            }

        }
    }

      void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

    }


}
