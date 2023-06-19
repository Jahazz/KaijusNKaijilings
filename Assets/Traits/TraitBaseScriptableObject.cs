using System;
using System.Collections.Generic;
using Tooltips;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitBaseScriptableObject", menuName = "ScriptableObjects/TraitBaseScriptableObject")]
public class TraitBaseScriptableObject : ScriptableObject, INameableGUIDableDescribableTooltipable
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public TraitType TraitType { get; private set; }
    [field: SerializeField]
    public List<StatModifier> ModifiedStatCollection { get; set; } = new List<StatModifier>();
    [field: SerializeField]
    public List<TraitBaseScriptableObject> ExcludesTraits { get; set; }
    [field: SerializeField]
    public string GUID { get; } = Guid.NewGuid().ToString();
    public TooltipType TooltipType { get; } = TooltipType.TRAIT;
}
