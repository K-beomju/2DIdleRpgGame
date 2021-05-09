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
    public GameObject[] lockObjs;


    [Header("Timer")]
    public Slider slider;
    public Text Timetext;
    public float gametime;

    [Header("Quest")]
    public Text questLvTxt; //퀘스트 1 레벨 텍스트
    public Text getGoldTxt; // 얻는 골드 텍스트
    public Text upgradedTxt; // 업그레이드 텍스트
    public int questLevel; //퀘스트 1  레벨 변수
    public float getGold; // 얻는 골드 변수
    public float upgradedGold; // 업그레이드 골드 변수
    private int setGold; // 업그레이드하면 받는 골드 증가



    bool Quest1 = false; // 퀘스트 잠금



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        setGold = 20;
        questLevel = 0;
        getGold = 10;
        upgradedGold = 10;
        upgradedTxt.text = ($"{upgradedGold.ToString()}");
        getGoldTxt.text = ($"{getGold.ToString()}");


        slider.maxValue = gametime;
        slider.value = gametime;

        dungeonCountText.text = ($"첫번째 던전 {GameManager.instance.dungeonCount.ToString()}");
        stageCountText.text = ($"{GameManager.instance.stageCount.ToString()}스테이지   {GameManager.instance.stageMobCount.ToString()}/{ GameManager.instance.allStageMobCount.ToString()}");

        goldText.text = GameManager.instance.Gold.ToString();
    }


    void Update()
    {
        if (Quest1)
        {
            Quest();
        }

    }

    public void LockQuest_1()
    {

        if (GameManager.instance.Gold >= 10)
        {

            if (!Quest1)
            {
                questLevel++; // 레벨 올려주고
                upgradedGold++; // 업그레이드 골드 올려주고
                GameManager.instance.Gold -= 10; // 잠금 해제 비용 차감
                lockObjs[0].SetActive(false); // 락 오브젝트 비활성화
                GoldCount(); // 골드 표시
                Quest1 = true; // 퀘스트 잠금 해제
            }
            else
            {
                if (GameManager.instance.Gold >= upgradedGold)
                {
                    GameManager.instance.Gold -= (long)upgradedGold; // 업그레이드 비용 만큼 빼주고

                    GoldCount(); // 골드 카운트 한번 해주고
                    questLevel++; // 퀘스트 레벨 1 올려주고
                    getGold += setGold; // 받는 골드 얻는 골드만큼 다시 올려줌
                    upgradedGold += 2; // 업그레이드 골드 변수 증가


                }
            }

        }

    }

    public void Quest()
    {

        gametime -= Time.deltaTime;

        int hours = Mathf.FloorToInt(gametime / 3600);
        int minutes = Mathf.FloorToInt(gametime / 60);
        int seconds = Mathf.FloorToInt(gametime - minutes * 60f);

        string textTime = ($"{hours:D2}:{minutes:D2}:{(int)seconds:D2}");

        Timetext.text = textTime;
        slider.value = gametime;

        if (gametime < 0)
        {
            GameManager.instance.Gold += (long)getGold;
            GoldCount();
            gametime = 3;
        }

        questLvTxt.text = ($"LV.{questLevel.ToString()}");
        getGoldTxt.text = ($"{getGold.ToString()}");
        upgradedTxt.text = ($"{upgradedGold.ToString()}");

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

}
