using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class Player : MonoBehaviour
{
     public static Player instance;

    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float Times;
    [SerializeField] float attackRange = 0.5f;

    public float attackDamage = 1f;

    private bool isAttack;

    public AudioClip AttackClip;
    private AudioSource playerAudioSource;

    [SerializeField] float speed = 1f;
    //public GameObject target;

    [Space(48)]

    [SerializeField]
    public GameObject[] skillObjs;
    public SkillHub[] Skills;


    void Start()
    {
        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();


    }
    void Update()
    {
        if (Physics2D.Raycast(transform.position, transform.right, 0.5f))
        {
            animator.SetBool("isAttack", true);
            BackGround.speed = 0f;
        }
        else
        {
            animator.SetBool("isAttack", false);
            playerAudioSource.Stop();
            BackGround.speed = 0.4f;

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

    public void BashAttack()
    {
        GameObject bashAttack = Instantiate(skillObjs[0], transform.position, Quaternion.identity);
        Destroy(bashAttack,0.5f);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (enemy != null)
            {
                var a = enemy.GetComponent<EnemyRun>();
                Debug.Log(a);
                // target.OnDamage(Skills[0].damage);

            }

        }
    }

     void OnDrawGizmos()
     {
         if (attackPoint == null)
             return;
         Gizmos.DrawWireSphere(attackPoint.position, attackRange);
          Gizmos.DrawWireSphere(attackPoint.position, Skills[0].Range);
     }



}
