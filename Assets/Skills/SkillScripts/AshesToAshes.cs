using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using StatusEffects;
using StatusEffects.BattlegroundStatusEffects;

namespace Skills
{
    [CreateAssetMenu(fileName = nameof(AshesToAshes), menuName = "ScriptableObjects/Skills/" + nameof(AshesToAshes))]
    public class AshesToAshes : SkillScriptableObject
    {
        [field: SerializeField]
        private StatusEffects.BattlegroundStatusEffects.AshesToAshes StatusEffectToApply { get; set; }

        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);
            StatusEffectToApply.ApplyStatus(currentBattle);
            float casterHp = caster.ModifiedStats.Health.MaxValue.PresentValue;
            caster.GetDamaged(new EntityDamageData(1,1, casterHp, casterHp, null));
        }

    }
}

