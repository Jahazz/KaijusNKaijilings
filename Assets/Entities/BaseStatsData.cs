using System.Collections.Generic;
using UnityEngine;
using Utils;

[System.Serializable]
public class BaseStatsData<Type>
{
    [field: SerializeField]
    public Type Might { get; set; }
    [field: SerializeField]
    public Type Magic { get; set; }
    [field: SerializeField]
    public Type Willpower { get; set; }
    [field: SerializeField]
    public Type Agility { get; set; }
    [field: SerializeField]
    public Type Initiative { get; set; }
}
