using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float helath;
    public float maxHealth = 5;
    public float speed = 1f;
    public HealthBar healthBar;
    Vector3 target = new Vector3(-0.2f, 2.45f, 0);

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

           Die();
        }

    }
    void Die()
    {
        Destroy(this.gameObject , 2f);
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target,speed * Time.deltaTime);
         if(GetDistance() < 0.1f)
         {

             speed = 0;
         }
    }
    float GetDistance()
    {
        float distance = Vector2.Distance(transform.position, target);
        return distance;
    }

}
