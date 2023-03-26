using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleScreenView : BaseView
{
    [field: SerializeField]
    private EntityStats EntityStatsPrefab { get; set; }
    [field: SerializeField]
    private Canvas BattleScreenCanvas { get; set; }
    [field: SerializeField]
    public Transform FirstEntityTargetTransform { get; private set; }
    [field: SerializeField]
    public Transform SecondEntityTargetTransform { get; private set; }
    [field: SerializeField]
    private List<SkillUseButton> SkillButtonCollection { get; set; }

    private Dictionary<Entity, BattleScreenEntityController> EntityBattleScreenEntityControllerPair { get; set; } = new Dictionary<Entity, BattleScreenEntityController>();

    public void SpawnEntityInPlayerPosition (Entity entity, bool isPlayerOwner)
    {
        SpawnInPosition(FirstEntityTargetTransform, entity, isPlayerOwner);
    }

    public void SpawnEntityInSecondPosition (Entity entity, bool isPlayerOwner)
    {
        SpawnInPosition(SecondEntityTargetTransform, entity, isPlayerOwner);
    }

    public void BindSkillToButtons (List<SkillScriptableObject> skillCollection, Entity ownerEntity)
    {
        for (int i = 0; i < skillCollection.Count; i++)
        {
            SkillButtonCollection[i].BindWithSkill(skillCollection[i], ownerEntity);
        }
    }

    public void PlayAnimationAsEntity (Entity entity, AnimationType animationType)
    {
        EntityBattleScreenEntityControllerPair[entity].PlayAnimation(animationType);
    }

    private void SpawnInPosition (Transform position, Entity entity, bool isPlayerOwner)
    {
        EntityBattleScreenEntityControllerPair.Add(entity, SingletonContainer.Instance.BattleScreenManager.SpawnEntity(position, entity));
        SpawnEntityStats(entity, position, isPlayerOwner);
    }

    private void SpawnEntityStats (Entity entity, Transform transformPosition, bool isPlayerOwner)
    {
        Instantiate(EntityStatsPrefab, BattleScreenCanvas.transform).Initialize(entity, transformPosition, isPlayerOwner);
    }
}
