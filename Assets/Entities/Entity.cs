using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;
using Utils;

[Serializable]
public class Entity
{
    [field: SerializeField]
    public StatsScriptable BaseEntityType { get; private set; }
    [field: SerializeField]
    public ObservableVariable<string> Name { get; set; } = new ObservableVariable<string>();
    [field: SerializeField]
    public ObservableStatsData StatsGainedThroughLeveling { get; set; } = new ObservableStatsData();
    [field: SerializeField]
    public ObservableStatsData ModifiedStats { get; set; } = new ObservableStatsData();
    [field: SerializeField]
    public BaseStatsData<float> MatStats { get; set; }
    [field: SerializeField]
    public EntityLevelData LevelData { get; private set; }
    [field: SerializeField]
    public ObservableCollection<TypeDataScriptable> TypeScriptableCollection { get; set; } = new ObservableCollection<TypeDataScriptable>();
    public ObservableCollection<SkillScriptableObject> SelectedSkillsCollection { get; set; } = new ObservableCollection<SkillScriptableObject>();

    private ObservableCollection<StatModifier> StatModifiers { get; set; } = new ObservableCollection<StatModifier>();
    public ObservableCollection<TraitBaseScriptableObject> TraitsCollection { get; set; } = new ObservableCollection<TraitBaseScriptableObject>();

    public Entity (StatsScriptable baseEntity)
    {
        AttachEvents();
        BaseEntityType = baseEntity;
        Name.PresentValue = baseEntity.Name;

        LevelData = new EntityLevelData();
        LevelData.OnLevelUp += HandleOnLevelUp;

        GenerateMatStats();
        InitializeStatsGainedThroughLeveling();
        RecalculateModifiedStats();
        InitializeTypesColection();
    }

    private void InitializeTypesColection ()
    {
        foreach (TypeDataScriptable typeScriptable in BaseEntityType.EntityTypeCollection)
        {
            TypeScriptableCollection.Add(typeScriptable);
        }
    }

    private void HandleOnLevelUp ()
    {
        RiseStats();
        RecalculateModifiedStats();
    }

    private void AttachEvents ()
    {
        StatModifiers.CollectionChanged += HandleStatModifierCollectionChanged;
        TraitsCollection.CollectionChanged += HandleStatModifierCollectionChanged;
    }

    private void HandleStatModifierCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
    {
        RecalculateModifiedStats();
    }


    private Dictionary<StatType, Dictionary<StatModifierType, float>> SortModifiedStats ()
    {
        Dictionary<StatType, Dictionary<StatModifierType, float>> output = new Dictionary<StatType, Dictionary<StatModifierType, float>>();

        foreach (StatType statType in Enum.GetValues(typeof(StatType)))
        {
            output.Add(statType, new Dictionary<StatModifierType, float>());

            List<StatModifier> AllStatsCollection = new List<StatModifier>();
            AllStatsCollection.AddRange(StatModifiers);

            foreach (TraitBaseScriptableObject item in TraitsCollection)
            {
                AllStatsCollection.AddRange(item.ModifiedStatCollection);
            }


            foreach (StatModifier singleModifier in AllStatsCollection)
            {
                if (singleModifier.ModifiedStat == statType)
                {
                    StatModifierType modifier = singleModifier.ModifierType;

                    if (output[statType].ContainsKey(modifier) == false)
                    {
                        output[statType][modifier] = 0;

                    }

                    output[statType][modifier] += singleModifier.Value;
                }
            }

            if (output[statType].Count == 0)
            {
                output[statType][StatModifierType.ADD] = 0; 
            }
        }

        return output;
    }

    private void RecalculateModifiedStats ()
    {
        Dictionary<StatType, Dictionary<StatModifierType, float>> sortedModifiers = SortModifiedStats();

        ApplyModifierForSortedModifiers(sortedModifiers, new List<StatType> { StatType.NONE});
    }

