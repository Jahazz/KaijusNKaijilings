using System;
using System.Collections.Generic;
using Tooltips;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
[CreateAssetMenu(fileName = "TypeData", menuName = "ScriptableObjects/TypeData", order = 2)]
public class TypeDataScriptable : ScriptableObject, INameableGUIDableDescribableTooltipable
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public List<TypeDamagePair> AttackerMultiplierCollection { get; set; }
    [field: SerializeField]
    public Sprite TypeSprite { get; set; }
    [field: SerializeField]
    public string GUID { get; } = Guid.NewGuid().ToString();
    public TooltipType TooltipType { get; } = TooltipType.ENTITY_TYPE;
}
