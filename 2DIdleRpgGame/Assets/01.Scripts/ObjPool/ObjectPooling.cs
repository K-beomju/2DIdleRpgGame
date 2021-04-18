using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
  private static ObjectPooling instance;
   public static ObjectPooling Instance
   {
       get
       {
           return instance;
       }
   }

    private void Awake()
   {
       if(instance)
       {
           Destroy(gameObject);
           return;
       }
       instance = this;
   }

     //적 프리팹
    public EnemyMove poolObj_Goblin;
    public EnemyMove poolObj_Skeleton;
    public EnemyMove poolObj_Bat;
    [Space(30)]

    //적 Pool
    public List<EnemyMove> Enemys = new List<EnemyMove>();
    //내가 생성할 적들의 갯수
    private readonly int EnemyCount = 3;

    [Space(30)]
    //현재 적 인덱스
    public int curEnemyIndex = 0;

    void Start()
    {
          isCreate = true;
        for(int i = 1; i < EnemyCount; ++i)
        {
            if(i % 2 == 1)
            {
            EnemyMove b = Instantiate<EnemyMove>(poolObj_Goblin);
            b.gameObject.SetActive(false);
            Enemys.Add(b);
            }
             if(i % 2 == 0)
            {
            EnemyMove c = Instantiate<EnemyMove>(poolObj_Skeleton);
            c.gameObject.SetActive(false);
            Enemys.Add(c);
            }
           else
            {
            EnemyMove d = Instantiate<EnemyMove>(poolObj_Bat);
            d.gameObject.SetActive(false);
            Enemys.Add(d);
            }

            //적이 생성되기전 까지는 비활성화 해준다.
        }
    }

    void Update()
    {
        FireBulet();
    }


     Vector2 Vec = new Vector3(3f, 2.5f); // 생성 위치
    public bool isCreate;

    //총알 발사
    void FireBulet()
    {
        if (isCreate)
        {
              isCreate = false;

            //활성화한 적이 아직 죽지 않았다면 리턴
            if(Enemys[curEnemyIndex].gameObject.activeSelf)
            {
                return;
            }

            // 적의 스폰 위치
            Enemys[curEnemyIndex].transform.position = Vec;

            // 활성
            Enemys[curEnemyIndex].gameObject.SetActive(true);

            // 마지막 적 소환했다면 다시 첫번째로
            if (curEnemyIndex >= EnemyCount - 1)
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


