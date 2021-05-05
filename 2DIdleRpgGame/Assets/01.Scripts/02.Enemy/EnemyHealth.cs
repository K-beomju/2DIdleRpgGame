using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class EnemyHealth : LivingEntity
{

    public Vector3 offset;
    protected Rigidbody2D rigid;

    private EnemyHPBar hpBar;
    private DamageText dmgText;
    private DropGold dropgold;


    private bool isMoving = true; // 윰직일 때

    private SpriteRenderer sr;
    private Animator animator;
    public LayerMask playerLayer;






    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Start()
    {
        maxHealth = health;
        hpBar = GameManager.GetEnemyHPBar();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.Reset(pos, 1);


    }



    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            hpBar.gameObject.SetActive(true);
            hpBar.SetValue(health / maxHealth);
            FrontPlr();
        }
        if (isMoving)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
            hpBar.SetPosition(pos);

        }

    }


    public void FrontPlr()
    {
        transform.Translate(Vector2.left * GameManager.instance.enemyMoveSpeed * Time.deltaTime);
    }



    public override void OnDamage(float damage)
    {


        SkillObject hitEffect = GameManager.instance.hitPool.GetOrCreate();
        hitEffect.SetPositionData(transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
        StartCoroutine(cAlpha());


        dmgText = GameManager.GetDamageText();
        dmgText.transform.position = this.transform.position;            // new Vector2(Random.Range( -0.1f, 0.1f), transform.position.y);
        DamageText.damage = damage;
        base.OnDamage(damage);


    }


    protected override void Die()
    {

        UiManager.instance.StageCount();
        DropGoldCount(2);
        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
        SpawnManager.isSpawn = true;
        health = maxHealth;
        sr.color = Color.white;
        if(SpawnManager.isBoss)
        {
          transform.localScale /= GameManager.instance.enemyBossSize; //곱하기로 바꾸기
          SpawnManager.isBoss = false;
        }

    }

    private void DropGoldCount(int count)
    {


        for (int i = 0; i < count; i++)
        {
            dropgold = GameManager.GetDropGold();
            dropgold.transform.position = this.transform.position;
        }

    }

    private IEnumerator cAlpha()
    {
        sr.color = new Color(255 / 255f, 175 / 255f, 175 / 255f, 255 / 255f);
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }

}
