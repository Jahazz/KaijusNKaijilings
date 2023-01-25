using System;
using UnityEngine;

[Serializable]
public class StatModifier
{
    [field: SerializeField]
    public StatModifierType ModifierType { get; private set; }
    [field: SerializeField]
    public StatType ModifiedStat {get; private set; }
    [field: SerializeField]
    public float Value { get; private set; }

    public StatModifier (StatModifierType modifierType, StatType modifiedStat, float value)
    {
        ModifierType = modifierType;
        ModifiedStat = modifiedStat;
        Value = value;
    }
}
