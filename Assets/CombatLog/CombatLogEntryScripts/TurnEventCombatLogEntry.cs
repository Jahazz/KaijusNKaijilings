namespace CombatLogging.Entries
{
    public class TurnEventCombatLogEntry : BaseCombatLogEntry
    {
        public TurnEventType CurrentEventType { get; private set; }
        public int TurnNumber { get; private set; }
        public override CombatLogEntryType CurrentActionType { get; protected set; } = CombatLogEntryType.TURN_CHANGED;
        protected override string ENTRY_FORMAT { get; set; } = "Turn {0} has {1}.";

        public TurnEventCombatLogEntry (TurnEventType currentEventType, int turnNumber)
        {
            CurrentEventType = currentEventType;
            TurnNumber = turnNumber;
        }

        public override string EntryToString ()
        {
            return string.Format(ENTRY_FORMAT, TurnNumber, CurrentEventType.ToString().ToLower());
        }
    }
}
