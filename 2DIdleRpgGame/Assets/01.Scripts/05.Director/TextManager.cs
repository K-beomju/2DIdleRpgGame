using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextManager : MonoBehaviour
{
    private static TextManager _instance = null;

    public static TextManager Instance {
        get {
            if(_instance == null) {

                _instance = GameObject.FindObjectOfType<TextManager>();

                if (_instance == null) {
                    Debug.LogError("There's no active TextManager Object");
                }
            }

            return _instance;
        }
    }


    private ObjectPooling<TmpText>[] tmpPool;
    public GameObject[] textTmp;

    void Awake()
    {
         tmpPool = new ObjectPooling<TmpText>[textTmp.Length]; //
        for(int i = 0; i < textTmp.Length; i++)
        {
            tmpPool[i] = new ObjectPooling<TmpText>(textTmp[i], this.transform, 10); //각 이펙트별로 3개씩만 생성
        }
    }



    void Update()
    {

    }



}
