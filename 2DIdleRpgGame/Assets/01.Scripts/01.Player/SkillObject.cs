using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{


    //public string skillName; // 스킬 이름
   // public float damage; // 스킬 데미지
    //  [SerializeField]
    // private float m_range; // 스킬 사거리
    //  public float range { get { return m_range; } } // 외부에서 set이 불가능함
   // public int range;



    public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

     public void SetDeactive()
     {
         gameObject.SetActive(false);
     }
}
