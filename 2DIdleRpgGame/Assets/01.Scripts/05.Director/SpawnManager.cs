using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnManager : MonoBehaviour
{
    private ObjectPooling<EnemyHealth>[] enemyPool;
    private EnemyHealth enemy;
    public GameObject[] enemyGroup;
    public GameObject spawnPosition;

    public int curEnemyIndex = 0;
    public static bool isSpawn;
    public static bool isBoss = false;




    void Awake()
    {
        enemyPool = new ObjectPooling<EnemyHealth>[enemyGroup.Length];
        for (int i = 0; i < enemyGroup.Length; i++)
        {
            enemyPool[i] = new ObjectPooling<EnemyHealth>(enemyGroup[i], this.transform, 1);
        }

    }

    void Start()
    {
        isSpawn = true;

    }

    void Update()
    {
        if (isSpawn)
        {
            if (GameManager.instance.stageMobCount < 10)
            {

                SpawnEnemy();
            }
            else
            {
                SpawnBoss();
            }

        }

    }


    void SpawnEnemy()
    {

        isSpawn = false;
        enemy = enemyPool[curEnemyIndex].GetOrCreate();
        enemy.transform.position = spawnPosition.transform.position;
        if (curEnemyIndex >= enemyGroup.Length - 1)
        {
            curEnemyIndex = 0;
        }
        else
        {
            curEnemyIndex++;
        }

    }

    void SpawnBoss()
    {

        isBoss = true;
        isSpawn = false;
        enemy = enemyPool[Random.Range(0, enemyGroup.Length - 1)].GetOrCreate();
        enemy.transform.position = spawnPosition.transform.position;
        enemy.transform.localScale *= GameManager.instance.enemyBossSize;


    }
}
