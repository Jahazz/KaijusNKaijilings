using MVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleScreenView : BaseView
{
    [field: SerializeField]
    private EntityStats EntityStatsPrefab { get; set; }
    [field: SerializeField]
    private Canvas BattleScreenCanvas { get; set; }
    [field: SerializeField]
    private Canvas DamageIndicatorCanvas { get; set; }
    [field: SerializeField]
    private DamageIndicator DamageIndicatorPrefab { get; set; }
    [field: SerializeField]
    public Transform FirstEntityTargetTransform { get; private set; }
    [field: SerializeField]
    public Transform SecondEntityTargetTransform { get; private set; }
    [field: SerializeField]
    private List<SkillUseButton> SkillButtonCollection { get; set; }
    [field: SerializeField]
    private RectTransform BottomBar { get; set; }
    [field: SerializeField]
    private GameObject BottomBarBlocker { get; set; }

    private Dictionary<Entity, BattleScreenEntityController> EntityBattleScreenEntityControllerPair { get; set; } = new Dictionary<Entity, BattleScreenEntityController>();

    public IEnumerator SpawnEntityInPlayerPosition(Entity entity, bool isPlayerOwner)
    {
        yield return SpawnInPosition(FirstEntityTargetTransform, entity, isPlayerOwner);
    }

    public IEnumerator SpawnEntityInSecondPosition(Entity entity, bool isPlayerOwner)
    {
        yield return SpawnInPosition(SecondEntityTargetTransform, entity, isPlayerOwner);
    }

    public IEnumerator TryToDestroyEntityOnScene(Entity entityToDestroy)
    {
        //if (entityToDestroy != null && EntityBattleScreenEntityControllerPair.ContainsKey(entityToDestroy) == true)
        //{
        EntityBattleScreenEntityControllerPair[entityToDestroy].DestroyEntityStats();
        Destroy(EntityBattleScreenEntityControllerPair[entityToDestroy].gameObject);
        yield return null;
        //}
    }

    public void BindSkillToButtons(List<SkillScriptableObject> skillCollection, Entity ownerEntity)
    {
        for (int i = 0; i < skillCollection.Count; i++)
        {
            SkillButtonCollection[i].BindWithSkill(skillCollection[i], ownerEntity);
        }
    }

    public IEnumerator PlayAnimationAsEntity(Entity entity, AnimationType animationType)
    {
        return EntityBattleScreenEntityControllerPair[entity].PlayAnimation(animationType);
    }

    public IEnumerator WaitUntilAnimatorIdle(Entity entity)
    {
        yield return new WaitUntil(() => EntityBattleScreenEntityControllerPair[entity].IsAnimatorIdle() == true);
    }

    public void SetBottomUIBarInteractible(bool isBottomBarInteractible)
    {
        BottomBarBlocker.SetActive(isBottomBarInteractible == false);
    }

    public void IsBottomBarShown(bool IsBottomBarShown)
    {
        if (IsBottomBarShown == true)
        {
            BottomBar.DOAnchorPosY(0, 1);
        }
        else
        {
            BottomBar.DOAnchorPosY(-400, 1);//TODO: Resolve magic values
        }
    }

    private IEnumerator SpawnInPosition(Transform position, Entity entity, bool isPlayerOwner)
    {
        BattleScreenEntityController entityController;
        Tweener tweener = SingletonContainer.Instance.BattleScreenManager.SpawnEntity(position, entity, out entityController);
        EntityBattleScreenEntityControllerPair.Add(entity, entityController);
        entityController.Initialize(entity, DamageIndicatorCanvas, DamageIndicatorPrefab, SpawnEntityStats(entity, position, isPlayerOwner));
        yield return tweener.WaitForCompletion();
    }

    private EntityStats SpawnEntityStats(Entity entity, Transform transformPosition, bool isPlayerOwner)
    {
        EntityStats output = Instantiate(EntityStatsPrefab, BattleScreenCanvas.transform);
        output.Initialize(entity, transformPosition, isPlayerOwner);
        return output;
    }
}
