using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class ScriptableStatusEffect : ScriptableObject
{
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; set; }
}
