using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Skills
{
    [CreateAssetMenu(fileName = nameof(BasicDamagingSkill), menuName = "ScriptableObjects/Skills/" + nameof(BasicDamagingSkill))]
    public class BasicDamagingSkill : SkillScriptableObject
    {
        public override void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
        {
            base.UseSkill(casterOwner, caster, target, currentBattle);

            SkillUtils.UseDamagingSkill(caster, target, BaseSkillData, DamageData);
        }
    }
}

