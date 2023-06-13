using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(ScriptableStatData), menuName = "ScriptableObjects/Descriptors/" + nameof(ScriptableStatData))]
public class ScriptableStatData : ScriptableObject
{
    [field: SerializeField]
    public StatType Stat { get; private set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
    [field: SerializeField]
    public string DisplayName { get; private set; }
    [field: SerializeField]
    public string StatDescription { get; private set; }
}
