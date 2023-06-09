using BattleCore;

namespace CombatLogging.Entries
{
    public class BattleFinishedCombatLogEntry : BaseCombatLogEntry
    {
        public BattleResultType Result { get; private set; }
        public Player FirstParticipant { get; private set; }
        public Player SecondParticipant { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.BATTLE_FINISHED;
        protected override string ENTRY_FORMAT { get; set; } = "Battle {0} vs {1} has finished. Result: {2}.";

        public BattleFinishedCombatLogEntry (BattleResultType result, Player firstParticipant, Player secondParticipant)
        {
            Result = result;
            FirstParticipant = firstParticipant;
            SecondParticipant = secondParticipant;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, FirstParticipant.Name, SecondParticipant.Name, Result.ToString());
        }
    }
}