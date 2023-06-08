using BattleCore;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Specialized;
using CombatLogging.Entries;
using StatusEffects.BattlegroundStatusEffects;

namespace CombatLogging.EventHandling
{
    public class BattleLogger : MonoBehaviour
    {
        public delegate void OnLogEntryCreatedArguments (BaseCombatLogEntry createdEntry);
        public event OnLogEntryCreatedArguments OnLogEntryCreated;

        private Battle CurrentBattle { get; set; }
        private List<BattleParticipantLogger> BattleParticipantsCollection = new List<BattleParticipantLogger>();

        public void Initialize (Battle currentBattle)
        {
            Dispose();

            CurrentBattle = currentBattle;

            foreach (BattleParticipant participant in currentBattle.BattleParticipantsCollection)
            {
                BattleParticipantsCollection.Add(new BattleParticipantLogger(participant, this));
            }

            CurrentBattle.BattlegroundStatusEffects.CollectionChanged += HandleBattlegroundStatusEffectsChanged;
            CurrentBattle.OnBattleFinished += HandleOnBattleFinished;
            CurrentBattle.OnTurnStart += HandleOnTurnStart;
            CurrentBattle.OnTurnEnd += HandleOnTurnEnd;
            CurrentBattle.OnSkillUse += HandleOnSkillUse;
            CurrentBattle.OnEntityDeath += HandleOnEntityDeath;
        }

        internal void InvokeOnEntryLogCreatedEvent (BaseCombatLogEntry createdEntry)
        {
            OnLogEntryCreated?.Invoke(createdEntry);
        }

        private void Dispose ()
        {
            CurrentBattle.BattlegroundStatusEffects.CollectionChanged -= HandleBattlegroundStatusEffectsChanged;
            CurrentBattle.OnBattleFinished -= HandleOnBattleFinished;
            CurrentBattle.OnTurnStart -= HandleOnTurnStart;
            CurrentBattle.OnTurnEnd -= HandleOnTurnEnd;
            CurrentBattle.OnSkillUse -= HandleOnSkillUse;
            CurrentBattle.OnEntityDeath -= HandleOnEntityDeath;

            CurrentBattle = null;

            foreach (BattleParticipantLogger participentLogger in BattleParticipantsCollection)
            {
                participentLogger.Dispose();
            }

            BattleParticipantsCollection.Clear();
        }

        //ADD_BATTLEGROUND_STATUS_EFFECT,
        //REMOVE_BATTLEGROUND_STATUS_EFFECT,
        private void HandleBattlegroundStatusEffectsChanged (object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.Action == NotifyCollectionChangedAction.Add)
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
            InvokeOnEntryLogCreatedEvent(new BattlegroundStatusEffectChangeCombatLogEntry(actionType, targetObject as BattlegroundStatusEffect));
        }

        //RESULT,
        private void HandleOnBattleFinished (BattleResultType battleResult)
        {
            InvokeOnEntryLogCreatedEvent(new BattleFinishedCombatLogEntry(battleResult, CurrentBattle.BattleParticipantsCollection[0].Player, CurrentBattle.BattleParticipantsCollection[1].Player));
        }
        //NEW_TURN/endTurn
        //    USE_ABILITY,

        private System.Collections.IEnumerator HandleOnTurnStart (int turnCOunter)
        {
            InvokeOnEntryLogCreatedEvent(new TurnEventCombatLogEntry(TurnEventType.STARTED, turnCOunter));
            yield return null;
        }

        private System.Collections.IEnumerator HandleOnTurnEnd (int turnCOunter)
        {
            InvokeOnEntryLogCreatedEvent(new TurnEventCombatLogEntry(TurnEventType.FINISHED, turnCOunter));
            yield return null;
        }
        //    USE_ABILITY,

        private void HandleOnSkillUse (BattleParticipant skillCasterOwner, Entity skillCaster, Entity skillTarget, Battle skillCurrentBattle, SkillScriptableObject usedSkill)
        {
            InvokeOnEntryLogCreatedEvent(new SkillUsedCombatLogEntry(skillCasterOwner, skillCaster, skillTarget, skillCurrentBattle, usedSkill));
        }
        //DIED,

        private void HandleOnEntityDeath (Entity entity, BattleParticipant owner)
        {
            InvokeOnEntryLogCreatedEvent(new EntityDiedCombatLogEntry(entity, owner));
        }
    }
}
