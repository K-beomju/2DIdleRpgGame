using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Numerics;
using TMPro;



public class UiManager : MonoBehaviour
{
    #region 퀘스트 UI
    [Serializable]
    public struct QuestStruct
    {
        [Header("Timer")]
        public Slider slider;
        public Text Timetext;
        public Button button;
        public float gametime;
        private float _gametime;



        [Header("Text")]
        public Text questLvTxt; //퀘스트 1 레벨 텍스트
        public Text getGoldTxt; // 얻는 골드 텍스트
        public Text upgradedTxt; // 업그레이드 텍스트
        public Text upgradedGoldTxt;


        [Header("variable")]
        public int lockCount; // 퀘스트 잠금 obj
        public float getGold; // 얻는 골드 변수
        private float  _getGold;
        public float upgradedGold; // 업그레이드 골드 변수

        [HideInInspector]
        public bool Quest; // 퀘스트 잠금
        private int questLevel; //퀘스트 1  레벨 변수
        private float lockQuest {get {return getGold / 2;}} // 잠금 해제 골드





        public void Set()
        {
            Quest = false;
            questLevel = 0;
            UiManager.instance.GoldCount(upgradedTxt, (BigInteger)upgradedGold);
            UiManager.instance.GoldCount(getGoldTxt, (BigInteger)getGold);
            UiManager.instance.GoldCount(upgradedGoldTxt, (BigInteger)lockQuest);
            slider.maxValue = gametime;
            slider.value = gametime;
            _gametime = gametime;
            _getGold = getGold;
        }

        public void LockQuest_1()
        {
            if (GameManager.instance.gold >= (BigInteger)upgradedGold)
            {
                upgradedGold *= 1.12f;
                questLevel++;

                if (!Quest)
                {
                    GameManager.instance.gold -= (BigInteger)upgradedGold;
                    UiManager.instance.lockObjs[lockCount].SetActive(false);
                    Quest = true;
                }
                else
                {
                    if (GameManager.instance.gold >= (BigInteger)upgradedGold)
                    {
                        GameManager.instance.gold -= (long)upgradedGold;
                        getGold += Mathf.Round(_getGold / 2);



                    }


                }
                UiManager.instance.GoldCount(UiManager.instance.goldText, GameManager.instance.gold);

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
                GameManager.instance.gold += (long)getGold;
                UiManager.instance.GoldCount(UiManager.instance.goldText, GameManager.instance.gold);
                gametime = _gametime;
            }

            if (GameManager.instance.gold < (BigInteger)upgradedGold)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }

            questLvTxt.text = ($"LV.{questLevel.ToString()}");
            UiManager.instance.GoldCount(getGoldTxt, (BigInteger)getGold);
            UiManager.instance.GoldCount(upgradedTxt, (BigInteger)upgradedGold);
        }


    }
    #endregion
    public QuestStruct[] questStructs;


    public static UiManager instance;

    [Space(45)]
    public Text goldText;
    public Text dungeonCountText;
    public Text stageCountText;[Space(45)]
    public Button[] buttons; // 캐릭터 , 펫 , 스킬 , 퀘스트 , 던전 , 상점 바꿔주는 버튼
    public GameObject[] panels;
    public GameObject[] lockObjs;
    private SkillObject effectQs;


    [Header("Dungeon")]
    public Slider st1Sldr;
    public Text dungeonPrTxt;

    [Header("Hpbar")]
    public Transform spawnPosition;




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
        GoldCount(goldText, GameManager.instance.gold);

        dungeonPrTxt.text = ($"진행도 {GameManager.instance.stageMobCount}%");
        st1Sldr.value = GameManager.instance.stageCount;
        st1Sldr.maxValue = 100;
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
        if(questStructs[i].Quest)
        {
            effectQs = GameManager.GetQsEffect(i);
        }
        questStructs[i].LockQuest_1();
    }


    public void GoldCount(Text valueText, BigInteger value)
    {
        valueText.text = ChangeMoney(value.ToString());
    }


    string ChangeMoney(string haveGold)
    {
        string[] unit = new string[] { "", "A", "B", "C", "D", "E", "F", "G", "H", "I" };
        int[] currentValue = new int[10];

        int index = 0;

        while (true)
        {
            string last4 = "";
            if (haveGold.Length >= 4)
            {
                last4 = haveGold.Substring(haveGold.Length - 4);
                int intLast4 = int.Parse(last4);

                currentValue[index] = intLast4 % 1000;

                haveGold = haveGold.Remove(haveGold.Length - 3);
            }
            else
            {
                currentValue[index] = int.Parse(haveGold);
                break;
            }

            index++;
        }


        if (index > 0)
        {
            int r = currentValue[index] * 1000 + currentValue[index - 1];
            return string.Format("{0:#.#}{1}", (float)r / 1000f, unit[index]);
        }

        return haveGold;
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

            st1Sldr.value = GameManager.instance.stageCount;
            dungeonPrTxt.text = ($"진행도 {GameManager.instance.stageCount}%");
        }
        stageCountText.text = ($"{GameManager.instance.stageCount.ToString()}스테이지   {GameManager.instance.stageMobCount.ToString()}/{ GameManager.instance.allStageMobCount.ToString()}");
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
