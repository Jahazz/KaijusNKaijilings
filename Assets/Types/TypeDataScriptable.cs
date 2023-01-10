using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "TypeData", menuName = "ScriptableObjects/TypeData", order = 2)]
public class TypeDataScriptable : ScriptableObject
{
    public string TypeName;
    public List<TypeDamagePair> AttackerMultiplierCollection;
}
