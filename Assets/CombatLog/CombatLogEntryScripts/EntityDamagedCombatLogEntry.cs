using BattleCore;

namespace CombatLogging.Entries
{
    public class EntityDamagedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant DamagedEntityOwner { get; private set; }
        public Entity DamagedEntity { get; private set; }
        public EntityDamageData DamageData { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_DAMAGED;
        protected override string ENTRY_FORMAT { get; set; } = "Player {0} entity {1}({2}) has been damaged for {3}.";

        public EntityDamagedCombatLogEntry (BattleParticipant damagedEntityOwner, Entity damagedEntity, EntityDamageData damageData)
        {
            DamagedEntityOwner = damagedEntityOwner;
            DamagedEntity = damagedEntity;
            DamageData = damageData;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, DamagedEntityOwner.Player.Name, DamagedEntity.Name.PresentValue, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(DamagedEntity.BaseEntityType), DamageData.TotalDamage);
        }
    }
}
