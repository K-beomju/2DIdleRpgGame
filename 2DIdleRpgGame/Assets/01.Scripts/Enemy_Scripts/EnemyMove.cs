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
    	Vector3 velo = Vector3.zero;
	transform.position = Vector3.Lerp(transform.position, target, 0.1f);

    }



}
