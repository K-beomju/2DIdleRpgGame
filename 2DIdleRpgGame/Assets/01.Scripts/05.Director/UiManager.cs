using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    [SerializeField] Text goldText;
    [SerializeField] Text dungeonCountText;
    [SerializeField] Text stageCountText;

    void Start()
    {
        goldText.text = GameManager.instance.Gold.ToString();
        dungeonCountText.text = string.Format("첫번째 던전 {0}", GameManager.instance.dungeonCount);

        stageCountText.text = string.Format("{0}스테이지   {1}/{2}", GameManager.instance.stageCount,
        GameManager.instance.stageMobCount,GameManager.instance.allStageMobCount);
    }
}
