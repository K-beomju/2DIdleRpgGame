using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable  {
    void OnDamage(float damage, bool isPushAttack = false);
    //옵셔널 파라메터를 사용해서 기본 false로 놓아서 기존의 코드에는 영향이 없도록 한다.

}