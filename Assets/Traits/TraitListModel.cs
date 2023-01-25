using MVC.List;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using UnityEngine;

public class TraitListModel : ListModel<TraitListElement, TraitBaseScriptableObject, TraitListView>
{
    private ObservableCollection<TraitBaseScriptableObject> TraitDataCollection { get; set; }

    public void InitializeTraits (ObservableCollection<TraitBaseScriptableObject> traitDataCollection)
    {
        if (TraitDataCollection != null)
        {
            TraitDataCollection.CollectionChanged -= ResetCollection;
        }

        TraitDataCollection = traitDataCollection;
        TraitDataCollection.CollectionChanged += ResetCollection;
        ResetCollection(null, null);
    }

    private void ResetCollection (object sender, NotifyCollectionChangedEventArgs e)
    {
        CurrentView.ClearList();

        foreach (TraitBaseScriptableObject item in TraitDataCollection)
        {
            CurrentView.AddNewItem(item);
        }
    }
}
