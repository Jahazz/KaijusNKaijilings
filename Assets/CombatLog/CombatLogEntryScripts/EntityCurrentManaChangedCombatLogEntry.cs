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

        public EntityCurrentManaChangedCombatLogEntry (BattleParticipant entityOwner, Entity entityThatResourceChanged, float oldValue, float newValue)
        {
            EntityOwner = entityOwner;
            EntityThatResourceChanged = entityThatResourceChanged;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, EntityOwner.Player.Name, EntityThatResourceChanged.Name, EntityThatResourceChanged.BaseEntityType.Name, OldValue, NewValue);
        }
    }
}
