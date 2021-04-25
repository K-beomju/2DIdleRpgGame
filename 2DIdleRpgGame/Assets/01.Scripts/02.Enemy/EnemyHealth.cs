using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{

    public Vector3 offset; // 위치 보정
    protected Rigidbody2D rigid;
    private EnemyHPBar hpBar; // EnemyHPbar 가져옴


    private bool isMoving = true; // 윰직일 때

    Transform player;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
          isDie = false; // LivingEntity isDie = false

    }

    void Start()
    {
         hpBar = GameManager.GetEnemyHPBar();// 게임매니저에서 hpbar 가져옴
        maxHealth = health; // 체력
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset); // 슬라이더 위치
        hpBar.Reset(pos, 1); // 슬라이더 벨루를 1로

         player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset); // 움직일 때 체력바도 움직임
           hpBar.SetPosition(pos); // 렉트 위치
        }

          if(this.gameObject.activeSelf)
          {
              hpBar.gameObject.SetActive(true);
          hpBar.SetValue(health / maxHealth); // value 값을 SetValue
          }




    }

    public override void OnDamage(float damage, bool isPushAttack = false)
    {
        // if (isDie) return;
        base.OnDamage(damage, isPushAttack);


        if (isPushAttack)
        {
            rigid.AddForce(new Vector2(100, 200));
        }
    }



    protected override void Die()
    {

       SpawnManager.Instance.isSpawn = true; // 죽을때 스폰 매니저에서 스폰을 트루
        isDie = true; // isDie true
        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false); // 적 비활성화
        health = maxHealth;

    }


}
