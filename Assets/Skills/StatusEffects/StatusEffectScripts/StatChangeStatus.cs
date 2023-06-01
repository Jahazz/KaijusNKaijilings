using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace StatusEffects.EntityStatusEffects
{
    public class StatChangeStatus : BaseScriptableEntityStatusEffect
    {
        [field: SerializeField]
        private StatType StatType { get; set; } 
        [field: SerializeField]
        private float ModificationValue { get; set; }


        public override void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle,int a)
        {
            EntityStatusEffect createdStatusEffect;
            bool hasStatusBeenApplied = SkillUtils.TryToApplyStatusEffect(this, target, currentBattle, 1, out createdStatusEffect);

            StatModifier CreatedModifier = new StatModifier(StatModifierType.MULTIPLY, StatType, 1);

            //if (hasStatusBeenApplied == true)
            //{
            //    target.StatModifiers.Add()
            //}

            //target.StatModifiers.Add()
        }

        public void UpdateStack (StatModifier CreatedModifier)
        {

        }
    }
}
