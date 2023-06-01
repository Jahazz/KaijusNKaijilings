using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using StatusEffects;

namespace Skills
{
    [CreateAssetMenu(fileName = nameof(HauntingOmen), menuName = "ScriptableObjects/Skills/" + nameof(HauntingOmen))]
    public class HauntingOmen : SkillScriptableObject
    {
        [field: SerializeField]
        private StatusEffects.EntityStatusEffects.HauntingOmen StatusEffectToApply { get; set; }

        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);

            SkillUtils.UseDamagingSkill(caster, target, BaseSkillData, DamageData);//Deals 10 damage

            caster.ModifiedStats.Mana.CurrentValue.PresentValue += caster.ModifiedStats.Mana.MaxValue.PresentValue * 0.2f;//regains 20% total mana 

            StatusEffectToApply.ApplyStatus(casterOwner, caster, caster, currentBattle,1);
        }
    }
}

