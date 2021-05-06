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

    private EnemyHPBar hpBar;   // 풀링
    private DamageText dmgText; // 풀링
    private GoldText goldText;  // 풀링
    private DropGold dropgold;  // 풀링


    private SpriteRenderer sr;
    private Animator animator;
    [SerializeField] LayerMask playerLayer;






    private void Awake()
    {
        gameObject.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    void OnEnable()
    {
        GameManager.instance.enemyMaxHealth = GameManager.instance.enemyHealth;
        hpBar = GameManager.GetEnemyHPBar();
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.Reset(pos, 1);

    }



    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            hpBar.gameObject.SetActive(true);
            hpBar.SetValue(GameManager.instance.enemyHealth / GameManager.instance.enemyMaxHealth);

            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
            hpBar.SetPosition(pos);

            transform.Translate(Vector2.left * GameManager.instance.enemyMoveSpeed * Time.deltaTime);
        }


    }




    public override void OnDamage(float damage)
    {


        OnHitEffect();
        StartCoroutine(cAlpha());


        dmgText = GameManager.GetDamageText();
        dmgText.transform.position = this.transform.position;            // new Vector2(Random.Range( -0.1f, 0.1f), transform.position.y);

        base.OnDamage(damage);


    }


    protected override void Die()
    {

        GameManager.instance.Gold += GameManager.instance.enemyGold;
        UiManager.instance.GoldCount();

        UpGoldText();
        DropGoldCount(2);

        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false);

        SpawnManager.isSpawn = true;
        GameManager.instance.enemyHealth = GameManager.instance.enemyMaxHealth;
        sr.color = Color.white;

        if (SpawnManager.isBoss)
        {
            transform.localScale *= 0.8f;
            SpawnManager.isBoss = false;
        }
        UiManager.instance.StageCount();

    }

    private void DropGoldCount(int count)
    {
        for (int i = 0; i < count; i++)
        {
            dropgold = GameManager.GetDropGold();
            dropgold.transform.position = this.transform.position;
        }

    }

    private void UpGoldText()
    {
        goldText = GameManager.GetGoldText();
        goldText.transform.position = GameManager.instance.goldTxt.position;
    }

    private void OnHitEffect()
    {
          SkillObject hitEffect = GameManager.instance.hitPool.GetOrCreate();
        hitEffect.SetPositionData(transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360f)));
    }

    private IEnumerator cAlpha()
    {
        sr.color = new Color(255 / 255f, 175 / 255f, 175 / 255f, 255 / 255f);
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }

}
