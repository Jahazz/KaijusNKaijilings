using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterMenuElementData
{
    [field: SerializeField]
    public CharacterMenuElement CharacterMenuElement { get; private set; }
    [field: SerializeField]
    public int PageIndex { get; private set; }
    [field: SerializeField]
    public CharacterMenuTabType TabType { get; private set; }
}
