using BattleCore;
using UnityEngine;

namespace StatusEffects.EntityStatusEffects
{
    public abstract class BaseEntityScriptableStatusEffect : BaseScriptableStatusEffect
    {
        [field: Space]
        [field: SerializeField]
        public StatusEffectType StatusEffectType { get; private set; }

        [field: Space]
        [field: SerializeField]
        public bool Cleansable { get; private set; }
        [field: SerializeField]
        public bool RemovedAtEndOfCombat { get; private set; }
        [field: SerializeField]
        public bool RemovedOnDeath { get; private set; }

        public abstract void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle);

        protected void AddRemoveEvents (BaseStatusEffect<BaseEntityScriptableStatusEffect> createdStatusEffect, Battle currentBattle, params Entity[] statusTargets)
        {
            foreach (Entity entityWithStatus in statusTargets)
            {
                AddEventsToEntiy(createdStatusEffect, currentBattle, entityWithStatus);
            }
        }

        private void AddEventsToEntiy (BaseStatusEffect<BaseEntityScriptableStatusEffect> createdStatusEffect, Battle currentBattle, Entity entityWithStatus)
        {
            if (Cleansable == true)
            {
                AddOnCleanseEvents(createdStatusEffect, entityWithStatus);
            }

            if (RemovedAtEndOfCombat == true)
            {
                AddEndOfCombatEvents(createdStatusEffect, currentBattle, entityWithStatus);
            }

            if (RemovedOnDeath == true)
            {
                AddOnDeathEvents(createdStatusEffect, entityWithStatus);
            }
        }

        private void AddOnCleanseEvents (BaseStatusEffect<BaseEntityScriptableStatusEffect> createdStatusEffect, Entity entityWithStatus)
        {
            entityWithStatus.OnCleanse += HandleOnEntityCleansed;

            void HandleOnEntityCleansed ()
            {
                entityWithStatus.OnCleanse -= HandleOnEntityCleansed;
                SkillUtils.RemoveStatusEffect(entityWithStatus, createdStatusEffect);
            }
        }

        private void AddEndOfCombatEvents (BaseStatusEffect<BaseEntityScriptableStatusEffect> createdStatusEffect, Battle currentBattle, Entity entityWithStatus)
        {
            currentBattle.OnBattleFinished += HandleOnEndOfCombat;

            void HandleOnEndOfCombat (BattleResultType battleResult)
            {
                currentBattle.OnBattleFinished -= HandleOnEndOfCombat;
                SkillUtils.RemoveStatusEffect(entityWithStatus, createdStatusEffect);
            }
        }

        private void AddOnDeathEvents (BaseStatusEffect<BaseEntityScriptableStatusEffect> createdStatusEffect, Entity entityWithStatus)
        {
            entityWithStatus.IsAlive.OnVariableChange += HandleOnEntityDeath;

            void HandleOnEntityDeath (bool newValue)
            {
                if (newValue == false)
                {
                    entityWithStatus.IsAlive.OnVariableChange -= HandleOnEntityDeath;
                    SkillUtils.RemoveStatusEffect(entityWithStatus, createdStatusEffect);
                }
            }
        }
    }
}

