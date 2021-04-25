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
    private Animator animator; // 애니메이터
    public Transform attackPoint; // 공격 포지션
    public LayerMask enemyLayers; // 적 레이어


    [SerializeField] [Range (0,2)] float attackRange = 0.5f; // 공격 사거리

    private bool isAttack; // 공격 유무

    public AudioClip AttackClip; // 공격 효과음
    private AudioSource playerAudioSource; // 오디오 소스



    [Space(48)]


    public GameObject[] skillObjs; // 스킬 갯수
    public SkillHub[] Skills; // 스킬

    private ObjectPooling<SkillObject>[] skillPool; // 스킬 이펙트풀링

    //단순히 컴포넌트를 받아오는 거라면 Awake에서 시행해라
    void Awake()
    {
        animator = GetComponent<Animator>(); // 애니메티어
        playerAudioSource = GetComponent<AudioSource>(); // 오디오 소스

        skillPool = new ObjectPooling<SkillObject>[skillObjs.Length]; //
        for(int i = 0; i < skillObjs.Length; i++)
        {
            skillPool[i] = new ObjectPooling<SkillObject>(skillObjs[i], this.transform, 3); //각 이펙트별로 3개씩만 생성
        }
    }

    void Update()
    {
        //레이캐스트 쓸때는 디버그를 이용해서 화면에 출력하면 정확하게 볼 수 있다.
        Debug.DrawRay(transform.position, transform.right * 0.3f, Color.blue);
        //충돌은 반드시 의도한 충돌만이 이루어지도록 레이어마스크를 사용해라.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 0.3f, enemyLayers );
        if (hit)
        {
            animator.SetBool("isAttack", true);
            EnemyAttack.moveSpeed = 0;
            GameManager.SetBackgroundSpeed(0f);
        }
        else
        {
            EnemyAttack.moveSpeed = 1f;
            animator.SetBool("isAttack", false);
            playerAudioSource.Stop();
            GameManager.SetBackgroundSpeed(0.2f); //이런식으로 게임매니저를 통해서 제3객체에 접근해라

        }

    }

    void Attack()
    {
        GameManager.CamShake(0.5f, 0.2f); //소소한 카메라 이펙트
        Collider2D[] hitEnemis = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers); // 공격 사거리 , 레이어
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
        bashAttack.SetPositionData( attackPoint.position, Quaternion.identity);  //차후 회전을 위해서라도 회전치는 넣어두는게 좋다.

        GameManager.CamShake(3, 0.5f);

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPoint.position, Skills[0].Range, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            IDamageable target = enemy.transform.GetComponent<IDamageable>();
            if (target != null)  //target을 가져와 놓고 왜 enemy를 널체크하고 있니?
            {
                target.OnDamage(Skills[0].Damage, true); //밀려나는 공격으로 설정 bashAttack.damage

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
