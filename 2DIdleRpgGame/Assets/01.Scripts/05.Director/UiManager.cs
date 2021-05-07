using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] Text goldText;
    [SerializeField] Text dungeonCountText;
    [SerializeField] Text stageCountText;
    [SerializeField] Image fadeOutImage;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        goldText.text = GameManager.instance.Gold.ToString();

        dungeonCountText.text = ($"첫번째 던전 {GameManager.instance.dungeonCount.ToString()}");
        stageCountText.text = ($"{GameManager.instance.stageCount.ToString()}스테이지   {GameManager.instance.stageMobCount.ToString()}/{ GameManager.instance.allStageMobCount.ToString()}");

    }

    public void DungeonCount()
    {
        GameManager.instance.dungeonCount++;
        dungeonCountText.text = ($"첫번째 던전 {GameManager.instance.dungeonCount.ToString()}");
    }

    public void StageCount()
    {
        GameManager.instance.stageMobCount++;
        if (GameManager.instance.stageMobCount > GameManager.instance.allStageMobCount)
        {
            GameManager.instance.stageMobCount = 1;
            GameManager.instance.enemyMaxHealth *= 2f; // 스테이지 up -> 적 체력 증가 - > 적 보스 체력 증가
            GameManager.instance.bFadeInOut = true;
            GameManager.instance.stageCount++;
        }
        stageCountText.text = ($"{GameManager.instance.stageCount.ToString()}스테이지   {GameManager.instance.stageMobCount.ToString()}/{ GameManager.instance.allStageMobCount.ToString()}");
    }

    public void GoldCount()
    {
        goldText.text = GameManager.instance.Gold.ToString();
    }

}
