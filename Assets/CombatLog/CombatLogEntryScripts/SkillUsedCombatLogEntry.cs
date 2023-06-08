using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatLogging.Entries
{
    public class SkillUsedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant SkillCasterOwner { get; private set; }
        public Entity SkillCaster{ get; private set; }
        public Entity SkillTarget { get; private set; }
        public Battle SkillCurrentBattle { get; private set; }
        public SkillScriptableObject UsedSkill { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.SKILL_USED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
