using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float Times;
    public float attackRange = 0.5f;
    private float attackDamage = 1f;

    private bool isAttack;

    public AudioClip AttackClip;
    private AudioSource playerAudioSource;




    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();


    }
    void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.right, 0.5f))
        {
            animator.SetBool("isAttack",true);

        }
        else
        {
            animator.SetBool("isAttack",false);
            playerAudioSource.Stop();

        }

    }

    void Attack()
    {
            playerAudioSource.Play();
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemis)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (enemy != null)
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
