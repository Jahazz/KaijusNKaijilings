using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class BaseStatsData<Type>
{
    [field: SerializeField]
    public Type Might { get; set; }
    [field: SerializeField]
    public Type Magic { get; set; }
    [field: SerializeField]
    public Type Willpower { get; set; }
    [field: SerializeField]
    public Type Agility { get; set; }
    [field: SerializeField]
    public Type Initiative { get; set; }

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
}
