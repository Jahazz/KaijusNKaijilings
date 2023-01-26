using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EntityManager : MonoBehaviour
{
    [field: SerializeField]
    private List<StatsScriptable> AllEntitiesTypes { get; set; } = new List<StatsScriptable>();
    [field: SerializeField]
    public List<TraitBaseScriptableObject> AvailableTraits { get; private set; } = new List<TraitBaseScriptableObject>();
    [field: SerializeField]
    public List<float> TraitCountChancesCollection { get; private set; } = new List<float>();
    [field: SerializeField]
    public LevelRequirementsScriptable LevelRequirements { get; private set; }

    public Entity RequestEntity (StatsScriptable entityType, int level, BaseStatsData<Vector2> matStatsRange = null)
    {
        Entity createdEntity = new Entity(entityType, matStatsRange);
        createdEntity.LevelData.AddExperience(LevelRequirements.GetExpNeededForLevel(level));
        AddRandomTraitsToEntity(createdEntity);
        return createdEntity;
    }

    public Entity RequestRandomEntity (int level)
    {
        return RequestEntity(AllEntitiesTypes.GetRandomElement(), level);
    }

    private void AddRandomTraitsToEntity (Entity entityToAddTraits)
    {
        float numberOfTraitsChance = Random.Range(0.0f, 1.0f);
        int numberOfTraits = 0;

        for (int i = TraitCountChancesCollection.Count - 1; i >= 0; i--)
        {
            float currentChance = TraitCountChancesCollection[i];

            if (numberOfTraitsChance <= currentChance)
            {
                numberOfTraits = i + 1;
                break;
            }
        }

        List<TraitBaseScriptableObject> availableTraits = new List<TraitBaseScriptableObject>(AvailableTraits);

        for (int i = 0; i < numberOfTraits; i++)
        {
            TraitBaseScriptableObject chosenTrait = availableTraits.GetRandomElement();

            Debug.Log("Chosen"+ chosenTrait);
            availableTraits.Remove(chosenTrait);

            foreach (var item in chosenTrait.ExcludesTraits)
            {
                availableTraits.Remove(item);
                Debug.Log("RemovedTrait"+item.Name);
            }

            entityToAddTraits.TraitsCollection.Add(chosenTrait);
        }
    }

}
