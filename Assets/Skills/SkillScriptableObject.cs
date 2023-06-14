using System;
using Tooltips;
using UnityEngine;

public class SkillScriptableObject : SkillBase, INameableGUIDableDescribableTooltipable
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
    public TooltipType TooltipType { get; protected set; } = TooltipType.ABILITY;
}
