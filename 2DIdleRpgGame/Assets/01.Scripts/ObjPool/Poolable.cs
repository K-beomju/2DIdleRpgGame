using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour
{
   protected ObjPool pool;

   public virtual void Create(ObjPool pool)
   {
       this.pool = pool;
       gameObject.SetActive(false);
   }

   public virtual void Push()
   {
       pool.Push(this);
   }




}
