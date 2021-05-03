using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DropGold : MonoBehaviour
{

    public float animDuration;
    public Ease ease;

    private  Rigidbody2D rigid;
    private SpriteRenderer sr;
    private float alpha = 1;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
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
        transform.DOMove(new Vector2(-2.6f, 1.6f), animDuration).SetEase(ease).SetDelay(0.7f);
        rigid.AddForce(new Vector2(Random.Range(-150, -90), Random.Range(140, 200)));
    }
    private IEnumerator SetDeactive()
    {
        sr.color = new Color(1, 1, 1, alpha);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        alpha = 1f;
    }



}
