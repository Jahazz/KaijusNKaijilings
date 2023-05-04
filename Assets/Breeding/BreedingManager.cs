using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreedingManager : MonoBehaviour
{
    public Entity Breed (Player breeder, List<Entity> entitiesToBreedCollection)
    {
        Entity output;

        foreach (Entity parent in entitiesToBreedCollection)
        {
            breeder.EntitiesInEquipment.Remove(parent);
        }

        output = SingletonContainer.Instance.EntityManager.RequestEntity(entitiesToBreedCollection[0].BaseEntityType, 1, GenerateTraitsForEntity(entitiesToBreedCollection), GetStatRangesForSelectedParents(entitiesToBreedCollection));
        breeder.EntitiesInEquipment.Add(output);

        return output;
    }

    public BaseStatsData<Vector2> GetStatRangesForSelectedParents (List<Entity> entitiesToBreedCollection)
    {
        BaseStatsData<Vector2> matStatsRange = new BaseStatsData<Vector2>();

        foreach (Entity parent in entitiesToBreedCollection)
        {
            matStatsRange.Agility = UpdateStatRange(matStatsRange.Agility, parent.MatStats.Agility);
            matStatsRange.Initiative = UpdateStatRange(matStatsRange.Initiative, parent.MatStats.Initiative);
            matStatsRange.Magic = UpdateStatRange(matStatsRange.Magic, parent.MatStats.Magic);
            matStatsRange.Might = UpdateStatRange(matStatsRange.Might, parent.MatStats.Might);
            matStatsRange.Willpower = UpdateStatRange(matStatsRange.Willpower, parent.MatStats.Willpower);

        }

        return matStatsRange;
    }

    private Vector2 UpdateStatRange (Vector2 statRange, float parentStatValue)
    {
        Vector2 output;

        if (statRange == Vector2.zero)
        {
            output = new Vector2(parentStatValue, parentStatValue);
        }
        else
        {
             output = new Vector2(Mathf.Min(statRange.x, parentStatValue), Mathf.Max(statRange.y, parentStatValue));
        }

        return output;
    }

    private List<TraitBaseScriptableObject> GenerateTraitsForEntity ( List<Entity> entitiesToBreedCollection)
    {
        List<TraitBaseScriptableObject> output = new List<TraitBaseScriptableObject>();

        foreach (Entity entity in entitiesToBreedCollection)
        {
            output.AddRange(entity.TraitsCollection.Except(output));
        }

        return output;
    }
}
