using BattleCore.ScreenEntity;
using System;
using System.Collections.Generic;
using Tooltips;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityData", menuName = "ScriptableObjects/EntityData", order = 1)]
public class StatsScriptable : ScriptableObject, INameableGUIDableDescribableTooltipable
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public string Description { get;  set; }
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
    [field: SerializeField]
    public string GUID { get; } = Guid.NewGuid().ToString();
    public TooltipType TooltipType { get; } = TooltipType.ENTITY;


}
