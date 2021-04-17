using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    Vector2 Vec = new Vector3(3f, 2.5f); // 생성 위치


    public static bool isCreate = true;
    private float createTime;

    void Update()
    {
        if(createTime < Time.time )
        {
           PoolManager.Instance.pool.Pop().transform.position = Vec;

            createTime = Time.time + 3f;
        }
    }



}
