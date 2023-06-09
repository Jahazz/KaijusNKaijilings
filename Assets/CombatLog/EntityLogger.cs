using CombatLogging.Entries;
using StatusEffects.EntityStatusEffects;
using System.Collections.Generic;
using System.Collections.Specialized;
using static Utils.ObservableVariable<float>;

namespace CombatLogging.EventHandling
{
    public class EntityLogger
    {
        private Entity CurrentEntity { get; set; }
        private Dictionary<StatType, VariableChangedArguments> OnStatChangeHook { get; set; } = new Dictionary<StatType, VariableChangedArguments>();
        private StatType[] statTypesToListen = new StatType[] { StatType.MIGHT, StatType.MAGIC, StatType.WILLPOWER, StatType.AGILITY, StatType.INITIATIVE };
        private BattleParticipantLogger BattleParticipantLogger { get; set; }

        public EntityLogger (Entity entity, BattleParticipantLogger battleParticipantLogger)
        {
            Dispose();

            CurrentEntity = entity;
            BattleParticipantLogger = battleParticipantLogger;

            CurrentEntity.OnDamaged += HandleOnDamaged;
            CurrentEntity.ModifiedStats.Mana.CurrentValue.OnVariableChange += HandleOnCurrentManaChanged;
            CurrentEntity.ModifiedStats.Mana.MaxValue.OnVariableChange += HandleOnMaxManaChanged;
            CurrentEntity.ModifiedStats.Health.MaxValue.OnVariableChange += HandleOnMaxHealthChanged;
            CurrentEntity.PresentStatusEffects.CollectionChanged += HandleOnStatusEffectsChanged;

            foreach (StatType statType in statTypesToListen)
            {
                VariableChangedArguments variableChangedArguments = (newValue, oldValue) => HandleOnStatChanged(statType, newValue, oldValue);
                CurrentEntity.ModifiedStats.GetStatOfType(statType).OnVariableChange += variableChangedArguments;
                OnStatChangeHook.Add(statType, variableChangedArguments);
            }
        }

        private void HandleOnStatusEffectsChanged (object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if(eventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object item in eventArgs.NewItems)
                {
                    InvokeStatusEffctEvent(ActionType.ADDED, item);
                }
            }

            if (eventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (object item in eventArgs.OldItems)
                {
                    InvokeStatusEffctEvent(ActionType.REMOVED, item);
                }
            }
        }

        private void InvokeStatusEffctEvent (ActionType actionType, object targetObject)
        {
            BattleParticipantLogger.BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityStatusEffectsChangedCombatLogEntry(BattleParticipantLogger.CurrentBattleParticipant,CurrentEntity, targetObject as BaseScriptableEntityStatusEffect, actionType,1,1 ));
        }

        private void HandleOnStatChanged (StatType statType, float newValue, float oldValue)
        {
            BattleParticipantLogger.BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityStatValueChangedCombatLogEntry(BattleParticipantLogger.CurrentBattleParticipant, CurrentEntity, statType, oldValue, newValue));
        }

        private void HandleOnMaxHealthChanged (float newValue, float oldValue)
        {
            BattleParticipantLogger.BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityResourceMaxValueChangedCombatLogEntry(BattleParticipantLogger.CurrentBattleParticipant, CurrentEntity, ResourceChangedType.HEALTH, oldValue, newValue));

        }

        private void HandleOnMaxManaChanged (float newValue, float oldValue)
        {
            BattleParticipantLogger.BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityResourceMaxValueChangedCombatLogEntry(BattleParticipantLogger.CurrentBattleParticipant, CurrentEntity, ResourceChangedType.MANA, oldValue, newValue));
        }

        private void HandleOnCurrentManaChanged (float newValue, float oldValue)
        {
            BattleParticipantLogger.BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityCurrentManaChangedCombatLogEntry(BattleParticipantLogger.CurrentBattleParticipant, CurrentEntity, oldValue, newValue));
        }

        private void HandleOnDamaged (EntityDamageData damage)
        {
            BattleParticipantLogger.BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityDamagedCombatLogEntry(BattleParticipantLogger.CurrentBattleParticipant, CurrentEntity, damage));
        }

        //HP_CHANGED,
        //MANA_CHANGED,
        //ADD_STATUS_EFFECT,
        //REMOVE_STATUS_EFFECT,

        public void Dispose ()
        {
            CurrentEntity.OnDamaged -= HandleOnDamaged;
            CurrentEntity.ModifiedStats.Mana.CurrentValue.OnVariableChange -= HandleOnCurrentManaChanged;
            CurrentEntity.ModifiedStats.Mana.MaxValue.OnVariableChange -= HandleOnMaxManaChanged;
            CurrentEntity.ModifiedStats.Health.MaxValue.OnVariableChange -= HandleOnMaxHealthChanged;
            CurrentEntity.PresentStatusEffects.CollectionChanged -= HandleOnStatusEffectsChanged;

            foreach (KeyValuePair<StatType, VariableChangedArguments> variableChangedEventKeyValueHandler in OnStatChangeHook)
            {
                CurrentEntity.ModifiedStats.GetStatOfType(variableChangedEventKeyValueHandler.Key).OnVariableChange += variableChangedEventKeyValueHandler.Value;
            }

            CurrentEntity = null;
        }
    }
}
