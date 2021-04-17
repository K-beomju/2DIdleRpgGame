using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   private static PoolManager instance;
   public static PoolManager Instance
   {
       get
       {
           return instance;
       }
   }

   public ObjPool pool;

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
