using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
    [SerializeField]
    //private float hp;  //���ʿ� �θ� health�� �ִµ� �� �ڽĿ� �� hp�� �ִ°ǰ�?
    public Vector3 offset;

    private Rigidbody2D rigid; //������ٵ� 2d�� ��������
    private EnemyHPBar hpBar;
    private bool isMoving = true; //EnemyMove 참조 바꿔야함      : �̰� ���߿� EnemyMove ��ũ��Ʈ�� ���� �ű⼭ �����ؿ��� ������ �����ؾ� �Ѵ�.

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        maxHealth = health;
        //slider.maxValue = maxHealth;
        //slider.value = health;
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
        //slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
        //slider.value = health;
    }

    public override void OnDamage(float damage, bool isPushAttack = false)
    {
        if (isDie) return;
        hpBar.SetValue(health / maxHealth);
        base.OnDamage(damage, isPushAttack);

        if (isPushAttack)
        {
            //���⼭ ��þ��ÿ� ���� ������ ���ָ�ȴ�.
            rigid.AddForce(new Vector2(150, 450));
        }
    }

    protected override void Die()
    {
        isDie = true;
        gameObject.SetActive(false);
        hpBar.gameObject.SetActive(false);
    }

}
