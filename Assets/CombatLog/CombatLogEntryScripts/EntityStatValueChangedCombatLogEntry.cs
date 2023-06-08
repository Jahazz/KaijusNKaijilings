using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityStatValueChangedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant EntityOwner { get; private set; }
        public Entity EntityThatResourceChanged { get; private set; }
        public StatType StatThatChanged { get; private set; }

        public float OldValue { get; private set; }
        public float NewValue { get; private set; }

        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_STAT_CHANGED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
