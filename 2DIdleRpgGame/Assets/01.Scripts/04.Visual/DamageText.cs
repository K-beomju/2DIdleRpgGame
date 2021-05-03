using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class DamageText : MonoBehaviour
{


    public static TextMeshPro text;
    public static float damage;
    public float animDuration;
    public Ease ease;
    private Color alpha;

    void Start()
    {

        text = GetComponent<TextMeshPro>();
        text.text = damage.ToString();


    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
        }
    }

    void OnEnable()
    {

            transform.DOMoveY(3f, animDuration).SetEase(ease);
            StartCoroutine(SetDeactive());
    }

    private IEnumerator SetDeactive()
    {
        yield return new WaitForSeconds(0.3f);
        gameObject.SetActive(false);

    }


}
