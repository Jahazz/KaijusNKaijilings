using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatTypeStatElementPair
{
    [field: SerializeField]
    public StatType StatType { get; private set; }
    [field: SerializeField]
    public StatElement StatElement { get; private set; }
}
