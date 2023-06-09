using BattleCore;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class EntityStatusEffect
{
    public delegate void OnStatusEffectRemovedParams ();
    public event OnStatusEffectRemovedParams OnStatusEffectRemoved;

    public BaseScriptableEntityStatusEffect BaseStatusEffect { get; private set; }
    public ObservableVariable<int> CurrentNumberOfStacks { get; set; } = new ObservableVariable<int>();
    private Entity Target { get; set; }
    private Battle CurrentBattle { get; set; }

    public EntityStatusEffect (BaseScriptableEntityStatusEffect baseStatusEffect, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        BaseStatusEffect = baseStatusEffect;
        Target = target;
        CurrentBattle = currentBattle;
        CurrentNumberOfStacks.PresentValue = numberOfStacksToAdd;
        AddEventsToEntiy(currentBattle);
    }

    public void Cleanup ()
    {
        Target.OnCleanse -= HandleOnEntityCleansed;
        CurrentBattle.OnBattleFinished -= HandleOnEndOfCombat;
        Target.IsAlive.OnVariableChange -= HandleOnEntityDeath;
        OnStatusEffectRemoved -= Cleanup;
    }

    private void AddEventsToEntiy (Battle currentBattle)
    {
        if (BaseStatusEffect.Cleansable == true)
        {
            Target.OnCleanse += HandleOnEntityCleansed;
        }

        if (BaseStatusEffect.RemovedAtEndOfCombat == true)
        {
            currentBattle.OnBattleFinished += HandleOnEndOfCombat;
        }

        if (BaseStatusEffect.RemovedOnDeath == true)
        {
            Target.IsAlive.OnVariableChange += HandleOnEntityDeath;
        }

        OnStatusEffectRemoved += Cleanup;
    }

    void HandleOnEntityCleansed ()
    {
        Cleanup();
        SkillUtils.RemoveAllStacksOfStatusEffect(Target, BaseStatusEffect);
    }

    void HandleOnEndOfCombat (BattleResultType _)
    {
        Cleanup();
        SkillUtils.RemoveAllStacksOfStatusEffect(Target, BaseStatusEffect);
    }

    void HandleOnEntityDeath (bool newValue, bool _)
    {
        if (newValue == false)
        {
            Cleanup();
            SkillUtils.RemoveAllStacksOfStatusEffect(Target, BaseStatusEffect);
        }
    }

    public void InvokeOnRemoved ()
    {
        OnStatusEffectRemoved.Invoke();
    }
}
