using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
     public GameObject hudDamageText;
    public Transform hudPos;


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


    public override void OnDamage(float damage)
    {
       GameObject hudText = Instantiate(hudDamageText); // 생성할 텍스트 오브젝트
        hudText.transform.position = hudPos.position; // 표시될 위치
        hudText.GetComponent<DamageTextManager>().damage = (int)damage; // 데미지 전달
        base.OnDamage(damage);
    }


}
