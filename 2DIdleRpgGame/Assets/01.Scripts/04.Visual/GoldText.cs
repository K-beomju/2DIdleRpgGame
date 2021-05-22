using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    private TextMeshPro text;
    private float animDuration = 1;
    private float alpha = 1;
    public Ease ease;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        text = GetComponent<TextMeshPro>();
        text.text = ($"+{GameManager.instance.enemyGold}");
        transform.DOMoveY(1.8f, animDuration).SetEase(ease);

    }

    void Update()
    {
        if(gameObject.activeSelf)
        {
           StartCoroutine(SetDeactive());
        }
    }

    private IEnumerator SetDeactive()
    {
        alpha -= Time.deltaTime * 2f;
        text.color = new Color(251, 255, 0, alpha);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        alpha = 1f;
    }


}
