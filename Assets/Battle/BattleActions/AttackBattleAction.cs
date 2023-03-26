using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBattleAction : BaseBattleAction
{
    public Entity Caster { get; private set; }
    public Entity Target { get; private set; }
    public SkillScriptableObject Skill { get; private set; }

    public AttackBattleAction (Entity caster, Entity target, SkillScriptableObject skill)
    {
        ActionType = BattleActionType.ATTACK;
        Caster = caster;
        Target = target;
        Skill = skill;
    }

    public override void Invoke (BattleParticipant actionOwner, Battle battle)
    {
        base.Invoke(actionOwner, battle);
    }
}
