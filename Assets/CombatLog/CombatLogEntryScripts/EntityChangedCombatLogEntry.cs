using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityChangedCombatLogEntry : BaseCombatLogEntry
    {
        public Entity EntityChangedTo { get; private set; }
        public Entity EntityChangedFrom { get; private set; }
        public BattleParticipant Owner { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_CHANGED;
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) has been swapped to {3}({4})";
        private const string ENTRY_FORMAT_WITHOUT_OLD_VALUE = "Payer {0} summoned entity {1}({2})";

        public EntityChangedCombatLogEntry (Entity entityChangedTo, Entity entityChangedFrom, BattleParticipant owner)
        {
            EntityChangedTo = entityChangedTo;
            EntityChangedFrom = entityChangedFrom;
            Owner = owner;
        }

        public override string EntryToString ()
        {
            string output;

            if (EntityChangedFrom == null)
            {
                output = string.Format(ENTRY_FORMAT_WITHOUT_OLD_VALUE, Owner.Player.Name, EntityChangedTo.Name.PresentValue, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(EntityChangedTo.BaseEntityType));
            }
            else
            {
                output = string.Format(ENTRY_FORMAT, Owner.Player.Name, EntityChangedFrom.Name.PresentValue, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(EntityChangedFrom.BaseEntityType), EntityChangedTo.Name.PresentValue, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(EntityChangedTo.BaseEntityType));
            }

            return output;
        }
    }
}
