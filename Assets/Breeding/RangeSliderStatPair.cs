using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RangeSliderStatPair
{
    [field: SerializeField]
    public RangeSlider RangeSlider { get; private set; }
    [field: SerializeField]
    public StatType StatType { get; set; }
}
