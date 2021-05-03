using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public enum SkillCategory
{
    BashAttack = 0,
    TornadoAttack = 1
}

public class Player : MonoBehaviour
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


    void Update()
    {
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

    public void Attack()
    {

        GameManager.CamShake(0.8f, 0.2f);
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(transform.position, GameManager.instance.attackRange, enemyLayers);
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

        GameManager.CamShake(4, 1f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            EnemyHealth target = enemy.transform.GetComponent<EnemyHealth>();   //IDamageable
            if (target != null)
            {
                StartCoroutine(hitEffecting(target, 0));
            }

         //   target.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));


        }
    }



    IEnumerator hitEffecting(EnemyHealth target, int s)
    {
        for (int i = 0; i < Skills[s].AttackCount; i++)
        {
            if(target.gameObject.activeSelf)
            {
            target.OnDamage(Skills[s].Damage);
            yield return new WaitForSeconds(0.1f);

            }

        }
    }

    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;
        ///  Debug.DrawRay(transform.position, transform.right * GameManager.instance.attackRange, Color.yellow);
        Gizmos.DrawWireSphere(attackPoint.position, Skills[0].Range);
    }




    // public override void OnDamage(float damage)
    // {
    //     base.OnDamage(damage);
    //     slider.value = health / maxHealth;
    //     hpText.text = health.ToString();
    // }

    // protected override void Die()
    // {
    //     gameObject.SetActive(false);
    //     slider.value = health / maxHealth;
    //     hpText.text = "0";

    // }

//    public void AttackSpeed()
//     {

//         StartCoroutine(UpAttackSpeed(GameManager.instance.attackSpeed, GameManager.instance.attackDelay));

//     }
//     private IEnumerator UpAttackSpeed(float changeSpeed, float delay)
//     {
//         attackSpeedUpObj.SetActive(true);
//         animator.SetFloat("attackSpeed", changeSpeed);
//         yield return new WaitForSeconds(delay);
//         attackSpeedUpObj.SetActive(false);
//         animator.SetFloat("attackSpeed", 0.7f);
//     }




    // public void TornadoAttack()
    // {
    //     SkillObject tornadoAttack = skillPool[(int)SkillCategory.TornadoAttack].GetOrCreate();
    //     tornadoAttack.SetPositionData(attackPoint.position + offSet, Quaternion.identity);

    //     GameManager.CamShake(3, 0.5f);
    //     Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(tornadoAttack.transform.position, Skills[1].Range, enemyLayers);
    //     foreach (Collider2D enemy in enemiesToDamage)
    //     {
    //         EnemyHealth target = enemy.transform.GetComponent<EnemyHealth>();
    //         if (target != null)
    //         {
    //             StartCoroutine(hitEffecting(target, 1));
    //         }

    //         target.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 0));


    //     }
    // }


}
