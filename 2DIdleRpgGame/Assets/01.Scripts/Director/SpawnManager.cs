using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnManager : MonoBehaviour
{
    private ObjectPooling<EnemyHealth>[] enemyPool;
    [SerializeField] GameObject[] enemyGroup;
    private static SpawnManager instance;
    private float spawnTime;

    void Awake()
    {
        enemyPool = new ObjectPooling<EnemyHealth>[enemyGroup.Length]; //
        for (int i = 0; i < enemyGroup.Length; i++)
        {
            enemyPool[i] = new ObjectPooling<EnemyHealth>(enemyGroup[i], this.transform, 1); //각 이펙트별로 3개씩만 생성
        }
    }

    void Start()
    {
        //  SpawnEnemy();
    }

    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {

        for (int i = 0; i < enemyGroup.Length; i++)
        {
            if(spawnTime < Time.time)
            {
            EnemyHealth enemy = enemyPool[i].GetOrCreate();
            spawnTime = Time.time + 3f;
            }
        }
    }




}
