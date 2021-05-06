using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public abstract class LivingEntity : MonoBehaviour, IDamageable
{

    public virtual void OnDamage(float damage)
    {
        GameManager.instance.enemyHealth -= damage;


        if (GameManager.instance.enemyHealth <= 0)
        {
            Die();

        }
    }

    protected abstract void Die();
}
