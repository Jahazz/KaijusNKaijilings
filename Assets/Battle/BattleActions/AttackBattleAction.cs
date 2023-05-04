using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCore.Actions
{
    public class AttackBattleAction : BaseBattleAction
    {
        public Entity Caster { get; private set; }
        public BattleParticipant CasterOwner { get; private set; }
        public BattleParticipant Target { get; private set; }
        public SkillScriptableObject Skill { get; private set; }

        public AttackBattleAction (BattleParticipant casterOwner, Entity caster, BattleParticipant target, SkillScriptableObject skill)
        {
            ActionType = BattleActionType.ATTACK;
            CasterOwner = casterOwner;
            Caster = caster;
            Target = target;
            Skill = skill;
        }
    }
}
