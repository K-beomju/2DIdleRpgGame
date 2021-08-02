using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class SkillUi : MonoBehaviour
{
    public Image skillImage;
    public Sprite[] sprites;
    public Image[] choiseImage;

    public Text skillNameText;
    public string[] skillNameStr;

    public Text skillDescriptionText;
    public string[] skillDescriptionStr;

    public Text energyText;
    public int[] energyValue;

    public Button mountBtn;
    private bool isMounting = false;



    void Start()
    {
        ChangeSkill(0);


    }

     public void ChangeSkill(int i)
    {
        for (int temp = 0; temp < sprites.Length; temp++)
        {
            choiseImage[temp].gameObject.SetActive(false);
        }
        skillNameText.text = skillNameStr[i];
        skillDescriptionText.text = skillDescriptionStr[i];
        energyText.text = energyValue[i].ToString();

        choiseImage[i].gameObject.SetActive(true);
        skillImage.sprite = sprites[i];
        MountSkill(i);
    }

    public void MountSkill(int i)
    {
        if(isMounting)
        {
            Debug.Log("장착");
           // mountBtn.colors.normalColor = Color.red;
            isMounting = false;

        }
        else if(!isMounting)
        {
            Debug.Log("장착 해제");
            isMounting = true;
        }

    }


}
