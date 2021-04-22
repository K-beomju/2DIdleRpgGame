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

    public void BashAttack()
    {
        GameObject bashAttack = Instantiate(skillObjs[0], transform.position, Quaternion.identity);
       // bashAttack.name = Skills[0].name; // 클론 미 클론
        Destroy(bashAttack,2f);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (enemy != null)
            {
             enemy.GetComponent<EnemyMove>().rigid.AddForce(Vector2.right * Skills[0].damage, ForceMode2D.Impulse);
                target.OnDamage(Skills[0].damage);
                // enemy.gameObject.SetActive(false);
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
