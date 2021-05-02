using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dotween : MonoBehaviour
{

    [SerializeField] Transform target;
   public float animDuration;
   public Ease ease;
    Rigidbody2D rigid;

    void Awake()
    {

        rigid = GetComponent<Rigidbody2D>();


    }
   void Start()
   {

        rigid.AddForce(new Vector2(-160,100));

        transform.DOMove(target.transform.position, animDuration).SetEase(ease).SetDelay(0.7f);

   }
   void Update()
   {

   }
}
