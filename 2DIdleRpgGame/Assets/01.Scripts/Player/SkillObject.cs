using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    //이걸 굳이 ScriptableObject로 만들어야 하는 이유를 모르겠어서 걍 내 가 편한대로 객체로 가져왔는데. 
    //아니라면 니가 걍 만들면 될듯
    public string skillName;
    public float damage;
    [SerializeField]
    private float m_range;
    public float range { get { return m_range; } }

    public void SetPositionData(Vector3 position, Quaternion rot)
    {
        transform.position = position;
        transform.rotation = rot;
    }

    //애니메이션이 끝나면 자동으로 실행될 함수
    public void SetDeactive()
    {
        gameObject.SetActive(false);
    }
}
