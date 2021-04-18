using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGround : MonoBehaviour
{
    private static BackGround instance;
   public static BackGround Instance
   {
       get
       {
           return instance;
       }
   }

    private void Awake()
   {
       if(instance)
       {
           Destroy(gameObject);
           return;
       }
       instance = this;
   }



}
