using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseStatsData<StatsType>
{
    [field: SerializeField]
    public StatsType Might { get; set; }
    [field: SerializeField]
    public StatsType Magic { get; set; }
    [field: SerializeField]
    public StatsType Willpower { get; set; }
    [field: SerializeField]
    public StatsType Agility { get; set; }
    [field: SerializeField]
    public StatsType Initiative { get; set; }
}
