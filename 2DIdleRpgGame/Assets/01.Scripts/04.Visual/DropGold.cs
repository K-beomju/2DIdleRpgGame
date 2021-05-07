using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropGold : MonoBehaviour
{

    private float animDuration =1;
    private float alpha = 1;

    public Ease ease;
    private Rigidbody2D rigid;
    private SpriteRenderer sr;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            alpha -= Time.deltaTime * 1.5f;
            StartCoroutine(SetDeactive());

        }
    }



    void OnEnable()
    {
        rigid.AddForce(new Vector2(Random.Range(-150, -90), Random.Range(140, 200)));
        transform.DOMove(new Vector2(-2.6f, 1.6f), animDuration).SetEase(ease).SetDelay(0.7f);
    }
    private IEnumerator SetDeactive()
    {
        sr.color = new Color(1, 1, 1, alpha);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        alpha = 1f;
    }



}
