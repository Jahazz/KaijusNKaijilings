using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class EntityManager : MonoBehaviour
{
    [field: SerializeField]
    public List<StatsScriptable> AllEntitiesTypes { get; set; } = new List<StatsScriptable>();
    [field: SerializeField]
    public List<TraitBaseScriptableObject> AvailableTraits { get; private set; } = new List<TraitBaseScriptableObject>();
    [field: SerializeField]
    public LevelRequirementsScriptable LevelRequirements { get; private set; }
    [field: SerializeField]
    private List<StatTypeSpritePair> StatTypeSpriteCollection { get; set; }
    [field: SerializeField]
    public Dictionary<StatType, Sprite> StatTypeSpriteDictionary { get; set; } = new Dictionary<StatType, Sprite>();



    public Entity RequestEntity (StatsScriptable entityType, int level, List<TraitBaseScriptableObject> availableTraits = null, BaseStatsData<Vector2> matStatsRange = null)
    {
        Entity createdEntity = new Entity(entityType, matStatsRange);
        createdEntity.LevelData.AddExperience(LevelRequirements.GetExpNeededForLevel(level));

        if (availableTraits == null)
        {
            availableTraits = AvailableTraits;
        }

        AddRandomTraitsToEntity(createdEntity, availableTraits);

        return createdEntity;
    }

    public Entity RequestRandomEntity (int level)
    {
        return RequestEntity(AllEntitiesTypes.GetRandomElement(), level);
    }

    public List<TraitBaseScriptableObject> GetTraitsFromList (List<TraitBaseScriptableObject> sourceList)
    {
        List<TraitBaseScriptableObject> excludedTraits = new List<TraitBaseScriptableObject>();
        List<TraitBaseScriptableObject> output = new List<TraitBaseScriptableObject>();

        foreach (TraitType item in Enum.GetValues(typeof(TraitType)))
        {
            output.Add(GetTraitOfType(item, sourceList, excludedTraits));
        }

        return output;
    }

    protected virtual void Start ()
    {
        DictionarizeStatTypeSpriteCollection();
    }

    private void DictionarizeStatTypeSpriteCollection ()
    {
        StatTypeSpriteDictionary = StatTypeSpriteCollection.ToDictionary(k => k.Stat, v => v.Sprite);
    }

    private void AddRandomTraitsToEntity (Entity entityToAddTraits, List<TraitBaseScriptableObject> availableTraits)
    {
        foreach (var item in GetTraitsFromList(availableTraits))
        {
            entityToAddTraits.TraitsCollection.Add(item);
        }
    }

    private TraitBaseScriptableObject GetTraitOfType (TraitType traitType, List<TraitBaseScriptableObject> sourceList, List<TraitBaseScriptableObject> excludedTraits)
    {
        List<TraitBaseScriptableObject> availableTraits = new List<TraitBaseScriptableObject>(sourceList);
        TraitBaseScriptableObject output;

        foreach (TraitBaseScriptableObject trait in AvailableTraits)
        {
            if (trait.TraitType != traitType)
            {
                availableTraits.Remove(trait);
            }
        }

        foreach (TraitBaseScriptableObject trait in excludedTraits)
        {
            availableTraits.Remove(trait);
        }

        output = availableTraits.GetRandomElement();

        foreach (var item in output.ExcludesTraits)
        {
            excludedTraits.Add(item);
        }

        return output;
    }

}