    private void ApplyModifierForSortedModifiers (Dictionary<StatType, Dictionary<StatModifierType, float>> sortedModifiers, List<StatType> excludedModifiers)
    {

        foreach (KeyValuePair<StatType, Dictionary<StatModifierType, float>> statType in sortedModifiers)
        {
            if (excludedModifiers == null || excludedModifiers.Contains(statType.Key) == false)
            {
                foreach (KeyValuePair<StatModifierType, float> statModifier in statType.Value)
                {
                    ModifiedStats.ApplyModifierToModifiedStats(statType.Key, CalculateChange(statModifier.Key, StatsGainedThroughLeveling.GetStatOfType(statType.Key).PresentValue, statModifier.Value));
                }
            }

        }
    }

    private float CalculateChange (StatModifierType statModifierType, float baseStat, float modifier)
    {
        float output = 0;

        switch (statModifierType)
        {
            case StatModifierType.ADD:
                output = baseStat + modifier;
                break;
            case StatModifierType.REMOVE:
                output = baseStat - modifier;
                break;
            case StatModifierType.MULTIPLY:
                output = baseStat * modifier;
                break;
            case StatModifierType.DIVIDE:
                output = baseStat / modifier;
                break;
            case StatModifierType.SET_FLAT_VALUE:
                output = modifier;
                break;
            default:
                break;
        }

        return output;
    }

    private void GenerateMatStats ()
    {
        MatStats = new BaseStatsData<float>();

        MatStats.Might = GenerateRandomStat(BaseEntityType.BaseMatRange.Might);
        MatStats.Magic = GenerateRandomStat(BaseEntityType.BaseMatRange.Magic);
        MatStats.Willpower = GenerateRandomStat(BaseEntityType.BaseMatRange.Willpower);
        MatStats.Agility = GenerateRandomStat(BaseEntityType.BaseMatRange.Agility);
        MatStats.Initiative = GenerateRandomStat(BaseEntityType.BaseMatRange.Initiative);
    }

    private float GenerateRandomStat (Vector2 range)
    {
        return UnityEngine.Random.Range(range.x, range.y);
    }

    private void RiseStats ()
    {
        StatsGainedThroughLeveling.Might.PresentValue += BaseEntityType.StatsPerLevel.Might + CalculateMatMightRise();
        StatsGainedThroughLeveling.Magic.PresentValue += BaseEntityType.StatsPerLevel.Magic + CalculateMatMagictRise();
        StatsGainedThroughLeveling.Willpower.PresentValue += BaseEntityType.StatsPerLevel.Willpower + CalculateMatWillpowerRise();
        StatsGainedThroughLeveling.Agility.PresentValue += BaseEntityType.StatsPerLevel.Agility + CalculateMatAgilityRise();
        StatsGainedThroughLeveling.Initiative.PresentValue += BaseEntityType.StatsPerLevel.Initiative + CalculateMatInitiativeRise();
    }

    private void InitializeStatsGainedThroughLeveling ()
    {
        StatsGainedThroughLeveling.Might.PresentValue = BaseEntityType.BaseStats.Might;
        StatsGainedThroughLeveling.Magic.PresentValue = BaseEntityType.BaseStats.Magic;
        StatsGainedThroughLeveling.Willpower.PresentValue = BaseEntityType.BaseStats.Willpower;
        StatsGainedThroughLeveling.Agility.PresentValue = BaseEntityType.BaseStats.Agility;
        StatsGainedThroughLeveling.Initiative.PresentValue = BaseEntityType.BaseStats.Initiative;
    }

    private float CalculateMatMightRise ()
    {
        return MatStats.Might;
    }

    private float CalculateMatMagictRise ()
    {
        return MatStats.Magic;
    }

    private float CalculateMatWillpowerRise ()
    {
        return MatStats.Willpower;
    }

    private float CalculateMatAgilityRise ()
    {
        return MatStats.Agility;
    }

    private float CalculateMatInitiativeRise ()
    {
        return MatStats.Initiative;
    }

}
