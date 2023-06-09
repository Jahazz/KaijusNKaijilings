using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityDiedCombatLogEntry : BaseCombatLogEntry
    {
        public Entity Entity { get; private set; }
        public BattleParticipant Owner { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_DIED;
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) has perished.";
        public EntityDiedCombatLogEntry (Entity entity, BattleParticipant owner)
        {
            Entity = entity;
            Owner = owner;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, Owner.Player.Name, Entity.Name, Entity.BaseEntityType.Name);
        }
    }
}
