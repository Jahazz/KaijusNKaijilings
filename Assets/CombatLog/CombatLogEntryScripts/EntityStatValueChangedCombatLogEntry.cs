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
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) {3} has been changed from {4} to {5}";
        public EntityStatValueChangedCombatLogEntry (BattleParticipant entityOwner, Entity entityThatResourceChanged, StatType statThatChanged, float oldValue, float newValue)
        {
            EntityOwner = entityOwner;
            EntityThatResourceChanged = entityThatResourceChanged;
            StatThatChanged = statThatChanged;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, EntityOwner.Player.Name, EntityThatResourceChanged.Name.PresentValue, EntityThatResourceChanged.BaseEntityType.name, OldValue, NewValue);
        }
    }
}
