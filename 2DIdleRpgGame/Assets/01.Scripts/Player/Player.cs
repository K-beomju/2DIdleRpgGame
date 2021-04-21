using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;




public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }



    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    private float Times;
    public float attackRange = 0.5f;
    public float attackDamage = 1f;

    private bool isAttack;

    public AudioClip AttackClip;
    private AudioSource playerAudioSource;

    public float speed = 1f;
    //public GameObject target;

    [Space(48)]

    [SerializeField]
    private GameObject bashSkillAttack;





    // void Move()
    // {
    //     transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    // }

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

    // void OnDrawGizmos()
    // {
    //     if (attackPoint == null)
    //         return;
    //     Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    // }



}
