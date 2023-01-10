using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    [field: SerializeField]
    private List<StatsScriptable> AllEntitiesTypes { get; set; } = new List<StatsScriptable>();
    [field: SerializeField]
    public LevelRequirementsScriptable LevelRequirements { get; private set; }

    public Entity RequestEntity (StatsScriptable entityType, int level)
    {
        Entity createdEntity = new Entity(entityType);
        createdEntity.AddExperience(LevelRequirements.GetExpNeededForLevel(level-1));
        return createdEntity;
    }

    public void Start ()
    {
        Entity a = RequestEntity(AllEntitiesTypes[0], 3);
        Debug.Log(a.Level);
    }

}
