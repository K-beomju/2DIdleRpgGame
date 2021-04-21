using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UiManager : MonoBehaviour
{


    public Text dadJokeTxt;
    public string[] tlqkf = { "이승핵", "남상현", "최명훈" };
    int i = 0;
    float countTime = 0;
    public float delayTime = 4f;
    void Update()
    {
        if (countTime < Time.time)
        {
            if (tlqkf.Length > i)
            {
                dadJokeTxt.text = string.Format("{0}", tlqkf[i]);
                countTime = Time.time + 4;
                i++;
            }
            else
            {
                i = 0;
            }
        }
    }


}
