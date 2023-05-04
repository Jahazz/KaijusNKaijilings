using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillScriptableObject : SkillBase
{
    [field: SerializeField]
    public BaseSkillData BaseSkillData { get; private set; }
}
