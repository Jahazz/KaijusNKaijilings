using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseSkillData
{
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public List<TypeDataScriptable> SkilType { get; private set; }
    [field: SerializeField]
    public GameObject GameobjectToSpawnOnHitTarget { get; private set; }
    [field: SerializeField]
    private string DevelopmentData { get; set; }
}
