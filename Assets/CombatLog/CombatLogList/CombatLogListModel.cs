using BattleCore;
using CombatLogging.Entries;
using CombatLogging.EventHandling;
using MVC.List;

namespace CombatLogging.UI
{
    public class CombatLogListModel : ListModel<CombatLogListElement, BaseCombatLogEntry, CombatLogListView>
    {
        private BattleLogger CurrentLogger { get; set; }

        public void Initialize(Battle currentBattle)
        {
            CurrentLogger?.Dispose();

            if (CurrentLogger != null)
            {
                CurrentLogger.OnLogEntryCreated -= HandeOnEntryCreated;
            }

            CurrentLogger = new BattleLogger(currentBattle);
            CurrentLogger.OnLogEntryCreated += HandeOnEntryCreated;
            CurrentView.ClearList();

        }

        private void HandeOnEntryCreated (BaseCombatLogEntry createdEntry)
        {
            CurrentView.AddNewItem(createdEntry);
        }
    }
}
