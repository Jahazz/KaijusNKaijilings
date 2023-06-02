using MVC.List;
using System.Collections.ObjectModel;

namespace StatusEffects.BattlegroundStatusEffects.UI
{
    public class BattlegroundStatusEffectsListModel : ListModel<BattlegroundStatusEffectsListElement, BattlegroundStatusEffect, BattlegroundStatusEffectsListView>
    {
        private ObservableCollection<BattlegroundStatusEffect> SourceCollection;
        public void Initialize (ObservableCollection<BattlegroundStatusEffect> sourceCollection)
        {
            CurrentView.ClearList();

            if (SourceCollection != null)
            {
                SourceCollection.CollectionChanged -= HandleSourceCollectionChanged;
            }

            SourceCollection = sourceCollection;
            SourceCollection.CollectionChanged += HandleSourceCollectionChanged;
        }

        private void HandleSourceCollectionChanged (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CurrentView.ClearList();
            InitializeFromSourceList();
        }

        private void InitializeFromSourceList ()
        {
            foreach (BattlegroundStatusEffect singleStatusEffect in SourceCollection)
            {
                CurrentView.AddNewItem(singleStatusEffect);
            }
        }
    }
}