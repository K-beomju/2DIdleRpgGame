using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public RectTransform canvas;

    public GameObject hpBarPrefab; //     private ObjectPooling<EnemyHPBar> barPool;
    public GameObject hitEffect; //  public ObjectPooling<SkillObject> hitPool;
    public GameObject textObj;
    public GameObject dropGold;

    [Space(45)]
    public CameraEffect camEffect;
    [SerializeField]
    private BackGround back;
    [Space(20)]

    [Header("Player")]
    public float attackDamage; // 플레이어 공격력, 공격범위
    public float attackRange;
    public float attackSpeed;
    public float attackDelay;

    [Header("Enemy")]
    public float enemyAttackRange = 0.5f;
    public float enemyAttackDamage = 1f;
    public float enemyMoveSpeed;
    public float _enemyMoveSpeed;
    public float enemyBossSize;

    [Header("BackGround")]
    public float backSpeed;

    [Header ("GameSystem")]
    public int dungeonCount; // @번째 던전
    public int stageCount; // @스테이지
    public int stageMobCount; // {0}
    public int allStageMobCount; // {1}


    private ObjectPooling<EnemyHPBar> barPool;
    public ObjectPooling<SkillObject> hitPool;
    public ObjectPooling<DamageText> dmgPool;
    public ObjectPooling<DropGold> dropPool;

    private long gold;
    public long Gold { get {return gold ;} set { gold = value;} }




    void Awake()
    {
        Gold = 20;
        dungeonCount = 1;
        stageCount = 1;
        stageMobCount = 1;
        allStageMobCount = 10;



        instance = this;
        barPool = new ObjectPooling<EnemyHPBar>(hpBarPrefab, canvas, 3);
        hitPool = new ObjectPooling<SkillObject>(hitEffect, this.transform, 10);
        dmgPool = new ObjectPooling<DamageText>(textObj, this.transform, 10);
        dropPool = new ObjectPooling<DropGold>(dropGold, this.transform, 8);

    }

    void Update()
    {

    }

    public static void SetBackgroundSpeed(float speed)
    {
        instance.back.SetSpeed(speed); //객체지향에서 개체내의 데이터를 다룬곳에서 다루면 코드의 복잡도를 증가시킨다.
    }

    public static EnemyHPBar GetEnemyHPBar()
    {
        return instance.barPool.GetOrCreate();
    }

    public static DamageText GetDamageText()
    {
        return instance.dmgPool.GetOrCreate();
    }

    public static DropGold GetDropGold()
    {
        return instance.dropPool.GetOrCreate();
    }


    public static void CamShake(float intense, float during)
    {
        instance.camEffect.SetShake(intense, during);
    }



}
