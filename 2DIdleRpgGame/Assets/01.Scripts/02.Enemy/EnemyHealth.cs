using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{

    public Vector3 offset;
    private Rigidbody2D rigid;
    private EnemyHPBar hpBar;
    private bool isMoving = true; //EnemyMove 참조 바꿔야함      : �̰� ���߿� EnemyMove ��ũ��Ʈ�� ���� �ű⼭ �����ؿ��� ������ �����ؾ� �Ѵ�.

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        maxHealth = health;
        hpBar = GameManager.GetEnemyHPBar(); //HP바 하나 가져옴
        Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
        hpBar.Reset(pos, 1);
    }

    void Update()
    {
        if (isMoving)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position + offset);
            hpBar.SetPosition(pos);
        }
        hpBar.SetValue(health / maxHealth);
    }

    public override void OnDamage(float damage, bool isPushAttack = false)
    {
        if (isDie) return;
        base.OnDamage(damage, isPushAttack);

        if (isPushAttack)
        {
            rigid.AddForce(new Vector2(100, 200));
        }
    }

    protected override void Die()
    {
        isDie = true;
        gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
    }

}
