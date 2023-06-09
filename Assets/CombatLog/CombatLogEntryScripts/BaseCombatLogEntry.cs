namespace CombatLogging.Entries
{
    public abstract class BaseCombatLogEntry
    {
        public abstract CombatLogEntryType CurrentActionType { get; protected set; }
        protected abstract string ENTRY_FORMAT {get; set;} 

        public abstract string EntryToString ();
    }
}

