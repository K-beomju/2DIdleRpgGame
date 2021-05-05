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

    void Awake()
    {
        if(instance == null)
        {
         instance = this;
        }
    }

    void Start()
    {
        goldText.text = GameManager.instance.Gold.ToString();

        dungeonCountText.text = ($"첫번째 던전 {GameManager.instance.dungeonCount}");
        stageCountText.text = ($"{GameManager.instance.stageCount}스테이지   {GameManager.instance.stageMobCount}/{ GameManager.instance.allStageMobCount}");

    }

    public void DungeonCount()
    {
        GameManager.instance.dungeonCount++;
        dungeonCountText.text = ($"첫번째 던전 {GameManager.instance.dungeonCount}");
    }

    public void StageCount()
    {
        GameManager.instance.stageMobCount++;
        if(GameManager.instance.stageMobCount > GameManager.instance.allStageMobCount)
        {
            GameManager.instance.stageMobCount = 0;

            GameManager.instance.stageCount++;
        }
        stageCountText.text = ($"{GameManager.instance.stageCount}스테이지   {GameManager.instance.stageMobCount}/{ GameManager.instance.allStageMobCount}");
    }

}
