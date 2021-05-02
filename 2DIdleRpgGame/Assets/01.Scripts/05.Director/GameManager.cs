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
    public float enemyMoveSpeed = 1f;

    [Header("BackGround")]
    public float backSpeed;


    public Text tlqkf;












    private ObjectPooling<EnemyHPBar> barPool;
    public ObjectPooling<SkillObject> hitPool;
    public ObjectPooling<DamageText> dmgPool;
    public ObjectPooling<DropGold> dropPool;


    void Awake()
    {
        instance = this;
        barPool = new ObjectPooling<EnemyHPBar>(hpBarPrefab, canvas, 3); //hp바는 3개면 충분
        hitPool = new ObjectPooling<SkillObject>(hitEffect, this.transform, 10);
        dmgPool = new ObjectPooling<DamageText>(textObj, this.transform, 10);
        dropPool = new ObjectPooling<DropGold>(dropGold, this.transform, 3);

    }

    public static void SetBackgroundSpeed(float speed)
    {
        instance.back.SetSpeed(speed); //객체지향에서 개체내의 데이터를 다룬곳에서 다루면 코드의 복잡도를 증가시킨다.
    }

    //적객체의 HP바가 필요하면
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
