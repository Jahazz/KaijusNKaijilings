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

    public void SpawnEntityInFirstPosition (Entity entity)
    {
        SpawnInPosition(FirstEntityTargetTransform, entity);
    }

    public void SpawnEntityInSecondPosition (Entity entity)
    {
        SpawnInPosition(SecondEntityTargetTransform, entity);
    }

    private void SpawnInPosition (Transform position, Entity entity)
    {
        SingletonContainer.Instance.BattleScreenManager.SpawnEntity(position, entity);
        SpawnEntityStats(entity, position);
    }

    private void SpawnEntityStats (Entity entity, Transform transformPosition)
    {
        Instantiate(EntityStatsPrefab, BattleScreenCanvas.transform).Initialize(entity, transformPosition);
    }
}
