using BattleCore;
using StatusEffects.EntityStatusEffects;

namespace CombatLogging.Entries
{
    public class EntityStatusEffectsChangedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleParticipant EntityOwner { get; private set; }
        public Entity TargetEntity { get; private set; }
        public EntityStatusEffect TargetStatusEffect { get; private set; }
        public ActionType ActionType { get; private set; }
        public float OldNumberOfStacks { get; private set; }
        public float NewNumberOfStacks { get; private set; }
        protected override string ENTRY_FORMAT { get; set; } = "Status effect {0} has been {1} to player {2} entity {3}({4}).";

        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.ENTITY_STATUS_EFFECT_CHANGED;

        public EntityStatusEffectsChangedCombatLogEntry (BattleParticipant entityOwner, Entity targetEntity, EntityStatusEffect targetStatusEffect, ActionType actionType, float oldNumberOfStacks, float newNumberOfStacks)
        {
            EntityOwner = entityOwner;
            TargetEntity = targetEntity;
            TargetStatusEffect = targetStatusEffect;
            ActionType = actionType;
            OldNumberOfStacks = oldNumberOfStacks;
            NewNumberOfStacks = newNumberOfStacks;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(TargetStatusEffect.BaseStatusEffect), ActionType.ToString().ToLower(), EntityOwner.Player.Name, TargetEntity.Name.PresentValue, SingletonContainer.Instance.TooltipManager.GenerateTooltipableURL(TargetEntity.BaseEntityType));
        }
    }
}
