using System;
using UnityEngine;

[Serializable]
public class DamageSkillData 
{

    [field: SerializeField]
    public Vector2 DamageRangeValue { get; private set; }
    [field: SerializeField]
    public StatType AttackType { get; private set; }
    [field: SerializeField]
    public StatType DefenceType { get; private set; }
}
