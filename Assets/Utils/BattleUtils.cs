using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class BattleUtils
{
    private const float STAT_DAMAGE_MULTIPLIER_MIN = 0.5f;
    private const float STAT_DAMAGE_MULTIPLIER_MAX = 2.0f;

    public static float GetDamageMultiplierBySkillAttribute (Entity attacker, StatType attackerType, Entity defender, StatType defenderType)
    {
        float attackerStatValue = attacker.ModifiedStats.GetStatOfType(attackerType).PresentValue;
        float defenderStatValue = defender.ModifiedStats.GetStatOfType(defenderType).PresentValue;
        float attackDamageStatFactor = attackerStatValue / defenderStatValue;
        float clampedValue = Mathf.Clamp(attackDamageStatFactor, STAT_DAMAGE_MULTIPLIER_MIN, STAT_DAMAGE_MULTIPLIER_MAX);

        Debug.Log(string.Format("DAMAGE MULTIPLIER BY ATTRIBUTE \n attacker {0}:{1} vs defender {2}:{3}. Result = x {4}(Clamped :{5})", attackerType, attackerStatValue, defenderType, defenderStatValue, attackDamageStatFactor, clampedValue));

        return clampedValue;
    }

    public static float GetDamageMultiplierByType (TypeDataScriptable attackerType, TypeDataScriptable defenderType)
    {
        float multiplier = attackerType.AttackerMultiplierCollection.Where(n => n.TypeData == defenderType).FirstOrDefault().Multiplier;

        Debug.Log(string.Format("DAMAGE MULTIPLIER BY TYPE \n attacker {0} vs defender {1}. Result = x {2}", attackerType.name, defenderType.name, multiplier));

        return multiplier;
    }

    public static float GetRandomSkillDamage (float min, float max)
    {
        float randomValue = Random.Range(min, max);

        Debug.Log(string.Format("RANDOM DAMAGE ({0}-{1}): {2}", min, max, randomValue));

        return randomValue;
    }

    public static float CalculateTotalDamage (float attributeDamageMultiplier, float typeDamageMultiplier, float attackRandomizedValue)
    {
        float totalAttackDamage = attributeDamageMultiplier * typeDamageMultiplier * attackRandomizedValue;

        Debug.Log(string.Format("TOTAL DAMAGE: {0}*{1}*{2}={3}", attributeDamageMultiplier, typeDamageMultiplier, attackRandomizedValue, totalAttackDamage));

        return totalAttackDamage;
    }
}
