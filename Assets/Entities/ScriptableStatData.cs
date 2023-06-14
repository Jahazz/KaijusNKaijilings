using System;
using System.Collections;
using System.Collections.Generic;
using Tooltips;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(ScriptableStatData), menuName = "ScriptableObjects/Descriptors/" + nameof(ScriptableStatData))]
public class ScriptableStatData : ScriptableObject, INameableGUIDableDescribableTooltipable
{
    [field: SerializeField]
    public StatType Stat { get; private set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public string GUID { get; set; } = Guid.NewGuid().ToString();
    public TooltipType TooltipType { get; protected set; } = TooltipType.STAT;
}
