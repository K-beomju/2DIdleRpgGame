using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMove : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 target = new Vector3(0.1f, 2.47f);
    bool isGoal;
    public Rigidbody2D rigid;
    float originDistance;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {

            transform.position = Vector3.Lerp(transform.position, target, 5f * Time.deltaTime);






    }




}
