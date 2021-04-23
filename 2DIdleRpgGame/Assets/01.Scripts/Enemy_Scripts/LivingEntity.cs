using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour, IDamageable
{
    protected float maxHealth; //최대 체력
    public float health; // 체력
    public Slider slider;


    protected void OnEnable() // 리셋
    {
        // dead = false;
        health = maxHealth;
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

        }
    }







}
