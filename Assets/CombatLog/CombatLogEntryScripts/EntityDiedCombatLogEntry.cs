using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityDiedCombatLogEntry : BaseCombatLogEntry
    {
        public Entity Entity { get; private set; }
        public BattleParticipant Owner { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_DIED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
