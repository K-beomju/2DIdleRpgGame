using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{


    public static ObjectPooling instance;

    public GameObject enemyPrefabs = null;

    public Queue<GameObject> m_queue = new Queue<GameObject>();

    [SerializeField] int monsterCount;

    float spawnTime;

    Vector2 Vec = new Vector3(5f, 2.463f);

    void Start()
    {
        instance = this;

        for(int i = 0; i< monsterCount; i++)
        {
            GameObject t_object = Instantiate(enemyPrefabs, Vec, Quaternion.identity);
            m_queue.Enqueue(t_object);
            t_object.SetActive(false);
        }

    }

    public void InsertQueue(GameObject p_object) //반납
    {
        m_queue.Enqueue(p_object);
        p_object.SetActive(false);
    }

    public GameObject GetQueue()
    {
        GameObject t_object = m_queue.Dequeue();
        t_object.SetActive(true);
        return t_object;
    }

    void  Update()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {

    }

}


