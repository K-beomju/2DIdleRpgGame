using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator; // 애니메이터
    public Transform attackPoint; // 공격 포지션
    public LayerMask playerLayer; // 플레이어 레이어

    [SerializeField] [Range (0,2)] float attackRange = 0.5f; // 공격 사거리
    [SerializeField] float attackDamage = 1f; // 공격 데미지
    private bool isAttack; // 공격 유무

    public static float moveSpeed = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }



    void Update()
    {
         Debug.DrawRay(transform.position, transform.right * -0.5f, Color.blue);
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
         if (moveSpeed == 0)
        {
            animator.SetBool("isAttack", true);
        }
        else
        {
            animator.SetBool("isAttack", false);
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
