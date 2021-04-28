using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum SkillCategory
{
    BashAttack = 0

}

public class Player : LivingEntity, IAttackable
{
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public Text hpText;
    public Slider slider;
    public AudioClip AttackClip;
    private AudioSource playerAudioSource;
    public GameObject attackSpeedUpObj;



    [Space(48)]
    public GameObject[] skillObjs;
    public SkillHub[] Skills;
    private ObjectPooling<SkillObject>[] skillPool;

    private int moveCount = 0;



    void Awake()
    {

        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        skillPool = new ObjectPooling<SkillObject>[skillObjs.Length];
        for (int i = 0; i < skillObjs.Length; i++)
        {
            skillPool[i] = new ObjectPooling<SkillObject>(skillObjs[i], this.transform, 3);
        }




        animator.SetFloat("attackSpeed", 0.7f);
        attackSpeedUpObj.SetActive(false);

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

        if(moveCount == 1)
        {
            StartCoroutine(bMoveSpeed());
        }


    }
    public void MoveSpeed()
    {
        moveCount++;
    }
   private IEnumerator bMoveSpeed()
   {
       GameManager.instance.backSpeed = 1;
       yield return new WaitForSeconds(3f);
       moveCount = 0;
       //yield return null;

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






    public void Attack()
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
        SkillObject bashAttack = skillPool[(int)SkillCategory.BashAttack].GetOrCreate();
        bashAttack.SetPositionData(attackPoint.position, Quaternion.identity);

        GameManager.CamShake(3, 0.5f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            //IDamageable
            EnemyHealth target = enemy.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                StartCoroutine(hitEffecting(target));
            }

            target.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));


        }
    }


    IEnumerator hitEffecting(EnemyHealth target)
    {
        for (int i = 0; i < Skills[0].AttackCount; i++)
        {

            target.OnDamage(Skills[0].Damage);
            yield return new WaitForSeconds(0.1f);

        }
    }



    public void AttackSpeed()
    {

        StartCoroutine(UpAttackSpeed(GameManager.instance.attackSpeed, GameManager.instance.attackDelay));

    }
    private IEnumerator UpAttackSpeed(float changeSpeed, float delay)
    {
        attackSpeedUpObj.SetActive(true);
        animator.SetFloat("attackSpeed", changeSpeed);
        yield return new WaitForSeconds(delay);
        attackSpeedUpObj.SetActive(false);
        animator.SetFloat("attackSpeed", 0.7f);
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
