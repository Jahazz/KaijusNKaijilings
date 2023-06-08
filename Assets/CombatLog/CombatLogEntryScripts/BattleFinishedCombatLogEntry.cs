using BattleCore;

namespace CombatLogging.Entries
{
    public class BattleFinishedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleResultType Result { get; private set; }
        public Player FirstParticipant { get; private set; }
        public Player SecondParticipant { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.BATTLE_FINISHED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}