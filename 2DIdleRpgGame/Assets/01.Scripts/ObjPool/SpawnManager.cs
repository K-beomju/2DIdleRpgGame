using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    Vector2 Vec = new Vector3(3f,  2.45f); // 생성 위치


    private bool isCreate = true;

    void Update()
    {
        if(isCreate)
        {
           PoolManager.Instance.pool.Pop().transform.position = Vec;
            isCreate = false;
        }
    }



}
