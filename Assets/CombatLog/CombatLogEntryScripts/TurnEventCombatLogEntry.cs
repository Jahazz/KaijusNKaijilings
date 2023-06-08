namespace CombatLogging.Entries
{
    public class TurnEventCombatLogEntry : BaseCombatLogEntry
    {
        public TurnEventType CurrentEventType { get; private set; }
        public int TurnNumber { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.TURN_CHANGED;

        public override string EntryToString ()
        {
            throw new System.NotImplementedException();
        }
    }
}
