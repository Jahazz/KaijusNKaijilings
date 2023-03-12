using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityLevelPair
{
    [field: SerializeField]
    public StatsScriptable Entity { get; private set; }
    [field: SerializeField]
    public int Level { get; private set; }
}
