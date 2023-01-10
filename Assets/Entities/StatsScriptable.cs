using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class StatsScriptable : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public List<TypeDataScriptable> EntityType { get; private set; }
    [field:SerializeField]
    public BaseStatsData<int> BaseStats { get; private set; }
    [field: SerializeField]
    public BaseStatsData<int> StatsPerLevel { get; private set; }
    [field: SerializeField]
    public BaseStatsData<Vector2Int> BaseMatRange { get; private set; }
    [field: SerializeField]
    public List<LevelSkillPair> SkillsWithRequirements { get; private set; }
}
