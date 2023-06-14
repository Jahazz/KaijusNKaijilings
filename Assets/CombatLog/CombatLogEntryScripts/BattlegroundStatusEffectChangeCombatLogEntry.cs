using StatusEffects.BattlegroundStatusEffects;

namespace CombatLogging.Entries
{
    public class BattlegroundStatusEffectChangeCombatLogEntry : BaseCombatLogEntry
    {
        public ActionType ActionType { get; private set; }
        public BattlegroundStatusEffect TargetStatusEffect { get; set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.BATTLEGROUND_STATUS_EFFECT_CHANGED;
        protected override string ENTRY_FORMAT { get; set; } = "Battleground status effect {0} has been {1}.";

        public BattlegroundStatusEffectChangeCombatLogEntry (ActionType actionType, BattlegroundStatusEffect targetStatusEffect)
        {
            ActionType = actionType;
            TargetStatusEffect = targetStatusEffect;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(TargetStatusEffect.BaseStatusEffect), ActionType.ToString().ToLower());
        }
    }
}

