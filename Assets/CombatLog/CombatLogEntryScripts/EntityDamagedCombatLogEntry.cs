using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityDamagedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant DamagedEntityOwner { get; private set; }
        public Entity DamagedEntity { get; private set; }
        public EntityDamageData DamageData { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_DAMAGED;
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) has been damaged for {3}";

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, DamagedEntityOwner.Player.Name, DamagedEntity.Name, DamagedEntity.BaseEntityType.Name, DamageData.TotalDamage);
        }
    }
}
