using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelSkillPair
{
    [field: SerializeField]
    public int RequiredLevel { get; set; }
    [field: SerializeField]
    public SkillScriptableObject AssignedSkill { get; set; }
}
