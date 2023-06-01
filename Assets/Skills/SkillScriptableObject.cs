using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillScriptableObject : SkillBase
{
    [field: SerializeField]
    public BaseSkillData BaseSkillData { get; private set; }
    [field: SerializeField]
    public bool IsDamageSkill { get; private set; }
    [field: SerializeField]
    public DamageSkillData DamageData { get; private set; }
}
