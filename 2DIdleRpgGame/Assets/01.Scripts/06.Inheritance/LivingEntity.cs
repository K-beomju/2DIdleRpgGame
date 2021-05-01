using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{
    protected float maxHealth;
    public float health;

    public virtual void OnDamage(float damage) // 이건 가지고 가야함 리빙 엔티티 적한테
    {
        health -= damage;


        if (health <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();
}
