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
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) casted ability {3}.";

        public SkillUsedCombatLogEntry (BattleParticipant skillCasterOwner, Entity skillCaster, Entity skillTarget, Battle skillCurrentBattle, SkillScriptableObject usedSkill)
        {
            SkillCasterOwner = skillCasterOwner;
            SkillCaster = skillCaster;
            SkillTarget = skillTarget;
            SkillCurrentBattle = skillCurrentBattle;
            UsedSkill = usedSkill;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, SkillCasterOwner.Player.Name, SkillCaster.Name, SkillCaster.BaseEntityType.Name, UsedSkill.BaseSkillData.Name);
        }
    }
}
