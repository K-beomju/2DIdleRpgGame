using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RectTransform canvas;
    public GameObject hpBarPrefab;
    public CameraEffect camEffect;
    public BackGround back;

    [Header ("Player")]
    public float attackDamage; // 플레이어 공격력, 공격범위
    public float attackRange;

    [Header ("Enemy")]
    public float enemyAttackRange = 0.5f;
    public float enemyAttackDamage = 1f;
    public float enemyMoveSpeed = 1f;

    public float backSpeed = 0.5f;



    private ObjectPooling<EnemyHPBar> barPool;

    void Awake()
    {
        instance = this;
        barPool = new ObjectPooling<EnemyHPBar>(hpBarPrefab, canvas, 3); //hp바는 3개면 충분
    }
    void Start()
    {

    }



































    public static void SetBackgroundSpeed(float speed){
        instance.back.SetSpeed(speed); //객체지향에서 개체내의 데이터를 다룬곳에서 다루면 코드의 복잡도를 증가시킨다.
    }

    //적객체의 HP바가 필요하면
    public static EnemyHPBar GetEnemyHPBar()
    {
        return instance.barPool.GetOrCreate();
    }

    public static void CamShake(float intense, float during)
    {
        instance.camEffect.SetShake(intense, during);
    }



}
