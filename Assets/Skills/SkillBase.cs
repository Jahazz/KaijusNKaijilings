using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBase : ScriptableObject
{
    public virtual void UseSkill (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle)
    {

    }
}
