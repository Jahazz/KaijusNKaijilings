using BattleCore.ScreenEntity;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class StatsScriptable : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public Sprite Image { get; private set; }
    [field: SerializeField]
    public BattleScreenEntityController ModelPrefab { get; private set; }
    [field: SerializeField]
    public List<TypeDataScriptable> EntityTypeCollection { get; private set; }
    [field:SerializeField]
    public BaseStatsData<float> BaseStats { get; private set; }
    [field: SerializeField]
    public BaseStatsData<float> StatsPerLevel { get; private set; }
    [field: SerializeField]
    public BaseStatsData<Vector2> BaseMatRange { get; private set; }
    [field: SerializeField]
    public List<LevelSkillPair> SkillsWithRequirements { get; private set; }
    [field: SerializeField]
    public int GroupCountRequiredToBreed { get; private set; }
}
