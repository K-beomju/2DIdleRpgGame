using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
    [Header("체력 설정란 health 건들지 마셈")]
    public float hp;
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
    }


}
