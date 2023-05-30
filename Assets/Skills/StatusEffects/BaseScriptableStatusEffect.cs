using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects
{
    public abstract class BaseScriptableStatusEffect : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }
        [field: SerializeField]
        public string Description { get; private set; }
        [field: SerializeField]
        public Sprite Image { get; set; }

        [field: Space]
        public StatusEffectType IsPositive { get; private set; }

        [field: Space]
        [field: SerializeField]
        public bool Cleansable { get; private set; }
        [field: SerializeField]
        public bool RemovedAtEndOfCombat { get; private set; }
        [field: SerializeField]
        public bool RemovedOnDeath { get; private set; }

        public abstract void ApplyStatus (BattleParticipant casterOwner, Entity caster, Entity target, Battle currentBattle);

        protected void AddRemoveEvents (BaseStatusEffect createdStatusEffect, Battle currentBattle, params Entity[] statusTargets)
        {
            foreach (Entity entityWithStatus in statusTargets)
            {
                AddEventsToEntiy(createdStatusEffect, currentBattle, entityWithStatus);
            }
        }

        private void AddEventsToEntiy (BaseStatusEffect createdStatusEffect, Battle currentBattle, Entity entityWithStatus)
        {
            if (Cleansable == true)
            {
                AddOnCleanseEvents(createdStatusEffect, currentBattle, entityWithStatus);
            }

            if (RemovedAtEndOfCombat == true)
            {
                AddEndOfCombatEvents(createdStatusEffect, currentBattle, entityWithStatus);
            }

            if (RemovedOnDeath == true)
            {
                AddOnDeathEvents(createdStatusEffect, currentBattle, entityWithStatus);
            }
        }

        private void AddEndOfCombatEvents (BaseStatusEffect createdStatusEffect, Battle currentBattle, Entity entityWithStatus)
        {
            currentBattle.OnBattleFinished += HandleOnEndOfCombat;

            void HandleOnEndOfCombat (BattleResultType battleResult)
            {
                currentBattle.OnBattleFinished -= HandleOnEndOfCombat;
                SkillUtils.RemoveStatusEffect(entityWithStatus, createdStatusEffect);
            }
        }

        private void AddOnDeathEvents (BaseStatusEffect createdStatusEffect, Battle currentBattle, Entity entityWithStatus)
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

        private void AddOnCleanseEvents (BaseStatusEffect createdStatusEffect, Battle currentBattle, Entity entityWithStatus)
        {
            entityWithStatus.OnCleanse += HandleOnEntityCleansed;

            void HandleOnEntityCleansed ()
            {
                entityWithStatus.OnCleanse -= HandleOnEntityCleansed;
                SkillUtils.RemoveStatusEffect(entityWithStatus, createdStatusEffect);
            }
        }
    }
}

