using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{

    public static TextMeshPro text;
    public Ease ease;
    private Color alpha;


    public float animDuration;


    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        text = GetComponent<TextMeshPro>();
        text.text = ($"+{GameManager.instance.enemyGold}");
        transform.DOMoveY(2f, animDuration).SetEase(ease);
        StartCoroutine(SetDeactive());
    }

    private IEnumerator SetDeactive()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

    }


}
