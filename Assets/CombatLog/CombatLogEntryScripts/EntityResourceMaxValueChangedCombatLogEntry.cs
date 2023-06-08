using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityResourceMaxValueChangedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant EntityOwner { get; private set; }
        public Entity EntityThatResourceChanged { get; private set; }
        public ResourceChangedType ResourceType { get; private set; }
        public float OldValue { get; private set; }
        public float NewValue { get; private set; }
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) maximum {3} has been changed from {4} to {5}.";

        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_MAX_RESOURCE_CHANGED;

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, EntityOwner.Player.Name, EntityThatResourceChanged.Name, EntityThatResourceChanged.BaseEntityType.Name,ResourceType.ToString().ToLower(), OldValue, NewValue);
        }
    }
}
