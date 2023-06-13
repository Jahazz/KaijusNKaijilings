using System;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(AdditionalTooltipScriptable), menuName = "ScriptableObjects/Descriptors/" + nameof(AdditionalTooltipScriptable))]
public class AdditionalTooltipScriptable : ScriptableObject, INameableGUIDableDescribable
{
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public string GUID { get; set; } = Guid.NewGuid().ToString();
}
