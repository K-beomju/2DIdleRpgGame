using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropGold : MonoBehaviour
{

    public float animDuration;
    public Ease ease;
    Rigidbody2D rigid;
    private SpriteRenderer sr;
    float alpha = 1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        rigid.AddForce(new Vector2(Random.Range(-140, -100), Random.Range(150,200)));
        transform.DOMove(new Vector2(-2.6f, 1.6f), animDuration).SetEase(ease).SetDelay(0.7f);

    }
    void Update()
    {
        alpha -= Time.deltaTime * 1.5f;
        sr.color = new Color(1, 1, 1, alpha);

    }


}
