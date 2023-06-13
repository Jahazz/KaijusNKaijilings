using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
[CreateAssetMenu(fileName = "TypeData", menuName = "ScriptableObjects/TypeData", order = 2)]
public class TypeDataScriptable : ScriptableObject, INameableGUIDableDescribable
{
    [field: FormerlySerializedAs("TypeName")]
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get; set; }
    [field: SerializeField]
    public List<TypeDamagePair> AttackerMultiplierCollection { get; set; }
    public Sprite TypeSprite { get; set; }
    [field: SerializeField]
    public string GUID { get; set; } = Guid.NewGuid().ToString();
}
