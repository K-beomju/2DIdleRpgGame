using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "New QuestData", menuName = "Scriptable Object/Quest Data", order = int.MinValue)]
public class QuestHub : ScriptableObject
{
    public Text questLvTxt; //퀘스트 1 레벨 텍스트
    public Text getGoldTxt; // 얻는 골드 텍스트
    public Text upgradedTxt; // 업그레이드 텍스트

    public int questLevel; //퀘스트 1  레벨 변수
    public float getGold; // 얻는 골드 변수
    public float upgradedGold; // 업그레이드 골드 변수
    public int setGold; // 업그레이드하면 받는 골드 증가
    public bool Quest1 = false; // 퀘스트 잠금
}

