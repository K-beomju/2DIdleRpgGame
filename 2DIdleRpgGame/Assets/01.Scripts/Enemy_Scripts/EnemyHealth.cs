using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{





    [SerializeField]
    private float hp;
    public Vector3 offset;



    void Start()
    {
        SetEntityDefault(hp);
        slider.maxValue = maxHealth;
        slider.value = health;

    }



    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.position + offset);
            slider.value = health;

    }


    public override void OnDamage(float damage)
    {
         base.OnDamage(damage);
    }






}
