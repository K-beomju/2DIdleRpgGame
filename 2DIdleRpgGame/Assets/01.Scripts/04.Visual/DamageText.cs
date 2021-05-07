using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{
    private TextMeshPro text;
    private Color alpha;
    private float animDuration = 1;
    public Ease ease;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        text = GetComponent<TextMeshPro>();
        text.text = GameManager.instance.attackDamage.ToString();
        transform.DOMoveY(GameManager.instance.destinatinon, animDuration).SetEase(ease);
        StartCoroutine(SetDeactive());
    }

    private IEnumerator SetDeactive()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

    }


}
