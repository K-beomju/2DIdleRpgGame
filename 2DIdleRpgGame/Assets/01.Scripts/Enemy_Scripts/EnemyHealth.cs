using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : LivingEntity
{
    public float hp;
   void Start()
    {
        SetEntityDefault(hp);
    }


}
