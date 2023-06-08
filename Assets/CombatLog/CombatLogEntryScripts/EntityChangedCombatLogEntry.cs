using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityChangedCombatLogEntry : BaseCombatLogEntry
    {
        public Entity EntityChangedTo { get; private set; }
        public Entity EntityChangedFrom { get; private set; }
        public BattleParticipant Owner { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_CHANGED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
