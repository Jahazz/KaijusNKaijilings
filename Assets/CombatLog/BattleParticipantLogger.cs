using BattleCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CombatLogging.EventHandling
{
    public class BattleParticipantLogger
    {
        private BattleParticipant CurrentBattleParticipant { get; set; }
        private EntityLogger CurrentEntityLogger { get; set; }
        private BattleLogger BattleLoggerReference { get; set; }

        public BattleParticipantLogger (BattleParticipant battleParticipant, BattleLogger battleLoggerReference)
        {
            CurrentBattleParticipant = battleParticipant;
            BattleLoggerReference = battleLoggerReference;

            CurrentBattleParticipant.CurrentEntity.OnVariableChange += HandleOnEntityChange;
        }

        //RECALLED,
        //SUMMONED,
        private void HandleOnEntityChange (Entity newValue, Entity oldValue)
        {
            CurrentEntityLogger?.Dispose();

            CurrentEntityLogger = new EntityLogger(newValue, BattleLoggerReference);
        }

        public void Dispose ()
        {
            CurrentEntityLogger.Dispose();

            CurrentBattleParticipant.CurrentEntity.OnVariableChange -= HandleOnEntityChange;
        }
    }
}
