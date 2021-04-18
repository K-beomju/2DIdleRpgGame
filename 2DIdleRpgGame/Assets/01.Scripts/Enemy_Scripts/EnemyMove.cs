using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1f;
    public Vector3 target = new Vector3(0.2f, 2.47f, 0);

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }



}
