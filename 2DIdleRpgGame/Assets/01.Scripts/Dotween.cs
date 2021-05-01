using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dotween : MonoBehaviour
{
   public float animDuration;
   public Ease ease;

   void Start()
   {
       transform.DOMoveY(3f, animDuration).SetEase(ease);
   }
}
