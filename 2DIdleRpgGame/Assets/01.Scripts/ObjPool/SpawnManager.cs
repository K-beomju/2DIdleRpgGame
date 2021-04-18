using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    Vector2 Vec = new Vector3(3f, 2.5f); // 생성 위치


    public static bool isCreate;
    private float createTime;

    void Start()
    {
        isCreate = true;
    }

    void Update()
    {
        if(createTime < Time.time && isCreate)
        {
           PoolManager.Instance.pool.Pop().transform.position = Vec;
            isCreate = false;
            createTime = Time.time + 5f;
        }
    }



}
