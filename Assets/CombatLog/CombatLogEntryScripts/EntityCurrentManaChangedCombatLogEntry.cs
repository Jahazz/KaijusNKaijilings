using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityCurrentManaChangedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant EntityOwner { get; private set; }
        public Entity EntityThatResourceChanged { get; private set; }
        public float OldValue { get; private set; }
        public float NewValue { get; private set; }

        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_MANA_CHANGED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
