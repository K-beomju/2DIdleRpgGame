using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;



public class EnemyHealth : LivingEntity
{

    public Vector3 offset;
    protected Rigidbody2D rigid;
    private EnemyHPBar hpBar;
    private DamageText dmgText;


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
        if(this.gameObject.activeSelf)
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
         RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, -0.8f, playerLayer);
        if (hit)
        {

            GameManager.instance.enemyMoveSpeed = 0;
        }
        else
        {
            GameManager.instance.enemyMoveSpeed = 1;
        }
    }



    public override void OnDamage(float damage)
    {

        dmgText = GameManager.instance.dmgPool.GetOrCreate();
         dmgText.transform.position = new Vector2(Random.Range( -0.1f, 0.1f), transform.position.y);
        DamageText.damage = damage;

        SkillObject hitEffect = GameManager.instance.hitPool.GetOrCreate();
        hitEffect.SetPositionData(transform.position,Quaternion.Euler(0, 0, Random.Range(0 ,360f)));
         StartCoroutine(cAlpha());
        base.OnDamage(damage);
    }


    protected override void Die()
    {

        hpBar.gameObject.SetActive(false);
       SpawnManager.isSpawn = true; // 죽을때 스폰 매니저에서 스폰을 트루
        gameObject.SetActive(false); // 적 비활성화
        health = maxHealth;
    }

    private IEnumerator cAlpha()
    {
        sr.color = new Color(255/ 255f, 175/ 255f, 175/ 255f, 255 / 255f);
        yield return new WaitForSeconds(0.3f);
        sr.color = new Color(1,1,1,1);
    }

}
