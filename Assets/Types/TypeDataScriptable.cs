using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TypeData", menuName = "ScriptableObjects/TypeData", order = 2)]
public class TypeDataScriptable : ScriptableObject
{
    public string TypeName;
    public string Description;
    public List<TypeDamagePair> AttackerMultiplierCollection;
    public Sprite TypeSprite;
    [field: SerializeField]
    public string SkillGUID { get; set; } = Guid.NewGuid().ToString();
}
