using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Entity
{
    [field: SerializeField]
    public StatsScriptable BaseEntityType { get; private set; }
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public int Level { get; set; } = 1;
    [field: SerializeField]
    public int Experience { get; private set; } = 0;
    [field: SerializeField]
    public int CurrentHp { get; set; } = 0;
    [field: SerializeField]
    public BaseStatsData<int> StatsGainedThroughLeveling { get; set; } = new BaseStatsData<int>();
    [field: SerializeField]
    public BaseStatsData<int> MatStats { get; set; }

    public Entity (StatsScriptable baseEntity)
    {
        BaseEntityType = baseEntity;
        Name = baseEntity.Name;
        GenerateMatStats();
    }

    private void GenerateMatStats ()
    {
        MatStats = new BaseStatsData<int>();

        MatStats.Might = GenerateRandomStat(BaseEntityType.BaseMatRange.Might);
        MatStats.Magic = GenerateRandomStat(BaseEntityType.BaseMatRange.Magic);
        MatStats.Willpower = GenerateRandomStat(BaseEntityType.BaseMatRange.Willpower);
        MatStats.Agility = GenerateRandomStat(BaseEntityType.BaseMatRange.Agility);
        MatStats.Initiative = GenerateRandomStat(BaseEntityType.BaseMatRange.Initiative);
    }

    private int GenerateRandomStat (Vector2Int range)
    {
        return UnityEngine.Random.Range(range.x, range.y);
    }

    public void AddExperience (int experience)
    {
        Experience += experience;
        int targetLevel = SingletonContainer.Instance.EntityManager.LevelRequirements.GetLevelAtExp(Experience);

        while (Level < targetLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp ()
    {
        Level++;
        RiseStats();
    }

    private void RiseStats ()
    {
        StatsGainedThroughLeveling.Might += BaseEntityType.StatsPerLevel.Might + CalculateMatMightRise();
        StatsGainedThroughLeveling.Magic += BaseEntityType.StatsPerLevel.Magic + CalculateMatMagictRise();
        StatsGainedThroughLeveling.Willpower += BaseEntityType.StatsPerLevel.Willpower + CalculateMatWillpowerRise();
        StatsGainedThroughLeveling.Agility += BaseEntityType.StatsPerLevel.Agility + CalculateMatAgilityRise();
        StatsGainedThroughLeveling.Initiative += BaseEntityType.StatsPerLevel.Initiative + CalculateMatInitiativeRise();
    }

    private int CalculateMatMightRise ()
    {
        return MatStats.Might;
    }

    private int CalculateMatMagictRise ()
    {
        return MatStats.Magic;
    }

    private int CalculateMatWillpowerRise ()
    {
        return MatStats.Willpower;
    }

    private int CalculateMatAgilityRise ()
    {
        return MatStats.Agility;
    }

    private int CalculateMatInitiativeRise ()
    {
        return MatStats.Initiative;
    }
}
