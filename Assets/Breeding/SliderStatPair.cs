using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SliderStatPair
{
    [field: SerializeField]
    public CustomProgressBar ProgressBar { get; private set; }
    [field: SerializeField]
    public StatType StatType { get; private set; }
}
