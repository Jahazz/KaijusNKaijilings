using System;
using System.Collections.Generic;
using Tooltips;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(AdditionalTooltipScriptable), menuName = "ScriptableObjects/Descriptors/" + nameof(AdditionalTooltipScriptable))]
public class AdditionalTooltipScriptable : ScriptableObject, INameableGUIDableDescribableTooltipable
{
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public string GUID { get; private set; } = Guid.NewGuid().ToString();
    [field: SerializeField]
    public List<string> Aliases { get; private set; }
    public TooltipType TooltipType { get; protected set; } = TooltipType.KEYWORD;
}
