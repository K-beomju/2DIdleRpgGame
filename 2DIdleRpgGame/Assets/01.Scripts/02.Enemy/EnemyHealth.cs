using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;


public class EnemyHealth : LivingEntity
{

    public Vector3 offset;
    public Vector3 bossOffset;
    protected Rigidbody2D rigid;

    private EnemyHPBar hpBar;
    private DamageText dmgText;
    private GoldText goldText;
    private DropGold dropgold;
    private SkillObject hitEffect;


    private SpriteRenderer sr;
    private Animator animator;
    public LayerMask playerLayer;

    private void Awake()
    {
        gameObject.SetActive(false);
        sr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }


    void OnEnable()
    {

        hpBar = GameManager.GetEnemyHPBar();
        hpBar.Reset(ScreenTransform(offset), 1);

        if (GameManager.instance.isBoss)
        {

            hpBar.transform.localScale = new Vector3(1.5f, 1, 0);
            hpBar.Reset(ScreenTransform(bossOffset), 1);

        }


    }


    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            hpBar.gameObject.SetActive(true);
            if (GameManager.instance.isBoss)
            {
                hpBar.SetValue(GameManager.instance.enemyHealth / GameManager.instance.EnemyBossHealth);
                hpBar.SetPosition(ScreenTransform(bossOffset));
            }
            else
            {
                hpBar.SetValue(GameManager.instance.enemyHealth / GameManager.instance.enemyMaxHealth);
                hpBar.SetPosition(ScreenTransform(offset));
            }
            transform.Translate(Vector2.left * GameManager.instance.enemyMoveSpeed * Time.deltaTime);
        }


    }


    public Vector3 ScreenTransform(Vector3 Correction)
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + Correction);
        return pos;
    }


    public override void OnDamage(float damage)
    {
        OnHitEffect();
        UpDamageText(damage);
        StartCoroutine(cAlpha());
        base.OnDamage(damage);
    }


    protected override void Die()
    {
        // Gold , UI
        GameManager.instance.gold += GameManager.instance.enemyGold;
        UiManager.instance.GoldCount(UiManager.instance.goldText, GameManager.instance.gold);
        UiManager.instance.StageCount();
        // GoldTxt , DropGold
        UpGoldText();
        DropGoldCount(2);
        // false
        hpBar.gameObject.SetActive(false);
        gameObject.SetActive(false);
        // 초기화
        GameManager.instance.isSpawn = true;
        GameManager.instance.enemyHealth = GameManager.instance.enemyMaxHealth;
        sr.color = Color.white;

        if (GameManager.instance.isBoss)
        {
            transform.localScale *= 0.8f;
            hpBar.transform.localScale = new Vector3(1, 1, 0);
            GameManager.instance.isBoss = false;

        }

    }



    #region 풀링 함수
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

    private void UpDamageText(float damage)
    {
        dmgText = GameManager.GetDamageText();
        dmgText.damageText.text = damage.ToString();
        dmgText.transform.position = this.transform.position;
    }

    private void OnHitEffect()
    {
        hitEffect = GameManager.GetHitEffect();
        hitEffect.SetPositionData(new Vector2(Random.Range(-0.1f , 0.3f), transform.position.y), Quaternion.Euler(0, 0, Random.Range(0, 360f)));
    }

    private IEnumerator cAlpha()

    {
        sr.color = new Color(1, 175 / 255f, 175 / 255f, 1);
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
    #endregion


}
