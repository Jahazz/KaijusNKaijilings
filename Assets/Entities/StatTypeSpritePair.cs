using System;
using UnityEngine;

[Serializable]
public class StatTypeSpritePair
{
    [field: SerializeField]
    public StatType Stat { get; private set; }
    [field: SerializeField]
    public Sprite Sprite { get; private set; }
}
