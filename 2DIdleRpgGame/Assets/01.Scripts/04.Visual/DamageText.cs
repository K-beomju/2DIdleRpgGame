using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
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
        text.text = GameManager.instance.attackDamage.ToString();
        transform.DOMoveY(3f, animDuration).SetEase(ease);
        StartCoroutine(SetDeactive());
    }

    private IEnumerator SetDeactive()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

    }


}
