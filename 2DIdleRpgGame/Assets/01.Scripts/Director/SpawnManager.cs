using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnManager : MonoBehaviour
{
    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            return instance;
        }
    }
    private ObjectPooling<EnemyHealth>[] enemyPool;
    [SerializeField] GameObject[] enemyGroup;
    public bool isSpawn ;
    public int curEnemyIndex = 0;
    public GameObject spawnPosition;


    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;


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
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        if (isSpawn)
        {
            isSpawn = false;
            EnemyHealth enemy = enemyPool[curEnemyIndex].GetOrCreate();
            enemyGroup[curEnemyIndex].transform.position = spawnPosition.transform.position;
            if (curEnemyIndex >= enemyGroup.Length - 1)
            {
                curEnemyIndex = 0;
            }
            else
            {
                curEnemyIndex++;
            }

        }

    }



}
