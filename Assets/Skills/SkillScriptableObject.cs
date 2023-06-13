using System;
using UnityEngine;

public class SkillScriptableObject : SkillBase, INameableGUIDableDescribable
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public BaseSkillData BaseSkillData { get; private set; }
    [field: SerializeField]
    public bool IsDamageSkill { get; private set; }
    [field: SerializeField]
    public DamageSkillData DamageData { get; private set; }
    [field: SerializeField]
    public string GUID { get; set; } = Guid.NewGuid().ToString();
}
