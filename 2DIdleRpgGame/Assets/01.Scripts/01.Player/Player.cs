using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillCategory
{
    BashAttack = 0
}

public class Player : MonoBehaviour
{
    private Animator animator;
    public Transform attackPoint;
    public LayerMask enemyLayers;

    public AudioClip AttackClip;
    private AudioSource playerAudioSource;



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
            GameManager.instance.enemyMoveSpeed = 0;
            animator.SetBool("isAttack", true);
            GameManager.SetBackgroundSpeed(0f);
        }
        else
        {
            GameManager.instance.enemyMoveSpeed = GameManager.instance._enemyMoveSpeed;
            animator.SetBool("isAttack", false);
            playerAudioSource.Stop();
            GameManager.SetBackgroundSpeed(0.5f);
        }
    }

    public void Attack()
    {
        int critiRan = Random.Range(0, 101);
        GameManager.CamShake(0.3f, 0.2f);
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(transform.position, GameManager.instance.attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemis)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (target != null)
            {
                if (critiRan <= GameManager.instance.critical)
                {
                    GameManager.instance.isCritical = true;
                    target.OnDamage(GameManager.instance.attackDamage * GameManager.instance.attackCriticalDamage);
                }
                else
                {
                    target.OnDamage(GameManager.instance.attackDamage);
                }
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


        }
    }



    IEnumerator hitEffecting(EnemyHealth target, int s)
    {
        for (int i = 0; i < Skills[s].AttackCount; i++)
        {
            if (target.gameObject.activeSelf)
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

        Gizmos.DrawWireSphere(attackPoint.position, Skills[0].Range);
    }

}
