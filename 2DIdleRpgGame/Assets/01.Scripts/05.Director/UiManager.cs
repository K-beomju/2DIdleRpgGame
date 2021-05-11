using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;



public class UiManager : MonoBehaviour
{
    [Serializable]
    public struct QuestStruct
    {
        [Header("Timer")]
        public Slider slider;
        public Text Timetext;
        public Button button;
        public float gametime;

        [Header("Quest")]
        public Text questLvTxt; //퀘스트 1 레벨 텍스트
        public Text getGoldTxt; // 얻는 골드 텍스트
        public Text upgradedTxt; // 업그레이드 텍스트
        public Text upgradedGoldTxt;
        public int lockQuest; // 잠금 해제 골드
        public int questLevel; //퀘스트 1  레벨 변수
        public int setGold; // 업그레이드하면 받는 골드 증가
        public float upgradedGold; // 업그레이드 골드 변수
        public float getGold; // 얻는 골드 변수
        public float onUpGold;
        private float _gametime;
        public bool Quest; // 퀘스트 잠금
        public int lockCount;

        public void Set()
        {
            Quest = false;
            upgradedTxt.text = ($"{upgradedGold.ToString()}");
            getGoldTxt.text = ($"{getGold.ToString()}");
            upgradedGoldTxt.text = ($"+{lockQuest.ToString()}");
            slider.maxValue = gametime;
            slider.value = gametime;
            _gametime = gametime;
        }

        public void LockQuest_1()
        {
            if (GameManager.instance.Gold >= lockQuest)
            {

                if (!Quest)
                {
                    questLevel++;
                    upgradedGold += onUpGold;
                    GameManager.instance.Gold -= lockQuest;
                    UiManager.instance.lockObjs[lockCount].SetActive(false);
                    UiManager.instance.GoldCount();
                    Quest = true;
                }
                else
                {
                    if (GameManager.instance.Gold >= upgradedGold)
                    {

                        GameManager.instance.Gold -= (long)upgradedGold;
                        UiManager.instance.GoldCount();
                        questLevel++;
                        getGold += setGold;
                        upgradedGold += onUpGold;

                    }


                }

            }

        }


        public void QuestGo()
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
                UiManager.instance.GoldCount();
                gametime = _gametime;
            }

            if (GameManager.instance.Gold < upgradedGold)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }

            questLvTxt.text = ($"LV.{questLevel.ToString()}");
            getGoldTxt.text = ($"{getGold.ToString()}");
            upgradedTxt.text = ($"{upgradedGold.ToString()}");
        }
    }
    public QuestStruct[] questStructs;




    public static UiManager instance;
    [Space(45)]

    public Text goldText;
    public Text dungeonCountText;
    public Text stageCountText;[Space(45)]
    public Button[] buttons;
    public GameObject[] panels;
    public GameObject[] lockObjs;



    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        for (int i = 0; i < questStructs.Length; i++)
        {

            questStructs[i].Set();
        }
    }

    void Start()
    {
        dungeonCountText.text = ($"첫번째 던전 {GameManager.instance.dungeonCount.ToString()}");
        stageCountText.text = ($"{GameManager.instance.stageCount.ToString()}스테이지   {GameManager.instance.stageMobCount.ToString()}/{ GameManager.instance.allStageMobCount.ToString()}");
        goldText.text = GameManager.instance.Gold.ToString();
    }


    void Update()
    {
        for (int i = 0; i < questStructs.Length; i++)
        {
            if (questStructs[i].Quest)
            {
                questStructs[i].QuestGo();
            }
        }

    }
    public void LockQuest(int i)
    {
        questStructs[i].LockQuest_1();
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
