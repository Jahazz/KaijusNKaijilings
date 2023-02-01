using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TraitBaseScriptableObject", menuName = "ScriptableObjects/TraitBaseScriptableObject")]
public class TraitBaseScriptableObject : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public TraitType TraitType { get; private set; }
    [field: SerializeField]
    public List<StatModifier> ModifiedStatCollection { get; set; } = new List<StatModifier>();
    [field: SerializeField]
    public List<TraitBaseScriptableObject> ExcludesTraits { get; set; }
}
