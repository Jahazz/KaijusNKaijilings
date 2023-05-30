using BattleCore;
using StatusEffects;
using StatusEffects.BattlegroundStatusEffects;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public static void ApplyStatusEffect (Entity target, BaseStatusEffect<BaseEntityScriptableStatusEffect> effectData)
    {
        target.PresentStatusEffects.Add(effectData);
    }

    public static void RemoveStatusEffect (Entity target, BaseStatusEffect<BaseEntityScriptableStatusEffect> effectData)
    {
        effectData.InvokeOnRemoved();
        target.PresentStatusEffects.Remove(effectData);
    }

    public static void ApplyStatusEffectToBattleground (Battle target, BaseStatusEffect<BaseScriptableBattlegroundStatusEffect> effectData)
    {
        target.BattlegroundStatusEffects.Add(effectData);
    }

    public static void RemoveStatusEffectFromBattleground (Battle target, BaseStatusEffect<BaseScriptableBattlegroundStatusEffect> effectData)
    {
        effectData.InvokeOnRemoved();
        target.BattlegroundStatusEffects.Remove(effectData);
    }
} 

