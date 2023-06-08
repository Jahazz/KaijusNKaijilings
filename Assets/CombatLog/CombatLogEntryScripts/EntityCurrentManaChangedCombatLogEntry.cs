using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityCurrentManaChangedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant EntityOwner { get; private set; }
        public Entity EntityThatResourceChanged { get; private set; }
        public float OldValue { get; private set; }
        public float NewValue { get; private set; }
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) mana has been changed from {3} to {4}.";

        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_MANA_CHANGED;

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, EntityOwner.Player.Name, EntityThatResourceChanged.Name, EntityThatResourceChanged.BaseEntityType.Name, OldValue, NewValue);
        }
    }
}
