using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleCore.Actions
{
    public class AttackBattleAction : BaseBattleAction
    {
        public Entity Caster { get; private set; }
        public BattleParticipant Target { get; private set; }
        public SkillScriptableObject Skill { get; private set; }

        public AttackBattleAction (Entity caster, BattleParticipant target, SkillScriptableObject skill)
        {
            ActionType = BattleActionType.ATTACK;
            Caster = caster;
            Target = target;
            Skill = skill;
        }
    }
}
