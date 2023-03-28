using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BasicDamageSkill", menuName = "ScriptableObjects/SkillData/BasicDamageSkill", order = 1)]
public class BasicDamageSkill : SkillScriptableObject
{
    [field: SerializeField]
    public Vector2 DamageRangeValue { get; private set; }
    [field: SerializeField]
    public StatType AttackType { get; set; }
    [field: SerializeField]
    public StatType DefenceType { get; set; }

    public override void UseSkill (Entity caster, Entity target)
    {
        base.UseSkill(caster, target);

        float attributeDamageMultiplier = BattleUtils.GetDamageMultiplierBySkillAttribute(caster, AttackType, target, DefenceType);
        float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(SkilType[0], target.BaseEntityType.EntityTypeCollection[0]);
        float attackRandomizedValue = BattleUtils.GetRandomSkillDamage(DamageRangeValue.x, DamageRangeValue.y);
        float totalDamage = BattleUtils.CalculateTotalDamage(attributeDamageMultiplier, typeDamageMultiplier, attackRandomizedValue);
       
        caster.PayForSkill(Cost);
        target.GetDamaged(new EntityDamageData(attributeDamageMultiplier, typeDamageMultiplier, attackRandomizedValue, totalDamage, GameobjectToSpawnOnHitTarget));
    }
}
