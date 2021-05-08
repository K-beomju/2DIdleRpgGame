using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    public TextMeshPro damageText {get; set;}
    private float animDuration = 1;
    public Ease ease;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        damageText = GetComponent<TextMeshPro>();
        transform.DOMoveY(GameManager.instance.destinatinon, animDuration).SetEase(ease);
        StartCoroutine(SetDeactive());
    }

    private IEnumerator SetDeactive()
    {
        if(GameManager.instance.isCritical)
        {
            damageText.color = Color.red;
            damageText.fontSize = 3f;
            GameManager.instance.isCritical = false;
        }
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);
        damageText.color = Color.white;
        damageText.fontSize = 2.66f;

    }


}
