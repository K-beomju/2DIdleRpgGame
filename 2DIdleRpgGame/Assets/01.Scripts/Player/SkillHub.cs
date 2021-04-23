using UnityEngine;


[CreateAssetMenu(fileName = "New SkillData", menuName = "Scriptable Object/Skill Data", order = int.MinValue)]
public class SkillHub : ScriptableObject
{
    public string skillName;
    public float damage;


    [SerializeField]
    private float range;
    public float Range { get { return range; } }


}



