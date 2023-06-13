using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(ScriptableStatData), menuName = "ScriptableObjects/Descriptors/" + nameof(ScriptableStatData))]
public class ScriptableStatData : ScriptableObject, INameableGUIDableDescribable
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
}
