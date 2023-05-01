using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class BaseStatsData<Type>
{
    [field: SerializeField]
    public Type Might { get; set; } = default;
    [field: SerializeField]
    public Type Magic { get; set; } = default;
    [field: SerializeField]
    public Type Willpower { get; set; } = default;
    [field: SerializeField]
    public Type Agility { get; set; } = default;
    [field: SerializeField]
    public Type Initiative { get; set; } = default;

    public virtual Type GetStatOfType (StatType statType)
    {
        Type output = default;

        switch (statType)
        {
            case StatType.MIGHT:
                output = Might;
                break;
            case StatType.MAGIC:
                output = Magic;
                break;
            case StatType.WILLPOWER:
                output = Willpower;
                break;
            case StatType.AGILITY:
                output = Agility;
                break;
            case StatType.INITIATIVE:
                output = Initiative;
                break;
            default:
                break;
        }

        return output;
    }

    public virtual void SetStatOfType (StatType statType, Type value)
    {
        switch (statType)
        {
            case StatType.MIGHT:
                Might = value;
                break;
            case StatType.MAGIC:
                Magic = value;
                break;
            case StatType.WILLPOWER:
                Willpower = value;
                break;
            case StatType.AGILITY:
                Agility = value;
                break;
            case StatType.INITIATIVE:
                Initiative = value;
                break;
            default:
                break;
        }

    }
}
