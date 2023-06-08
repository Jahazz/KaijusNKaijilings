using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityDamagedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant DamagedEntityOwner { get; private set; }
        public Entity DamagedEntity { get; private set; }
        public EntityDamageData DamageData { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_DAMAGED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
