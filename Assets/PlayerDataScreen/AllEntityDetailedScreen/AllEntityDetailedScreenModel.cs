using MVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEntityDetailedScreenModel : BaseModel<AllEntityDetailedScreenView>
{
    private Action<StatsScriptable> OnEntitySelectionCallback { get; set; }

    public StatsScriptable CurrentEntity { get; private set; }

    public virtual void ShowDataOfEntity (StatsScriptable targetEntity)
    {
        CurrentEntity = targetEntity;

        CurrentView.SetPersistentData(CurrentEntity.Name, CurrentEntity.Description, CurrentEntity.Image, CurrentEntity.EntityTypeCollection);

        GenerateGraphs();





    }

    public void GenerateGraphs ()
    {
        var a = new BaseStatsData<Vector2>();
        var overallMaxStats = new BaseStatsData<Vector2>();
        StatType[] statTypesCollection = { StatType.MIGHT, StatType.MAGIC, StatType.WILLPOWER, StatType.AGILITY, StatType.INITIATIVE };

        Dictionary<StatType, Vector2> c = new Dictionary<StatType, Vector2>();

        foreach (var singleEntity in SingletonContainer.Instance.EntityManager.AllEntitiesTypes)
        {
            foreach (var statType in statTypesCollection)
            {
                float currentEntityStatValue = singleEntity.BaseMatRange.GetStatOfType(statType).y;

                if (overallMaxStats.GetStatOfType(statType).y <= currentEntityStatValue)
                {
                    overallMaxStats.SetStatOfType(statType, new Vector2(0, currentEntityStatValue));
                }

                if (c.ContainsKey(statType) == false || c[statType].y <= currentEntityStatValue)
                {
                    c[statType] = new Vector2(0, currentEntityStatValue);
                }
            }
        }

        

        CurrentView.SetStats(CurrentEntity.BaseMatRange, overallMaxStats, statTypesCollection);
    }

    public void SetChooseEntityCallback (Action<StatsScriptable> onEntitySelectionCallback)
    {
        OnEntitySelectionCallback = onEntitySelectionCallback;
    }
}
