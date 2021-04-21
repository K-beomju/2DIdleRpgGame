using UnityEngine;


[CreateAssetMenu(fileName = "New SkillData", menuName = "Scriptable Object/Skill Data", order = int.MinValue)]
public class SkillHub : ScriptableObject
{
    [SerializeField]

    private string skillName;

    public string SkillName { get { return SkillName; } }

    [SerializeField]

    private int damage;

    public int Damage { get { return damage; } }


}



