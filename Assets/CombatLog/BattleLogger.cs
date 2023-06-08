using BattleCore;
using System.Collections.Generic;
using UnityEngine;

namespace CombatLogging.EventHandling
{
    public class BattleLogger : MonoBehaviour
    {
        private Battle CurrentBattle { get; set; }
        private List<BattleParticipantLogger> BattleParticipantsCollection = new List<BattleParticipantLogger>();

        public void Initialize (Battle currentBattle)
        {
            Dispose();

            CurrentBattle = currentBattle;

            foreach (BattleParticipant participant in currentBattle.BattleParticipantsCollection)
            {
                BattleParticipantsCollection.Add(new BattleParticipantLogger(participant));
            }

            CurrentBattle.BattlegroundStatusEffects.CollectionChanged += HandleBattlegroundStatusEffectsChanged;
            CurrentBattle.OnBattleFinished += HandleOnBattleFinished;
            CurrentBattle.OnTurnStart += HandleOnTurnStart;
            CurrentBattle.OnTurnEnd += HandleOnTurnEnd;
            CurrentBattle.OnSkillUse += HandleOnSkillUse;
            CurrentBattle.OnEntityDeath += HandleOnEntityDeath;
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
        private void HandleBattlegroundStatusEffectsChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }
        //RESULT,
        private void HandleOnBattleFinished (BattleResultType battleResult)
        {
            throw new System.NotImplementedException();
        }
        //NEW_TURN/endTurn
        //    USE_ABILITY,

        private System.Collections.IEnumerator HandleOnTurnStart (int turnCOunter)
        {
            throw new System.NotImplementedException();
        }

        private System.Collections.IEnumerator HandleOnTurnEnd (int turnCOunter)
        {
            throw new System.NotImplementedException();
        }
        //    USE_ABILITY,

        private void HandleOnSkillUse (BattleParticipant skillCasterOwner, Entity skillCaster, Entity skillTarget, Battle skillCurrentBattle, SkillScriptableObject usedSkill)
        {
            throw new System.NotImplementedException();
        }
        //DIED,

        private void HandleOnEntityDeath (Entity entity, BattleParticipant owner)
        {
            throw new System.NotImplementedException();
        }
    }
}
