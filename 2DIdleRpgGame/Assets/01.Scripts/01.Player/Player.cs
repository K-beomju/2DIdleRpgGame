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

public class Player : LivingEntity
{
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public Text hpText;
    public Slider slider;




    public AudioClip AttackClip;
    private AudioSource playerAudioSource;
    private bool isAttack;


    [SerializeField] [Range(0, 2)] float attackRange = 0.5f;

    [Space(48)]
    public GameObject[] skillObjs;
    public SkillHub[] Skills;
    private ObjectPooling<SkillObject>[] skillPool;



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



        Debug.DrawRay(transform.position, transform.right * 0.3f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.3f, enemyLayers);
        if (hit)
        {
            playerAudioSource.Play();
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
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
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
    }

    public void BashAttack()
    {
        SkillObject bashAttack = skillPool[(int)SkillCategory.Bash].GetOrCreate();
        bashAttack.SetPositionData(attackPoint.position, Quaternion.identity);  //차후 회전을 위해서라도 회전치는 넣어두는게 좋다.

        GameManager.CamShake(3, 0.5f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            EnemyHealth target = enemy.transform.GetComponent<EnemyHealth>(); //IDamageable

            if (target != null)  //target을 가져와 놓고 왜 enemy를 널체크하고 있니?
            {
                target.OnDamage(Skills[0].Damage); //밀려나는 공격으로 설정 bashAttack.damage
                if (Skills[0].Damage >= 240)
                {
                    target.OnSkill(0);
                }
            }
        }
    }

    public void SelfHeal()
    {
        SkillObject selfHeal = skillPool[(int)SkillCategory.Heal].GetOrCreate();
        selfHeal.SetPositionData(transform.position, Quaternion.identity);  //차후 회전을 위해서라도 회전치는 넣어두는게 좋다.

    }




    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(attackPoint.position, Skills[0].Range);
    }




    public override void OnDamage(float damage)
    {
        base.OnDamage(damage);
         slider.value = health / maxHealth;
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
        slider.value = health / maxHealth;
        hpText.text = "0";

    }



}
