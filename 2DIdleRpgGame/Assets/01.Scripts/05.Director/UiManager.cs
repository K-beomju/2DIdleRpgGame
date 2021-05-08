using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Text goldText;
    public Text dungeonCountText;
    public Text stageCountText;
    public Button[] buttons;
    public GameObject[] panels;



    public Slider slider;
    public Text Timetext;
    public float gametime;


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
            GameManager.instance.isFadeInOut = true;
            GameManager.instance.stageCount++;
        }
        stageCountText.text = ($"{GameManager.instance.stageCount.ToString()}스테이지   {GameManager.instance.stageMobCount.ToString()}/{ GameManager.instance.allStageMobCount.ToString()}");
    }

    public void GoldCount()
    {
        goldText.text = GameManager.instance.Gold.ToString();
    }

    public void MainButton(int i)
    {
        for (int temp = 0; temp < buttons.Length; temp++)
        {
            buttons[temp].interactable = true;
            panels[temp].gameObject.SetActive(false);
        }
        buttons[i].interactable = false;
        panels[i].gameObject.SetActive(true);
    }

    void Update()
    {
        Quest();
    }

    public void Quest()
    {
        float time = gametime - Time.time;
        int hours = Mathf.FloorToInt(time / 3600);
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time - minutes * 60f);

        string textTime = ($"{hours:D2}:{minutes:D2}:{(int)seconds:D2}");




        // 자 시간을 뺄거야 만약에 2초에서 초당 1초씩 까인다면 second가 0보다 작아질 경우


    }


}
