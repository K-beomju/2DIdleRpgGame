using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poolable : MonoBehaviour,IDamgeable
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

    public float hp = 2;
     public void OnDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            SpawnManager.isCreate = true;
            Debug.Log("적 죽음");
            Push();
        }

    }

}
