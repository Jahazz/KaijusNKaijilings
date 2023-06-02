using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StatusEffects.EntityStatusEffects
{
    [CreateAssetMenu(fileName = nameof(StatBuff), menuName = "ScriptableObjects/StatusEffects/" + nameof(StatBuff))]
    public class StatBuff : BaseScriptableEntityStatusEffect
    {
        [field: SerializeField]
        private StatType StatType { get; set; }
        [field: SerializeField]
        private float ModificationValue { get; set; }
        [field: SerializeField]
        private StatBuff InvertedScriptableEffect { get; set; }
        [field: SerializeField]
        private float BaseMultiplier { get; set; }

        public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle, int stacksToApply)
        {
            EntityStatusEffect invertedEffectObjectOnTarget = SkillUtils.GetStatusOfTypeFromEntity(InvertedScriptableEffect, target);

            if (invertedEffectObjectOnTarget != null)//has negative effect
            {
                stacksToApply = invertedEffectObjectOnTarget.CurrentNumberOfStacks.PresentValue - stacksToApply;
                SkillUtils.RemoveStatusEffect(target, InvertedScriptableEffect, stacksToApply);

            }

            if (stacksToApply > 0)
            {
                EntityStatusEffect createdStatusEffect;
                bool hasStatusBeenCreated = SkillUtils.TryToApplyStatusEffect(this, target, currentBattle, stacksToApply, out createdStatusEffect);

                if (hasStatusBeenCreated == true)
                {
                    StatModifier createdStatModifier = null;

                    createdStatusEffect.CurrentNumberOfStacks.OnVariableChange += CurrentNumberOfStacks_OnVariableChange;

                    void CurrentNumberOfStacks_OnVariableChange (int newValue)
                    {
                        if (createdStatModifier != null)
                        {
                            target.StatModifiers.Remove(createdStatModifier);
                        }

                        createdStatModifier = CreateModifier(stacksToApply);

                        target.StatModifiers.Add(createdStatModifier);
                    }
                }
            }
        }

        

        private StatModifier CreateModifier (int numberOfStacks)
        {
            float value = StatusEffectType == StatusEffectType.BUFF ? 1 + (numberOfStacks * BaseMultiplier) : 1 - (numberOfStacks * BaseMultiplier);
            return new StatModifier(StatModifierType.MULTIPLY, StatType, value); ;
        }
    }
}
