using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    //�̰� ���� ScriptableObject�� ������ �ϴ� ������ �𸣰ھ �� �� �� ���Ѵ�� ��ü�� �����Դµ�. 
    //�ƴ϶�� �ϰ� �� ����� �ɵ�
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

    //�ִϸ��̼��� ������ �ڵ����� ����� �Լ�
    public void SetDeactive()
    {
        gameObject.SetActive(false);
    }
}
