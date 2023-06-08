using BattleCore;
using StatusEffects.EntityStatusEffects;

namespace CombatLogging.Entries
{
    public class EntityStatusEffectsChangedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant EntityOwner { get; private set; }
        public Entity TargetEntity { get; private set; }
        public BaseScriptableEntityStatusEffect TargetStatusEffect { get; private set; }
        public ActionType ActionType { get; private set; }
        public float OldNumberOfStacks { get; private set; }
        public float NewNumberOfStacks { get; private set; }

        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_STATUS_EFFECT_CHANGED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
