using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable  {
    void OnDamage(float damage, bool isPushAttack = false);
    //�ɼų� �Ķ���͸� ����ؼ� �⺻ false�� ���Ƽ� ������ �ڵ忡�� ������ ������ �Ѵ�.

}