using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float helath;
    public float maxHealth = 5;
    public HealthBar healthBar;

    void Start()
    {
        helath = maxHealth;
        healthBar.SetHealth(helath, maxHealth);
    }
    public void OnDamage(float damage)
    {
        helath -= damage;
        healthBar.SetHealth(helath, maxHealth);

        if(helath <= 0)
        {
            Destroy(this.gameObject);
        }

    }
}
