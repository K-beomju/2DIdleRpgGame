using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected float maxHealth; //최대 체력
    public float health; // 체력
    protected bool isDie = false;

    public virtual void OnDamage(float damage, bool isPushAttack = false)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();


    /*
      protected void SetEntityDefault(float hp) // 최대 Hp
    {
        //maxHealth = hp;  사용할 필요가 없어보인다.
        //OnEnable(); 이건 호출하라고 만들어논건 아닌데
    }
    */
}
