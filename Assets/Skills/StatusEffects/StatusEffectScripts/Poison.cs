using BattleCore;
using StatusEffects.EntityStatusEffects;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(Poison), menuName = "ScriptableObjects/StatusEffects/" + nameof(Poison))]
public class Poison : BaseScriptableEntityStatusEffect
{
    [field: SerializeField]
    private float DamagePerStack { get; set; }

    public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int numberOfStacksToAdd)
    {
        EntityStatusEffect createdStatusEffect;
        bool hasStatusBeenApplied = SkillUtils.TryToApplyStatusEffect(this, target, currentBattle, numberOfStacksToAdd, out createdStatusEffect);

        if (hasStatusBeenApplied == true)
        {
            currentBattle.OnSkillUse += Wrapper;
            createdStatusEffect.OnStatusEffectRemoved += HandleOnStatusEffectRemoved;

            void Wrapper (BattleParticipant skillCasterOwner, Entity skillCaster, Entity skillTarget, Battle skillCurrentBattle, SkillScriptableObject usedSkill)
            {
                if (skillCaster == target)
                {
                    float typeDamageMultiplier = BattleUtils.GetDamageMultiplierByType(SkilType[0], skillCaster.BaseEntityType.EntityTypeCollection[0]);
                    float damageValue = DamagePerStack * createdStatusEffect.CurrentNumberOfStacks.PresentValue;
                    skillCaster.GetDamaged(new EntityDamageData(1.0f, typeDamageMultiplier, damageValue, damageValue, null));
                }
            }

            void HandleOnStatusEffectRemoved ()
            {
                currentBattle.OnSkillUse -= Wrapper;
            }
        }
    }


}
