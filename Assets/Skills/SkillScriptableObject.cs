using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScriptableObject : SkillBase
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
}
