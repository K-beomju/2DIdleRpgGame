using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum SkillCategory
{
    Bash = 0,
    Heal = 1
}

public class Player : LivingEntity,IAttackable
{
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Text hpText;
    public Slider slider;
    public AudioClip AttackClip;
    private AudioSource playerAudioSource;


    [Space(48)]
    public GameObject[] skillObjs;
    public SkillHub[] Skills;
    private ObjectPooling<SkillObject>[] skillPool;

    RaycastHit2D hits;


    void Awake()
    {
        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        skillPool = new ObjectPooling<SkillObject>[skillObjs.Length];
        for (int i = 0; i < skillObjs.Length; i++)
        {
            skillPool[i] = new ObjectPooling<SkillObject>(skillObjs[i], this.transform, 3);
        }
    }

    void Start()
    {
        maxHealth = health;
        hpText.text = health.ToString();

    }


    void Update()
    {

        hpText.text = health.ToString();
        Attackstatus();

    }





    public void Attackstatus()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, GameManager.instance.attackRange, enemyLayers);
        if (hit)
        {

            animator.SetBool("isAttack", true);
            GameManager.SetBackgroundSpeed(0f);
        }
        else
        {
            animator.SetBool("isAttack", false);
            playerAudioSource.Stop();
            GameManager.SetBackgroundSpeed(0.2f);
        }
    }






    void Attack()
    {
        GameManager.CamShake(0.5f, 0.2f);
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, GameManager.instance.attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemis)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                target.OnDamage(GameManager.instance.attackDamage);
            }

        }
    }
    void AttackSound()
    {
        playerAudioSource.Play();
    }



    public void BashAttack()
    {
        SkillObject bashAttack = skillPool[(int)SkillCategory.Bash].GetOrCreate();
        bashAttack.SetPositionData(attackPoint.position, Quaternion.identity);

        GameManager.CamShake(3, 0.5f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            EnemyHealth target = enemy.transform.GetComponent<EnemyHealth>(); //IDamageable

            if (target != null)
            {
                for (int i = 0; i < Skills[0].AttackCount; i++)
                {
                target.OnDamage(Skills[0].Damage);
                target.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));

                }


            }
        }
    }








    void OnDrawGizmos()
    {
         if (attackPoint == null)
         return;
        Gizmos.DrawWireSphere(attackPoint.position, Skills[0].Range);
    }




    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
         slider.value = health / maxHealth;
           hpText.text = health.ToString();
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        slider.value = health / maxHealth;
        hpText.text = "0";

    }



}
