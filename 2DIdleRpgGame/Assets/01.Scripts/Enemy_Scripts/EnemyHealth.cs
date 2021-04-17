using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamgeable
{
    public float hp = 10;

     public void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // Die();
            Debug.Log("적 죽음");
        }

    }
}
