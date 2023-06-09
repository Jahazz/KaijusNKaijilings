using BattleCore;
using CombatLogging.Entries;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatLogging.EventHandling
{
    public class BattleParticipantLogger
    {
        public BattleParticipant CurrentBattleParticipant { get; private set; }
        public BattleLogger BattleLoggerReference { get; private set; }
        private EntityLogger CurrentEntityLogger { get; set; }

        public BattleParticipantLogger (BattleParticipant battleParticipant, BattleLogger battleLoggerReference)
        {
            CurrentBattleParticipant = battleParticipant;
            BattleLoggerReference = battleLoggerReference;

            HandleOnEntityChange(CurrentBattleParticipant.CurrentEntity.PresentValue, null);
            CurrentBattleParticipant.CurrentEntity.OnVariableChange += HandleOnEntityChange;
        }

        //RECALLED,
        //SUMMONED,
        private void HandleOnEntityChange (Entity newValue, Entity oldValue)
        {
            CurrentEntityLogger?.Dispose();

            CurrentEntityLogger = new EntityLogger(newValue, this);

            BattleLoggerReference.InvokeOnEntryLogCreatedEvent(new EntityChangedCombatLogEntry(newValue, oldValue, CurrentBattleParticipant));
        }

        public void Dispose ()
        {
            CurrentEntityLogger.Dispose();

            CurrentBattleParticipant.CurrentEntity.OnVariableChange -= HandleOnEntityChange;
        }
    }
}
