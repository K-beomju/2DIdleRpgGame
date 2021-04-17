using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemy;
    public List<Enemy> enemyList = new List<Enemy>();
    Vector3 Vec = new Vector3(3f, 3f, 0f); // 생성 위치
    bool spawnEnemies;
    private float spawnEnemyTimes;
    void Start()
    {

    }

    void Update()
    {
        if(spawnEnemyTimes < Time.time)
        {
            InstantiateEnemy();
            spawnEnemyTimes = Time.time + 3f;
        }

    }
    void InstantiateEnemy()
    {
        Enemy myEnemy = Instantiate(enemy, Vec, Quaternion.identity).GetComponent<Enemy>();
        enemyList.Add(myEnemy);

    }
   public void Dead(Enemy _enemy)
    {
        enemyList.Remove(_enemy);
    }
}
