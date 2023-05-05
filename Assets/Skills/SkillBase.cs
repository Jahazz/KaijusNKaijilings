using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : ScriptableObject
{

    protected Battle CurrentBattle { get; set; }
    protected BattleParticipant CasterOwner { get; set; }
    protected Entity Caster { get; set; }

    public virtual void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
    {
        CurrentBattle = currentBattle;
        CasterOwner = casterOwner;
        Caster = caster;
    }
}
