using UnityEngine;


[CreateAssetMenu(fileName = "New SkillData", menuName = "Scriptable Object/Skill Data", order = int.MinValue)]
public class SkillHub : ScriptableObject
{
    [SerializeField]
    private float damage;
    public float Damage { get { return damage; } }


    [SerializeField]
    private float range;
    public float Range { get { return range; } }

     [SerializeField]
    private float attackCount;
    public float AttackCount { get { return attackCount; } }

}



