using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum SkillCategory
{
    Bash = 0,
    Heal = 1
}

public class Player : MonoBehaviour
{
    //public static Player instance; //이건 선넘지..
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

    private ObjectPooling<SkillObject>[] skillPool;

    //단순히 컴포넌트를 받아오는 거라면 Awake에서 시행해라
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerAudioSource = GetComponent<AudioSource>();

        skillPool = new ObjectPooling<SkillObject>[skillObjs.Length];
        for(int i = 0; i < skillObjs.Length; i++)
        {
            skillPool[i] = new ObjectPooling<SkillObject>(skillObjs[i], this.transform, 3); //각 이펙트별로 3개씩만 생성
        }
    }

    void Update()
    {
        //레이캐스트 쓸때는 디버그를 이용해서 화면에 출력하면 정확하게 볼 수 있다.
        Debug.DrawRay(transform.position, transform.right * 0.5f, Color.blue);
        //충돌은 반드시 의도한 충돌만이 이루어지도록 레이어마스크를 사용해라.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.5f, enemyLayers );
        if (hit)
        {
            animator.SetBool("isAttack", true);
            //이런식으로 게임매니저를 통해서 제3객체에 접근해라
            GameManager.SetBackgroundSpeed(0f);
        }
        else
        {
            animator.SetBool("isAttack", false);
            playerAudioSource.Stop();
            //이런식으로 게임매니저를 통해서 제3객체에 접근해라
            GameManager.SetBackgroundSpeed(0.2f);

        }

    }

    void Attack()
    {
        playerAudioSource.Play();
        GameManager.CamShake(0.5f, 0.2f); //소소한 카메라 이펙트
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemis)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (target != null)  //enemy가 null인지 체크하는게 아니라 target이 널인지 체크해야지
            {
                target.OnDamage(attackDamage);
            }

        }
    }

    public void BashAttack()
    {
        SkillObject bashAttack = skillPool[(int)SkillCategory.Bash].GetOrCreate();
        bashAttack.SetPositionData( attackPoint.position, Quaternion.identity);  //차후 회전을 위해서라도 회전치는 넣어두는게 좋다.

        GameManager.CamShake(4, 1f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            //if(enemy != null)
            if (target != null)  //target을 가져와 놓고 왜 enemy를 널체크하고 있니?
            {
                target.OnDamage(bashAttack.damage, true); //밀려나는 공격으로 설정
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
