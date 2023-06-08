using System;
using System.Collections.Generic;
using static Utils.ObservableVariable<float>;

namespace CombatLogging.EventHandling
{
    public class EntityLogger
    {
        private Entity CurrentEntity { get; set; }
        private Dictionary<StatType, VariableChangedArguments> OnStatChangeHook { get; set; } = new Dictionary<StatType, VariableChangedArguments>();
        private StatType[] statTypesToListen = new StatType[] { StatType.MIGHT, StatType.MAGIC, StatType.WILLPOWER, StatType.AGILITY, StatType.INITIATIVE };
        private BattleLogger BattleLoggerReference { get; set; }

        public EntityLogger (Entity entity, BattleLogger battleLoggerReference)
        {
            Dispose();

            CurrentEntity = entity;
            BattleLoggerReference = battleLoggerReference;

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

        private void HandleOnStatusEffectsChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void HandleOnStatChanged (StatType statType, float newValue, float oldValue)
        {
            throw new System.NotImplementedException();
        }

        private void HandleOnMaxHealthChanged (float newValue, float oldValue)
        {
            throw new System.NotImplementedException();
        }

        private void HandleOnMaxManaChanged (float newValue, float oldValue)
        {
            throw new System.NotImplementedException();
        }

        private void HandleOnCurrentManaChanged (float newValue, float oldValue)
        {
            throw new System.NotImplementedException();
        }

        private void HandleOnDamaged (EntityDamageData damage)
        {
            throw new System.NotImplementedException();
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
