using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityChangedCombatLogEntry : BaseCombatLogEntry
    {
        public Entity EntityChangedTo { get; private set; }
        public Entity EntityChangedFrom { get; private set; }
        public BattleParticipant Owner { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_CHANGED;
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) has been swapped to {1}({2})";

        public EntityChangedCombatLogEntry (Entity entityChangedTo, Entity entityChangedFrom, BattleParticipant owner)
        {
            EntityChangedTo = entityChangedTo;
            EntityChangedFrom = entityChangedFrom;
            Owner = owner;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, Owner.Player.Name, EntityChangedFrom.Name, EntityChangedFrom.BaseEntityType.Name, EntityChangedTo.Name, EntityChangedTo.BaseEntityType.Name);
        }
    }
}
