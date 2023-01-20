using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EntityManager : MonoBehaviour
{
    [field: SerializeField]
    private List<StatsScriptable> AllEntitiesTypes { get; set; } = new List<StatsScriptable>();
    [field: SerializeField]
    public LevelRequirementsScriptable LevelRequirements { get; private set; }

    public Entity RequestEntity (StatsScriptable entityType, int level)
    {
        Entity createdEntity = new Entity(entityType);
        createdEntity.LevelData.AddExperience(LevelRequirements.GetExpNeededForLevel(level));
        return createdEntity;
    }

    public Entity RequestRandomEntity (int level)
    {
        return RequestEntity(AllEntitiesTypes.GetRandomElement(), level);
    }

}
