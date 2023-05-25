using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseSkillData
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public int Cost { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public List<TypeDataScriptable> SkilType { get; private set; }
    [field: SerializeField]
    public GameObject GameobjectToSpawnOnHitTarget { get; private set; }
    [field: SerializeField]
    private string DevelopmentData { get; set; }
}
