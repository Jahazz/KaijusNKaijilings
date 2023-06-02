using MVC.List;
using System.Collections.ObjectModel;

namespace StatusEffects.EntityStatusEffects.UI
{
    public class EntityStatusEffectsListModel : ListModel<EntityStatusEffectsListElement, EntityStatusEffect, EntityStatusEffectsListView>
    {
        private ObservableCollection<EntityStatusEffect> SourceCollection;

        public void Initialize (ObservableCollection<EntityStatusEffect> sourceCollection)
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
            foreach (EntityStatusEffect singleStatusEffect in SourceCollection)
            {
                CurrentView.AddNewItem(singleStatusEffect);
            }
        }
    }
}