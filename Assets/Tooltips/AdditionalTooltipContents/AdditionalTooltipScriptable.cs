using UnityEngine;


[CreateAssetMenu(fileName = nameof(AdditionalTooltipScriptable), menuName = "ScriptableObjects/Descriptors/" + nameof(AdditionalTooltipScriptable))]
public class AdditionalTooltipScriptable : ScriptableObject
{
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public string DisplayName { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
}
