using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class SpawnManager : MonoBehaviour
{
    private ObjectPooling<EnemyHealth>[] enemyPool;
    private EnemyHealth enemy;
    public GameObject[] enemyGroup;
    public Transform spawnPosition;




    void Awake()
    {
        enemyPool = new ObjectPooling<EnemyHealth>[enemyGroup.Length];
        for (int i = 0; i < enemyGroup.Length; i++)
        {
            enemyPool[i] = new ObjectPooling<EnemyHealth>(enemyGroup[i], this.transform, 1);
        }

    }

    void Update()
    {
        if (GameManager.instance.isSpawn)
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

        GameManager.instance.isSpawn = false;
        enemy = enemyPool[GameManager.instance.curEnemyIndex].GetOrCreate();
        enemy.transform.position = spawnPosition.position;
        GameManager.instance.destinatinon = enemy.transform.position.y + 0.7f;
        GameManager.instance.enemyMaxHealth = GameManager.instance.enemyHealth;


        if (GameManager.instance.curEnemyIndex >= enemyGroup.Length - 1)
        {
            GameManager.instance.curEnemyIndex = 0;
        }
        else
        {
            GameManager.instance.curEnemyIndex++;
        }

    }

    void SpawnBoss()
    {

        GameManager.instance.isBoss = true;
        GameManager.instance.isSpawn = false;
        enemy = enemyPool[Random.Range(0, enemyGroup.Length - 1)].GetOrCreate();
        enemy.transform.position = spawnPosition.position;
        GameManager.instance.destinatinon = enemy.transform.position.y + 0.7f;
        enemy.transform.localScale *= GameManager.instance.enemyBossSize;
        GameManager.instance.enemyHealth = GameManager.instance.EnemyBossHealth;

    }


}
