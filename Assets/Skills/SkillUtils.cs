using BattleCore;
using StatusEffects;
using StatusEffects.BattlegroundStatusEffects;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public static class SkillUtils
{
    public static void UseDamagingSkill (Entity caster, Entity target, BaseSkillData baseSkillData, DamageSkillData damageSkillData)
    {
        float attributeDamageMultiplier = BattleUtils.GetDamageMultiplierBySkillAttribute(caster, damageSkillData.AttackType, target, damageSkillData.DefenceType);
        float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(baseSkillData.SkilType[0], target.BaseEntityType.EntityTypeCollection[0]);
        float attackRandomizedValue = BattleUtils.GetRandomSkillDamage(damageSkillData.DamageRangeValue.x, damageSkillData.DamageRangeValue.y);
        float totalDamage = BattleUtils.CalculateTotalDamage(attributeDamageMultiplier, typeDamageMultiplier, attackRandomizedValue);

        caster.PayForSkill(baseSkillData.Cost);
        target.GetDamaged(new EntityDamageData(attributeDamageMultiplier, typeDamageMultiplier, attackRandomizedValue, totalDamage, baseSkillData.GameobjectToSpawnOnHitTarget));
    }

    public static bool TryToApplyStatusEffect (BaseScriptableEntityStatusEffect baseScriptableStatusEffect, Entity target, Battle currentBattle, int numberOfStacksToAdd, out EntityStatusEffect createdStatusEffect)
    {
        bool hasStatusBeenApplied = false;
        createdStatusEffect = null;

        if (target.IsAlive.PresentValue == true)
        {
            EntityStatusEffect statusInEntity = GetStatusOfTypeFromEntity(baseScriptableStatusEffect, target);

            if (statusInEntity == null)
            {
                createdStatusEffect = new EntityStatusEffect(baseScriptableStatusEffect, target, currentBattle, numberOfStacksToAdd);
                target.PresentStatusEffects.Add(createdStatusEffect);
                hasStatusBeenApplied = true;
            }
            else
            {
                AddStacksToStatusEffect(statusInEntity, numberOfStacksToAdd);
            }
        }

        return hasStatusBeenApplied;
    }

    public static void RestoreResource (Entity targetEntity, ResourceToChangeType typeOfResourceToChange, bool percentageValue, float value)
    {
        Resource<float> resourceToChange = typeOfResourceToChange == ResourceToChangeType.HEALTH ? targetEntity.ModifiedStats.Health : targetEntity.ModifiedStats.Mana;
        float newValue;

        if (percentageValue == true)
        {
            newValue = resourceToChange.CurrentValue.PresentValue + (resourceToChange.MaxValue.PresentValue * value);
        }
        else
        {
            newValue = resourceToChange.CurrentValue.PresentValue + value;
        }

        newValue = Mathf.Clamp(newValue, 0, resourceToChange.MaxValue.PresentValue);
        resourceToChange.CurrentValue.PresentValue = newValue;
    }

    public static void RemoveStatusEffect (Entity target, BaseScriptableEntityStatusEffect baseScriptableStatusEffect, int stacksToRemove)
    {
        EntityStatusEffect statusInEntity = GetStatusOfTypeFromEntity(baseScriptableStatusEffect, target);

        if (statusInEntity != null)
        {
            int numberOfStacks = statusInEntity.CurrentNumberOfStacks.PresentValue - stacksToRemove;

            if (numberOfStacks > 0)
            {
                statusInEntity.CurrentNumberOfStacks.PresentValue = numberOfStacks;
            }
            else
            {
                RemoveAllStacksOfStatusEffect(target, baseScriptableStatusEffect);
            }
        }
    }

    public static void RemoveAllStacksOfStatusEffect (Entity target, BaseScriptableEntityStatusEffect baseScriptableStatusEffect)
    {
        EntityStatusEffect statusInEntity = GetStatusOfTypeFromEntity(baseScriptableStatusEffect, target);

        target.PresentStatusEffects.Remove(statusInEntity);
        statusInEntity.Cleanup();
        statusInEntity.InvokeOnRemoved();
    }

    public static BattlegroundStatusEffect ApplyStatusEffectToBattleground (BaseScriptableBattlegroundStatusEffect effectData, Battle target)
    {
        BattlegroundStatusEffect createdStatusEffect = new BattlegroundStatusEffect(effectData, target);
        target.BattlegroundStatusEffects.Add(createdStatusEffect);
        return createdStatusEffect;
    }

    public static void RemoveStatusEffectFromBattleground (Battle target, BaseScriptableBattlegroundStatusEffect effectData)
    {
        BattlegroundStatusEffect statusToRemove = target.BattlegroundStatusEffects.FirstOrDefault(n => n.BaseStatusEffect == effectData);

        if (statusToRemove != null)
        {
            statusToRemove.InvokeOnRemoved();
            statusToRemove.Cleanup();
            target.BattlegroundStatusEffects.Remove(statusToRemove);
        }
    }

    public static void Cleanse (Entity target)
    {
        target.Cleanse();
    }

    public static void CleanseBattleground (Battle target)
    {
        target.BattlegroundStatusEffects.Clear();
    }

    public static void Retreat (Entity target, Battle currentBattle)
    {
        BattleParticipant participant = currentBattle.GetParticipantByEntity(target);
        participant.CurrentEntity.PresentValue = participant.GetRandomAliveEntity();
    }

    public static EntityStatusEffect GetStatusOfTypeFromEntity (BaseScriptableEntityStatusEffect baseScriptableStatusEffect, Entity target)
    {
        return target.PresentStatusEffects.FirstOrDefault(n => n.BaseStatusEffect == baseScriptableStatusEffect);
    }

    private static void AddStacksToStatusEffect (EntityStatusEffect targetStatus, int stacksToAdd)
    {
        int numberOfStacks = targetStatus.CurrentNumberOfStacks.PresentValue + stacksToAdd;
        targetStatus.CurrentNumberOfStacks.PresentValue = Mathf.Clamp(numberOfStacks, 0, targetStatus.BaseStatusEffect.MaxStacks);
    }
}

