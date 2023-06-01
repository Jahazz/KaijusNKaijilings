using BattleCore;
using StatusEffects.BattlegroundStatusEffects;
using StatusEffects.EntityStatusEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatusEffects.BattlegroundStatusEffects
{
    public class BattlegroundStatusEffect
    {
        public delegate void OnStatusEffectRemovedParams ();
        public event OnStatusEffectRemovedParams OnStatusEffectRemoved;
        public BaseScriptableBattlegroundStatusEffect BaseStatusEffect { get; private set; }
        private Battle CurrentBattle { get; set; }

        public BattlegroundStatusEffect (BaseScriptableBattlegroundStatusEffect baseStatusEffect, Battle currentBattle)
        {
            BaseStatusEffect = baseStatusEffect;
            CurrentBattle = currentBattle;
            AddEventsToBattleground();
        }

        public void Cleanup ()
        {
            CurrentBattle.OnBattlegroundCleanse -= HandleOnBattlegroundCleansed;
            CurrentBattle.OnBattleFinished -= HandleOnEndOfCombat;
        }

        private void AddEventsToBattleground ()
        {
            if (BaseStatusEffect.Cleansable == true)
            {
                CurrentBattle.OnBattlegroundCleanse += HandleOnBattlegroundCleansed;
            }

            if (BaseStatusEffect.RemovedAtEndOfCombat == true)
            {
                CurrentBattle.OnBattleFinished += HandleOnEndOfCombat;
            }
        }

        private void HandleOnBattlegroundCleansed ()
        {
            Cleanup();
            SkillUtils.RemoveStatusEffectFromBattleground(CurrentBattle, BaseStatusEffect);
        }

        void HandleOnEndOfCombat (BattleResultType _)
        {
            Cleanup();
            SkillUtils.RemoveStatusEffectFromBattleground(CurrentBattle, BaseStatusEffect);
        }

        public void InvokeOnRemoved ()
        {
            OnStatusEffectRemoved.Invoke();
        }
    }
}
