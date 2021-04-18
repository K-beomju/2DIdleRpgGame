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
      hp = 10f;
   }

    public float hp;

    void Start()
    {
        hp = 10;
    }


     public void OnDamage(float damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
           gameObject.SetActive(false);
            ObjectPooling.Instance.isCreate = true;

        }

    }

}
