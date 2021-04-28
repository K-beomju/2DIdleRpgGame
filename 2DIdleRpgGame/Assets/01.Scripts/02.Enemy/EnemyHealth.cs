using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


public class EnemyHealth : LivingEntity
{

    public Vector3 offset; // 위치 보정
    protected Rigidbody2D rigid;
    private EnemyHPBar hpBar; // EnemyHPbar 가져옴
    private EnemyHealth hitEffect;


    private bool isMoving = true; // 윰직일 때





    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();

    }

    void Start()
    {
         hpBar = GameManager.GetEnemyHPBar();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.Reset(pos, 1);
        maxHealth = health;
    }



    void Update()
    {
        if (isMoving)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
           hpBar.SetPosition(pos);
        }
        if(this.gameObject.activeSelf)
        {
              hpBar.gameObject.SetActive(true);
              hpBar.SetValue(health / maxHealth);
        }

    }


    public override void OnDamage(float damage)
    {


        SkillObject hitEffect = GameManager.instance.hitPool.GetOrCreate();
        hitEffect.SetPositionData(transform.position, Quaternion.identity);


        base.OnDamage(damage);
    }









    protected override void Die()
    {
       SpawnManager.isSpawn = true; // 죽을때 스폰 매니저에서 스폰을 트루
        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false); // 적 비활성화
        health = maxHealth;
    }


}
