using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public delegate void UpChLevelStatus(float value);
    public UpChLevelStatus Ucls;


    public RectTransform canvas;
    public Button[] etImage;
    public Transform goldTxt;

    [Header("Pooling Objs")]
    public GameObject hpBarPrefab;
    public GameObject hitEffect;
    public GameObject textObj;
    public GameObject dropGold;
    public GameObject textGold;
    public GameObject questEffect;

    [Header("Player")] [Space(20)]
    public float attackDamage;
    public float attackCriticalDamage;
    public float attackSpeed;
    public float attackRange;
    public float attackDelay;
    public float critical; // 크리티컬
    public int chLevel; // 캐릭터 레벨
    public bool isCritical;


    [Header("Enemy")]  [Space(20)]
    public float enemyMoveSpeed;
    public float _enemyMoveSpeed;
    public float enemyBossSize;
    private float enemyBossHealth;
    public float EnemyBossHealth { get { return enemyMaxHealth * 5; } }
    public float enemyMaxHealth {get; set; }
    public float enemyHealth;
    public long enemyGold;


    [Header("GameSystem")]  [Space(20)]
    public int dungeonCount; // @번째 던전
    public int stageCount; // @스테이지s
    public int stageMobCount; // {0}
    public int allStageMobCount; // {1}
    public bool isFadeInOut; // 페이드 아웃


    // [SerializeField]
    // private long gold;
    // public long Gold { get { return gold; } set { gold = value; } } // 보류
    public BigInteger gold;



    [Header("SpawnSystem")]  [Space(20)]
    public int curEnemyIndex = 0;
    public bool isSpawn;
    public bool isBoss = false;
    public Transform spawnPosition; // 스폰 포인트


    [Header("Text")]  [Space(20)]
    public float destinatinon;



    [Header("Etc")]  [Space(20)]
    public CameraEffect camEffect;
    [SerializeField]
    private BackGround back;

    //Status
    private float up1ChLevel;
    public float Up1ChLevel { get { return up1ChLevel; } set { up1ChLevel = value; } }

    private float up10ChLevel;
    public float Up10ChLevel { get { return up10ChLevel; } set { up10ChLevel = value; } }

    private float up100ChLevel;
    public float Up100ChLevel { get { return up100ChLevel; } set { up100ChLevel = value; } }


    private ObjectPooling<EnemyHPBar> barPool;
    private ObjectPooling<DamageText> dmgPool;
    private ObjectPooling<DropGold> dropPool;
    private ObjectPooling<GoldText> goldPool;
    private ObjectPooling<SkillObject> hitPool;
    private ObjectPooling<SkillObject>[] uEtPool;





    void Awake()
    {
        instance = this;
        barPool = new ObjectPooling<EnemyHPBar>(hpBarPrefab, canvas, 3);
        hitPool = new ObjectPooling<SkillObject>(hitEffect, this.transform, 10);
        dmgPool = new ObjectPooling<DamageText>(textObj, this.transform, 10);
        dropPool = new ObjectPooling<DropGold>(dropGold, this.transform, 8);
        goldPool = new ObjectPooling<GoldText>(textGold, this.transform, 8);
        uEtPool = new ObjectPooling<SkillObject>[etImage.Length];
        for (int i = 0; i < etImage.Length; i++)
        {
         uEtPool[i] = new ObjectPooling<SkillObject>(questEffect, this.etImage[i].transform, 3 );
        }

        isSpawn = true;
        isBoss = false;
        isFadeInOut = false;
        isCritical = false;
        dungeonCount = 1;
        stageCount = 1;
        stageMobCount = 1;
        destinatinon = 1;
        allStageMobCount = 10;
        attackCriticalDamage = 1.5f;
        critical = 1;
        chLevel = 1;
        gold = 10;
        up1ChLevel = 524;

        Ucls += UpSetStatus;
        Ucls(up1ChLevel);
    }

    public void UpSetStatus(float value)
    {
        up10ChLevel = 0;
        up100ChLevel = 0;
        for (int i = 0; i <= 9; i++)
        {
            up10ChLevel += value + 26*i;
        }
        for (int i = 0; i <= 99; i++)
        {
            up100ChLevel += value + 26*i;
        }
    }



    public static void SetBackgroundSpeed(float speed)
    {
        instance.back.SetSpeed(speed); //객체지향에서 개체내의 데이터를 다룬곳에서 다루면 코드의 복잡도를 증가시킨다.
    }

    public static EnemyHPBar GetEnemyHPBar() // 적 HP bar
    {
        return instance.barPool.GetOrCreate();
    }

    public static DamageText GetDamageText()
    {
        return instance.dmgPool.GetOrCreate();
    }

    public static GoldText GetGoldText()
    {
        return instance.goldPool.GetOrCreate();
    }

    public static DropGold GetDropGold()
    {
        return instance.dropPool.GetOrCreate();
    }

    public static SkillObject GetHitEffect()
    {
        return instance.hitPool.GetOrCreate();
    }

    public static SkillObject GetQsEffect(int i)
    {
        return instance.uEtPool[i].GetOrCreate();
    }


    // public static void CamShake(float intense, float during)
    // {
    //     instance.camEffect.SetShake(intense, during);
    // }





}
