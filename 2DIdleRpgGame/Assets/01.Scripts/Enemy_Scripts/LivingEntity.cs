using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LivingEntity : MonoBehaviour, IDamageable
{
    private float maxHealth; //최대 체력
    private float health; // 체력
    public bool dead { get; protected set; } //사망

    //  protected Action onHit; //맞을 시 발동
    // protected Action onDeath; //사망 시 발동


    protected void OnEnable() // 생명체가 활성화될 떄 상태를 리셋 Start() 와 비슷함
    {
        dead = false;
        health = maxHealth; // 풀피로 시작함
    }

    protected void SetEntityDefault(float hp) // 최대 Hp 를 설정함
    {
        maxHealth = hp;
        OnEnable();
    }


    public virtual void OnDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            ObjectPooling.Instance.isCreate = true;

        }

    }




    //  public void Die() // Die 처리
    // {
    //     if (onDeath != null) //onDeath 이벤트에 등록된 메서드가 있다면 실행
    //     {
    //         onDeath();
    //     }
    //     dead = true;
    // }

    // public void Hit() // Hit 처리
    // {
    //     if (onHit != null) //onHit 이벤트에 등록된 메서드가 있다면 실행
    //     {
    //         onHit();
    //     }
    // }
}